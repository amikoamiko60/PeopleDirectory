namespace PeopleDirectory.DataContracts.Requests
{
    public class CreateIndividualRequest : BaseIndividualRequest
    {
        public List<CreatePhoneNumberRequest> PhoneNumbers { get; set; }
    }
}
