using Microsoft.EntityFrameworkCore;
using PeopleDirectory.BusinessLogic.Mappers;
using PeopleDirectory.DataAccess.Entities;
using PeopleDirectory.DataAccess.Interfaces.Repositories;
using PeopleDirectory.DataContracts.Constants;
using PeopleDirectory.DataContracts.Requests;
using PeopleDirectory.DataContracts.Resources;
using PeopleDirectory.DataContracts.Responses;
using System.Data;

namespace PeopleDirectory.DataAccess.Repositories
{
    internal sealed class IndividualRepository(ApplicationDbContext db) : IIndividualRepository
    {
        public async Task<bool> CheckIndividualExistsAsync(int id, CancellationToken cancellationToken = default)
        => await db.Individuals.Where(a => a.Id == id).AnyAsync(cancellationToken);

        public async Task CreateIndividualAsync(CreateIndividualRequest request, CancellationToken cancellationToken = default)
        {
            var individual = IndividualMapper.MapToEntity(request);

            await db.Individuals.AddAsync(individual, cancellationToken);
        }

        public async Task DeleteIndividualAsync(int id, CancellationToken cancellationToken)
        {
            var individual = await db.Individuals
            .Include(a => a.Relationships)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

            if (individual == null)
            {
                var localizedMessage = ValidationMessages.GetMessage(ValidationMessageKeys.IndividualNotFound);
                throw new DataException(localizedMessage);
            } 
            if (individual != null)
            {
                db.RelatedIndividuals.RemoveRange(individual.Relationships);

                db.Individuals.Remove(individual);
            }
        }

        public async Task EditIndividualAsync(int id, EditIndividualRequest request, CancellationToken cancellationToken = default)
        {
            var existingIndividual = await db.Individuals
                .Include(a => a.PhoneNumbers)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingIndividual == null)
            {
                var localizedMessage = ValidationMessages.GetMessage(ValidationMessageKeys.IndividualNotFound);
                throw new DataException(localizedMessage);
            }

            existingIndividual.FirstName = request.FirstName;
            existingIndividual.LastName = request.LastName;
            existingIndividual.Gender = request.Gender;
            existingIndividual.PersonalNumber = request.PersonalNumber;
            existingIndividual.BirthDate = request.BirthDate;
            existingIndividual.CityId = request.CityId;

            var phoneIdsFromRequest = request.PhoneNumbers.Select(a => a.Id).ToList();
            var existingPhoneIds = existingIndividual.PhoneNumbers.Select(a => a.Id).ToList();

            var phonesToRemove = existingIndividual.PhoneNumbers
            .Where(p => !phoneIdsFromRequest.Contains(p.Id))
            .ToList();

            foreach (var phone in phonesToRemove)
            {
                existingIndividual.PhoneNumbers.Remove(phone);
            }

            foreach (var phone in request.PhoneNumbers)
            {
                var existingPhone = existingIndividual.PhoneNumbers
                    .FirstOrDefault(a => a.Id == phone.Id);

                if (existingPhone != null)
                {
                    existingPhone.Type = phone.Type;
                    existingPhone.Number = phone.Number;
                }
                else
                {
                    existingIndividual.PhoneNumbers.Add(new PhoneNumber
                    {
                        Type = phone.Type,
                        Number = phone.Number,
                        IndividualId = id
                    });
                }
            }
        }

        public async Task SaveIndividualImageAsync(int id, string imagePath, CancellationToken cancellationToken = default)
        {
            var existingIndividual = await db.Individuals
               .Where(a => a.Id == id)
               .FirstOrDefaultAsync(cancellationToken);

            existingIndividual.ImagePath = imagePath;
        } 
        public async Task<GetIndividualResponse> GetIndividualByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await db.Individuals
                .Include(a => a.PhoneNumbers)
                .Where(a => a.Id == id)
                .Select(a => new GetIndividualResponse()
                {
                    Id = a.Id,
                    BirthDate = a.BirthDate,
                    CityId = a.CityId,
                    FirstName = a.FirstName,
                    Gender = a.Gender,
                    LastName = a.LastName,
                    PersonalNumber = a.PersonalNumber,
                    ImagePath = a.ImagePath,
                    PhoneNumbers = a.PhoneNumbers.Select(a => new GetPhoneNumbersResponse()
                    {
                        Id = a.Id,
                        Number = a.Number,
                        Type = a.Type,
                    }).ToList(),
                    RelatedIndividuals = a.Relationships.Select(a => new GetRelatedIndividualResponse()
                    {
                        Id = a.Id,
                        RelatedIndividualId = a.RelatedIndividualId,
                        RelationshipType = a.RelationshipType,
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PaginatedIndividualsResponse> GetIndividualsAsync(SearchIndividualsRequest request, CancellationToken cancellationToken)
        {
            var query = db.Individuals
                        .Include(a => a.PhoneNumbers)
                        .Include(a => a.Relationships)
                        .AsQueryable();

            if (!string.IsNullOrEmpty(request.FirstName))
            {
                query = query.Where(a => EF.Functions.Like(a.FirstName, $"%{request.FirstName}%"));
            }
            if (!string.IsNullOrEmpty(request.LastName))
            {
                query = query.Where(a => EF.Functions.Like(a.LastName, $"%{request.LastName}%"));
            }
            if (!string.IsNullOrEmpty(request.PersonalNumber))
            {
                query = query.Where(a => EF.Functions.Like(a.PersonalNumber, $"%{request.PersonalNumber}%"));
            }
            if (request.BirthDate.HasValue)
            {
                query = query.Where(a => a.BirthDate == request.BirthDate.Value);
            }
            if (request.CityId.HasValue)
            {
                query = query.Where(a => a.CityId == request.CityId.Value);
            }
            if (request.Gender.HasValue)
            {
                query = query.Where(a => a.Gender == request.Gender.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var individuals = await query
                .OrderBy(a => a.LastName) 
                .ThenBy(a => a.FirstName)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(a => new GetIndividualResponse
                {
                    Id = a.Id,
                    BirthDate = a.BirthDate,
                    CityId = a.CityId,
                    FirstName = a.FirstName,
                    Gender = a.Gender,
                    LastName = a.LastName,
                    PersonalNumber = a.PersonalNumber,
                    ImagePath = a.ImagePath,
                    PhoneNumbers = a.PhoneNumbers.Select(p => new GetPhoneNumbersResponse
                    {
                        Id = p.Id,
                        Number = p.Number,
                        Type = p.Type,
                    }).ToList(),
                    RelatedIndividuals = a.Relationships.Select(r => new GetRelatedIndividualResponse
                    {
                        Id = r.Id,
                        RelatedIndividualId = r.RelatedIndividualId,
                        RelationshipType = r.RelationshipType,
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            return new PaginatedIndividualsResponse
            {
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                Individuals = individuals
            };
        }

        public async Task<IEnumerable<RelationshipReportResponse>> GetRelationshipReportAsync(RelationshipReportRequest request, CancellationToken cancellationToken)
        {
            var query = db.RelatedIndividuals
             .GroupBy(a => new { a.IndividualId, a.RelationshipType })
             .Select(b => new
             {
                 b.Key.IndividualId,
                 b.Key.RelationshipType,
                 Count = b.Count()
             })
             .Join(db.Individuals,
                 a => a.IndividualId,
                 b => b.Id,
                 (a, b) => new
                 {
                     b.Id,
                     IndividualName = b.FirstName + " " + b.LastName,
                     a.RelationshipType,
                     a.Count
                 });

            if (request?.RelationType.HasValue == true)
            {
                query = query.Where(a => a.RelationshipType == request.RelationType.Value);
            }

            return await query
                   .GroupBy(a => new { a.Id, a.IndividualName })
                   .Select(b => new RelationshipReportResponse
                   {
                       IndividualId = b.Key.Id,
                       IndividualName = b.Key.IndividualName,
                       RelationshipCounts = b.Select(a => new RelationshipTypeCount
                       {
                           RelationshipType = a.RelationshipType,
                           Count = a.Count
                       }).ToList()
                   })
                   .ToListAsync(cancellationToken);
        }
    }
}
