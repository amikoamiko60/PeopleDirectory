using PeopleDirectory.DataContracts.Constants;
using PeopleDirectory.DataContracts.Enums;
using PeopleDirectory.DataContracts.Resources;
using System.ComponentModel.DataAnnotations;

namespace PeopleDirectory.DataContracts.Requests
{
    public class EditPhoneNumberRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [EnumDataType(typeof(PhoneType), ErrorMessageResourceName = ValidationMessageKeys.PhoneTypeInvalid, ErrorMessageResourceType = typeof(ValidationMessages))]
        public PhoneType Type { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessageResourceName = ValidationMessageKeys.PhoneNumberMinLength, ErrorMessageResourceType = typeof(ValidationMessages))]
        public string Number { get; set; }
    }
}
