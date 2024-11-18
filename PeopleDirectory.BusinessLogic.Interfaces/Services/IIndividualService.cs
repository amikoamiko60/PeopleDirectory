using Microsoft.AspNetCore.Http;
using PeopleDirectory.DataContracts.Requests;
using PeopleDirectory.DataContracts.Responses;

namespace PeopleDirectory.BusinessLogic.Interfaces.Services
{
    public interface IIndividualService
    {
        Task CreateIndividualAsync(CreateIndividualRequest request, CancellationToken cancellationToken = default);
        
        Task DeleteIndividualAsync(int id, CancellationToken cancellationToken = default);
        
        Task EditIndividualAsync(int id, EditIndividualRequest request, CancellationToken cancellationToken = default);
        
        Task GetIndividual(int id, CancellationToken cancellationToken);

        Task<string> UploadPhotoAsync(int id, IFormFile file, CancellationToken cancellationToken);


        Task<PaginatedIndividualsResponse> GetIndividualsAsync(SearchIndividualsRequest request, CancellationToken cancellationToken);
    }
}
