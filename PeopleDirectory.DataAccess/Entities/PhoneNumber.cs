using PeopleDirectory.DataContracts.Enums;

namespace PeopleDirectory.DataAccess.Entities
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        public int IndividualId { get; set; }

        public Individual Individual { get; set; }

        public PhoneType Type { get; set; }  

        public string Number { get; set; }
    }
}
