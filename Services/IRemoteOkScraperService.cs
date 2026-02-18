namespace Career_Path.Services
{
    public interface IRemoteOkScraperService
    {
        Task<Result<RemoteOkJobResponse>> SearchJobsAsync(RemoteOkSearchRequest request, CancellationToken cancellationToken = default);
    }
}
