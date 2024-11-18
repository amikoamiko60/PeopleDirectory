using Microsoft.AspNetCore.Mvc;
using PeopleDirectory.BusinessLogic.Interfaces.Services;
using PeopleDirectory.DataContracts.Requests;
using PeopleDirectory.DataContracts.Responses;

namespace PeopleDirectory.Api.Controllers
{
    [Route("individual")]
    public class IndividualController
        (IIndividualService inidividualService,
        IRelatedIndividualService relatedIndividualService) : ControllerBase
    {
        [HttpGet]
        public async Task<PaginatedIndividualsResponse> GetIndividuals([FromQuery] SearchIndividualsRequest request, CancellationToken cancellationToken)
        => await inidividualService.GetIndividualsAsync(request, cancellationToken);

        [HttpPost]
        public async Task CreateIndividual([FromBody]CreateIndividualRequest request, CancellationToken cancellationToken)
        => await inidividualService.CreateIndividualAsync(request, cancellationToken);

        [HttpPut("{id}")]
        public async Task EditIndividual([FromRoute] int id, [FromBody] EditIndividualRequest request, CancellationToken cancellationToken)
          => await inidividualService.EditIndividualAsync(id, request, cancellationToken);

        [HttpGet("{id}")]
        public async Task GetIndividual([FromRoute] int id, CancellationToken cancellationToken)
         => await inidividualService.GetIndividualAsync(id, cancellationToken);

        [HttpDelete("{id}")]
        public async Task DeleteIndividual([FromRoute] int id, CancellationToken cancellationToken)
          => await inidividualService.DeleteIndividualAsync(id, cancellationToken);

        [HttpPost("{id}/add-related-individual")]
        public async Task CreateRelatedIndividual([FromRoute] int id, [FromBody] CreateRelatedIndividualRequest request, CancellationToken cancellationToken)
         =>  await relatedIndividualService.CreateRelatedIndividualAsync(id, request, cancellationToken);

        [HttpDelete("{id}/remove-related-individual/{relationId}")]
        public async Task RemoveRelatedIndividual([FromRoute] int id, [FromRoute] int relationId, CancellationToken cancellationToken)
        => await relatedIndividualService.RemoveRelatedIndividualAsync(id, relationId, cancellationToken);

        [HttpGet("relationship-report")]
        public async Task<IEnumerable<RelationshipReportResponse>> GetRelationshipReport([FromQuery] RelationshipReportRequest request, CancellationToken cancellationToken)
        => await relatedIndividualService.GetRelationshipReportAsync(request, cancellationToken);

        [HttpPost("{id}/upload-photo")]
        public async Task<string> UploadPhoto([FromRoute] int id, [FromForm] IFormFile file, CancellationToken cancellationToken)
        => await inidividualService.UploadPhotoAsync(id, file, cancellationToken);
    }
}
