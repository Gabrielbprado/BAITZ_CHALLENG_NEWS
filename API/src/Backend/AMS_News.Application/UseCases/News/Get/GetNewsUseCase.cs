using AMS_News.Application.Extensions;
using AMS_News.Communication.Response.News;
using AMS_News.Domain.Contracts.News;
using AMS_News.Domain.Contracts.Storage;
using AMS_News.Domain.Contracts.User;
using AutoMapper;

namespace AMS_News.Application.UseCases.News.Get;

public class GetNewsUseCase(
    INewsReadOnlyRepository readOnlyRepository,
    IMapper mapper,
    IBlobStorageService blobStorageService,
    IUserReadOnlyRepository userReadOnlyRepository) : IGetNewsUseCase
{
    private readonly INewsReadOnlyRepository _readOnlyRepository = readOnlyRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;

    public async Task<IEnumerable<ResponseNewsJson>> Execute()
    {
        var listNews = await _readOnlyRepository.GetNews();
        var response = _mapper.Map<IEnumerable<ResponseNewsJson>>(listNews);

        foreach (var responseNews in response)
        {
            var news = listNews.FirstOrDefault(n => n.Id == responseNews.Id);
            if (news?.ImageIdentifier is not null)
            {
                var user = await _userReadOnlyRepository.GetById(news.CustomerId);
                responseNews.ImageUrl = $"{UrlApiImage.API_URL_IMAGE}{await _blobStorageService.GetUri(user, news.ImageIdentifier)}";
            }
        }

        return response;
    }
}