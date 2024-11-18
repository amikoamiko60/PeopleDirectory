using PeopleDirectory.DataContracts.Enums;

namespace PeopleDirectory.DataContracts.Responses
{
    public class GetPhoneNumbersResponse
    {
        public int Id { get; set; }

        public PhoneType Type { get; set; }

        public string Number { get; set; }
    }
}
