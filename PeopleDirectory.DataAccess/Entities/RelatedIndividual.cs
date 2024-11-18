using PeopleDirectory.DataContracts.Enums;

namespace PeopleDirectory.DataAccess.Entities
{
    public class RelatedIndividual
    {
        public int Id { get; set; }

        public int IndividualId { get; set; }

        public Individual Individual { get; set; }

        public int RelatedIndividualId { get; set; }

        public Individual RelatedPerson { get; set; }

        public RelationType RelationshipType { get; set; }
    }
}
