using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NasaEpicApiClient.Models;

namespace NasaEpicApiClient
{
    public interface INasaEpicApiClient
    {
        public Task<IReadOnlyCollection<DateTime>> GetNaturalImageAvailableDates(CancellationToken cancellationToken = default);

        public Task<IReadOnlyCollection<ImageModel>> GetImagesForDate(DateTime date, CancellationToken cancellationToken = default);

        public Task<RawImage> GetImageByDateName(DateTime date, string format, string name, CancellationToken cancellationToken = default);
    }
}
