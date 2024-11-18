using PeopleDirectory.DataContracts.Enums;

namespace PeopleDirectory.DataContracts.Responses
{
    public class GetIndividualResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public string ImagePath { get; set; }

        public List<GetPhoneNumbersResponse> PhoneNumbers { get; set; }

        public List<GetRelatedIndividualResponse> RelatedIndividuals { get; set; }
    }
}
