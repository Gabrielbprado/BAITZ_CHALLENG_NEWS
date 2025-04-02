using AMS_News.Communication.Response.News;
using AMS_News.Domain.Contracts.News;
using AMS_News.Domain.Contracts.Token;
using AutoMapper;

namespace AMS_News.Application.UseCases.News.GetByUser;

public class GetByUserNewsUseCase(ILoggedUser loggedUser,IMapper mapper,INewsReadOnlyRepository repository) : IGetByUserNewsUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IMapper _mapper = mapper;
    private readonly INewsReadOnlyRepository _repository = repository;
    public async Task<List<ResponseNewsJson>> ExecuteAsync()
    {
        var user = await _loggedUser.User();
        var news = await _repository.GetNewsByUser(user.Id);
        return _mapper.Map<List<ResponseNewsJson>>(news);
    }
}