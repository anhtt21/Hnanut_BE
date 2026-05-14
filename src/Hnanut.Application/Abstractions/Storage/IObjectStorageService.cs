namespace Hnanut.Application.Abstractions.Storage;

public interface IObjectStorageService
{
    Task<string> UploadAsync(
        Stream content,
        string objectKey,
        string contentType,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(string objectKey, CancellationToken cancellationToken = default);
}
