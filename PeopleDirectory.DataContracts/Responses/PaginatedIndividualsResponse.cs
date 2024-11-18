namespace PeopleDirectory.DataContracts.Responses
{
    public class PaginatedIndividualsResponse
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<GetIndividualResponse> Individuals { get; set; }
    }
}
