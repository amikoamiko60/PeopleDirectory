using PeopleDirectory.DataContracts.Constants;
using PeopleDirectory.DataContracts.Enums;
using PeopleDirectory.DataContracts.Resources;
using System.ComponentModel.DataAnnotations;

namespace PeopleDirectory.DataContracts.Requests
{
    public class CreateRelatedIndividualRequest
    {
        public int RelatedIndividualId { get; set; }

        [EnumDataType(typeof(RelationType), ErrorMessageResourceName = ValidationMessageKeys.RelatedPersonTypeInvalid, ErrorMessageResourceType = typeof(ValidationMessages))]
        public RelationType RelationshipType { get; set; }
    }
}
