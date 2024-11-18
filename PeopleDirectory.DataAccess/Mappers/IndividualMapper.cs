using PeopleDirectory.DataAccess.Entities;
using PeopleDirectory.DataContracts.Requests;

namespace PeopleDirectory.BusinessLogic.Mappers
{
    public static class IndividualMapper
    {
        public static Individual MapToEntity(CreateIndividualRequest request)
        {
            return new Individual
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                PersonalNumber = request.PersonalNumber,
                BirthDate = request.BirthDate,
                CityId = request.CityId,
                PhoneNumbers = request.PhoneNumbers.Select(a => new PhoneNumber
                {
                    Type = a.Type,
                    Number = a.Number
                }).ToList()
            };
        }
    }
}
