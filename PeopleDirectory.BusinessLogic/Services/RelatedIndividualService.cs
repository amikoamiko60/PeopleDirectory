using PeopleDirectory.BusinessLogic.Interfaces.Services;
using PeopleDirectory.DataAccess.Interfaces.Repositories;
using PeopleDirectory.DataAccess.Interfaces;
using PeopleDirectory.DataContracts.Requests;
using PeopleDirectory.DataContracts.Responses;
using PeopleDirectory.BusinessLogic.Exceptions;
using PeopleDirectory.DataContracts.Resources;

namespace PeopleDirectory.BusinessLogic.Services
{
    internal sealed class RelatedIndividualService
        (IIndividualRepository individualRepo,
          IUnitOfWork unitOfWork,
          IRelatedIndividualRepository relatedIndividualRepo) : IRelatedIndividualService
    {
        public async Task CreateRelatedIndividualAsync(int id, CreateRelatedIndividualRequest request, CancellationToken cancellationToken)
        {
            var individualExists = await individualRepo.CheckIndividualExistsAsync(id, cancellationToken);

            var relatedIndividualExists = await individualRepo.CheckIndividualExistsAsync(request.RelatedIndividualId, cancellationToken);

            if (!individualExists || !relatedIndividualExists)
            {
                var localizedMessage = ValidationMessages.GetMessage("OneOrBothIndividualNotFound");
                throw new KeyNotFoundException(localizedMessage);
            }

            var relationshipExists = await relatedIndividualRepo.CheckRelationshipExistsAsync(id, request.RelatedIndividualId, cancellationToken);
            
            if (relationshipExists)
            {
                var localizedMessage = ValidationMessages.GetMessage("RelationshipAlreadyExists");
                throw new KeyNotFoundException(localizedMessage);
            }

            await relatedIndividualRepo.CreateRelatedIndividualAsync(id, request, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<RelationshipReportResponse>> GetRelationshipReportAsync(RelationshipReportRequest request, CancellationToken cancellationToken)
        => await individualRepo.GetRelationshipReportAsync(request, cancellationToken);

        public async Task RemoveRelatedIndividualAsync(int id, int relationId, CancellationToken cancellationToken)
        {
            var individualExists = await individualRepo.CheckIndividualExistsAsync(id, cancellationToken);

            if (!individualExists)
            {
                var localizedMessage = ValidationMessages.GetMessage("IndividualNotFound");
                throw new BusinessException(localizedMessage);
            }

            await relatedIndividualRepo.RemoveRelatedIndividualAsync(relationId, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
