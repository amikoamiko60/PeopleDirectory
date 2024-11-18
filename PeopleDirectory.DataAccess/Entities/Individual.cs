using PeopleDirectory.DataContracts.Enums;

namespace PeopleDirectory.DataAccess.Entities
{
    public class Individual
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public string ImagePath { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public ICollection<RelatedIndividual> Relationships { get; set; }
    }
}
