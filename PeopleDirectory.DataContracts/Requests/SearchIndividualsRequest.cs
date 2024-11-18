using PeopleDirectory.DataContracts.Enums;

namespace PeopleDirectory.DataContracts.Requests
{
    public class SearchIndividualsRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? CityId { get; set; }

        public Gender? Gender { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
