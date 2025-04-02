using AMS_News.Application.Extensions;
using AMS_News.Communication.Response.News;
using AMS_News.Domain.Contracts.News;
using AMS_News.Domain.Contracts.Storage;
using AMS_News.Domain.Contracts.User;
using AutoMapper;

namespace AMS_News.Application.UseCases.News.GetById;

public class GetByIdNewsUseCase(INewsReadOnlyRepository repository,IMapper mapper,IBlobStorageService blobStorageService,IUserReadOnlyRepository userReadOnlyRepository) : IGetByIdNewsUseCase
{
    private readonly INewsReadOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    
    public async Task<ResponseNewsJson> ExecuteAsync(long id)
    {
        var news = await _repository.GetNewsById(id);
        var user = await _userReadOnlyRepository.GetById(news.CustomerId);
        var response = _mapper.Map<ResponseNewsJson>(news);
        response.ImageUrl = $"{UrlApiImage.API_URL_IMAGE}{await _blobStorageService.GetUri(user, news.ImageIdentifier)}";
        for (int i = 0; i < response.Topics.Count; i++)
        {
            response.Topics[i].ImageUrl = $"{UrlApiImage.API_URL_IMAGE}{await _blobStorageService.GetUri(user, news.Topics[i].ImageIdentifier)}";
        }
        return response;
    }
}