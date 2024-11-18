using Microsoft.EntityFrameworkCore;
using PeopleDirectory.DataAccess.Entities;
using PeopleDirectory.DataAccess.Exceptions;
using PeopleDirectory.DataAccess.Interfaces.Repositories;
using PeopleDirectory.DataContracts.Constants;
using PeopleDirectory.DataContracts.Requests;
using PeopleDirectory.DataContracts.Resources;

namespace PeopleDirectory.DataAccess.Repositories
{
    internal sealed class RelatedIndividualRepository
        (ApplicationDbContext db) : IRelatedIndividualRepository
    {
        public async Task<bool> CheckRelationshipExistsAsync(int individualId, int relatedIndividualId, CancellationToken cancellationToken = default)
        => await db.RelatedIndividuals
             .Where(a => a.IndividualId == individualId
              && a.RelatedIndividualId == relatedIndividualId)
              .AnyAsync(cancellationToken);

        public async Task CreateRelatedIndividualAsync(int id, CreateRelatedIndividualRequest request, CancellationToken cancellationToken)
        {
            var relatedIndividual = new RelatedIndividual
            {
                IndividualId = id,
                RelatedIndividualId = request.RelatedIndividualId,
                RelationshipType = request.RelationshipType
            };

            await db.RelatedIndividuals.AddAsync(relatedIndividual, cancellationToken);
        }

        public async Task RemoveRelatedIndividualAsync(int relationId, CancellationToken cancellationToken)
        {
            var relationship = await db.RelatedIndividuals
                                      .Where(a => a.Id == relationId)
                                      .FirstOrDefaultAsync(cancellationToken);

            if (relationship == null)
            {
                var localizedMessage = ValidationMessages.GetMessage(ValidationMessageKeys.RelationshipNotFound);
                throw new DataException(localizedMessage);
            }

            db.RelatedIndividuals.Remove(relationship);
        }
    }
}
