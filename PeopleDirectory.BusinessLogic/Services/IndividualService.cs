using Microsoft.AspNetCore.Http;
using PeopleDirectory.BusinessLogic.Exceptions;
using PeopleDirectory.BusinessLogic.Interfaces.Services;
using PeopleDirectory.DataAccess.Interfaces;
using PeopleDirectory.DataAccess.Interfaces.Repositories;
using PeopleDirectory.DataContracts.Constants;
using PeopleDirectory.DataContracts.Requests;
using PeopleDirectory.DataContracts.Resources;
using PeopleDirectory.DataContracts.Responses;

namespace PeopleDirectory.BusinessLogic.Services
{
    internal sealed class IndividualService
         (IIndividualRepository individualRepo, 
          IUnitOfWork unitOfWork,
          ICityRepository cityRepo) : IIndividualService
    {
        const string ImagesFolder = "wwwroot";
        const string IndividualsFolder = "images/individuals";
        public async Task CreateIndividualAsync(CreateIndividualRequest request, CancellationToken cancellationToken = default)
        {
            await CheckCity(request.CityId, cancellationToken);

            await individualRepo.CreateIndividualAsync(request, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteIndividualAsync(int id, CancellationToken cancellationToken = default)
        {
            await individualRepo.DeleteIndividualAsync(id, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task EditIndividualAsync(int id, EditIndividualRequest request, CancellationToken cancellationToken = default)
        {
            await CheckCity(request.CityId, cancellationToken);

            await individualRepo.EditIndividualAsync(id, request, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task GetIndividualAsync(int id, CancellationToken cancellationToken)
        => await individualRepo.GetIndividualByIdAsync(id, cancellationToken);

        public async Task<PaginatedIndividualsResponse> GetIndividualsAsync(SearchIndividualsRequest request, CancellationToken cancellationToken)
        => await individualRepo.GetIndividualsAsync(request, cancellationToken);

        public async Task<string> UploadPhotoAsync(int id, IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
            {
                var localizedMessage = ValidationMessages.GetMessage(ValidationMessageKeys.InvalidFile);
                throw new BusinessException(localizedMessage);
            }

            var individual = await CheckAndGetIndividualAsync(id, cancellationToken);

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                var localizedMessage = ValidationMessages.GetMessage(ValidationMessageKeys.UnsupportedFileFormat);
                throw new BusinessException(localizedMessage);
            }

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var folderPath = Path.Combine(ImagesFolder, IndividualsFolder);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fullPath = Path.Combine(folderPath, uniqueFileName);

            if (!string.IsNullOrEmpty(individual.ImagePath))
            {
                var oldFilePath = Path.Combine(ImagesFolder, individual.ImagePath);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            var imagePath = Path.Combine(IndividualsFolder, uniqueFileName);

            await individualRepo.SaveIndividualImageAsync(id, imagePath, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return imagePath;
        }

        private async Task<GetIndividualResponse> CheckAndGetIndividualAsync(int id, CancellationToken cancellationToken = default)
        {
            var individual = await individualRepo.GetIndividualByIdAsync(id, cancellationToken);

            if (individual == null)
            {
                var localizedMessage = ValidationMessages.GetMessage(ValidationMessageKeys.IndividualNotFound);
                throw new BusinessException(localizedMessage);
            }

            return individual;
        }
        private async Task CheckCity(int cityId, CancellationToken cancellationToken = default)
        {
            var isCityExists = await cityRepo.CheckCityExistsAsync(cityId, cancellationToken);
            if (!isCityExists)
            {
                var localizedMessage = ValidationMessages.GetMessage(ValidationMessageKeys.CityNotFound);
                throw new BusinessException(localizedMessage);
            }
        }
    }
}
