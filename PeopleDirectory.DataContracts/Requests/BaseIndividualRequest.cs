using PeopleDirectory.DataContracts.Constants;
using PeopleDirectory.DataContracts.Enums;
using PeopleDirectory.DataContracts.Resources;
using PeopleDirectory.DataContracts.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeopleDirectory.DataContracts.Requests
{
    public class BaseIndividualRequest
    {
        [Required(ErrorMessageResourceName = ValidationMessageKeys.NameRequired, ErrorMessageResourceType = typeof(ValidationMessages))]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = ValidationMessageKeys.NameMinLength, ErrorMessageResourceType = typeof(ValidationMessages))]
        [RegularExpression("^[ა-ჰ]+$|^[a-zA-Z]+$", ErrorMessageResourceName = ValidationMessageKeys.NameInvalidCharacters, ErrorMessageResourceType = typeof(ValidationMessages))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = ValidationMessageKeys.SurnameRequired, ErrorMessageResourceType = typeof(ValidationMessages))]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = ValidationMessageKeys.SurnameMinLength, ErrorMessageResourceType = typeof(ValidationMessages))]
        [RegularExpression("^[ა-ჰ]+$|^[a-zA-Z]+$", ErrorMessageResourceName = ValidationMessageKeys.SurnameInvalidCharacters, ErrorMessageResourceType = typeof(ValidationMessages))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = ValidationMessageKeys.GenderRequired, ErrorMessageResourceType = typeof(ValidationMessages))]
        [EnumDataType(typeof(Gender), ErrorMessageResourceName = ValidationMessageKeys.GenderInvalid, ErrorMessageResourceType = typeof(ValidationMessages))]
        public Gender Gender { get; set; }

        [Required(ErrorMessageResourceName = ValidationMessageKeys.PersonalNumberRequired, ErrorMessageResourceType = typeof(ValidationMessages))]
        [RegularExpression(@"^\d{11}$", ErrorMessageResourceName = ValidationMessageKeys.PersonalNumberRequired, ErrorMessageResourceType = typeof(ValidationMessages))]
        public string PersonalNumber { get; set; }

        [Required(ErrorMessageResourceName = ValidationMessageKeys.BirthDateRequired, ErrorMessageResourceType = typeof(ValidationMessages))]
        [BirthDateValidation]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessageResourceName = ValidationMessageKeys.CityIdRequired, ErrorMessageResourceType = typeof(ValidationMessages))]
        public int CityId { get; set; }
    }
}
