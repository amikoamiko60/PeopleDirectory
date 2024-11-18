using PeopleDirectory.DataContracts.Enums;

namespace PeopleDirectory.DataContracts.Responses
{
    public class RelationshipTypeCount
    {
        public RelationType RelationshipType { get; set; }
        public int Count { get; set; }
    }
}
