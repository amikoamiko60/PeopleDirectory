namespace PeopleDirectory.DataContracts.Requests
{
    public class EditIndividualRequest : BaseIndividualRequest
    {
        public List<EditPhoneNumberRequest> PhoneNumbers { get; set; }
    }
}
