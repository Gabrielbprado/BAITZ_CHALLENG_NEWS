using AMS_News.Domain.Contracts;
using AMS_News.Domain.Contracts.News;

namespace AMS_News.Application.UseCases.News.Delete;

public class DeleteNewsUseCase(INewsDeleteOnlyRepository repository,INewsReadOnlyRepository readOnlyRepository,IUnityOfWork unityOfWork) : IDeleteNewsUseCase
{
    private readonly INewsDeleteOnlyRepository _repository = repository;
    private readonly INewsReadOnlyRepository _readOnlyRepository = readOnlyRepository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;

    public async Task ExecuteAsync(long id)
    {
        var news = await _readOnlyRepository.GetNewsById(id);
        _repository.DeleteAsync(news);
        await _unityOfWork.Commit();
    }
}