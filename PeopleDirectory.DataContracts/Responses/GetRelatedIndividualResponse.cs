using PeopleDirectory.DataContracts.Enums;

namespace PeopleDirectory.DataContracts.Responses
{
    public class GetRelatedIndividualResponse
    {
        public int Id { get; set; }

        public int RelatedIndividualId { get; set; }

        public RelationType RelationshipType { get; set; }
    }
}
