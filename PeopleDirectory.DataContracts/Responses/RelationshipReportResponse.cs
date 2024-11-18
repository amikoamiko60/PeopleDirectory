namespace PeopleDirectory.DataContracts.Responses
{
    public class RelationshipReportResponse
    {
        public int IndividualId { get; set; }
        public string IndividualName { get; set; }
        public List<RelationshipTypeCount> RelationshipCounts { get; set; }
    }
}
