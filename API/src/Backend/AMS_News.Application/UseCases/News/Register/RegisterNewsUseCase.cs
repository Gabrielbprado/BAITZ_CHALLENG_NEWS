using AMS_News.Application.Extensions;
using AMS_News.Communication.Request.News;
using AMS_News.Communication.Response.News;
using AMS_News.Domain.Contracts;
using AMS_News.Domain.Contracts.News;
using AMS_News.Domain.Contracts.Storage;
using AMS_News.Domain.Contracts.Token;
using AMS_News.Exceptions.BaseExceptions;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AMS_News.Application.UseCases.News.Register;

public class RegisterNewsUseCase(INewsWriteOnlyRepository writeOnlyRepository,ILoggedUser loggedUser,IMapper mapper,IUnityOfWork unityOfWork,IBlobStorageService blob) : IRegisterNewsUseCase
{
    private readonly INewsWriteOnlyRepository _writeOnlyRepository = writeOnlyRepository;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IMapper _mapper = mapper;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IBlobStorageService _blob = blob;

    public async Task<ResponseRegiteredNewsJson> Execute(RequestNewsJson request)
    {
        await Validate(request);
        var user = await _loggedUser.User();
        var news = _mapper.Map<Domain.Entities.News>(request);
        news.Author = user.Name;
        var topics = _mapper.Map<IList<Domain.Entities.Topics>>(request.Topics);
        news.Topics = topics;
        news.CustomerId = user.Id;
        if (request.Image is not null)
        {
            
            var fileStream = request.Image.OpenReadStream();

            (var isValidImage, var extension) = fileStream.ValidateAndGetImageExtension();

            if (isValidImage is false)
            {
                throw new ErrorOnValidatorException(["Only images are accepted"]);
            }

            news.ImageIdentifier = $"{Guid.NewGuid()}{extension}";

             await _blob.Upload(user, fileStream, news.ImageIdentifier);
        }

        if (topics.Count > 0)
        {
            
        
        for (int i = 0; i < request.Topics.Count; i++)
        {
            var topicRequest = request.Topics[i];
            var topic = news.Topics[i]; 

            if (topicRequest.Image is not null)
            {
                var topicFileStream = topicRequest.Image.OpenReadStream();
                (var isValidImage, var extension) = topicFileStream.ValidateAndGetImageExtension();

                if (isValidImage is false)
                {
                    throw new ErrorOnValidatorException(["Only images are accepted for topics"]);
                }

                var imageIdentifier = $"{Guid.NewGuid()}{extension}";

                topic.ImageIdentifier = imageIdentifier;

                await _blob.Upload(user, topicFileStream, imageIdentifier);
            }
        } 
}


        await _writeOnlyRepository.AddNews(news);
        await _unityOfWork.Commit();
        return new ResponseRegiteredNewsJson()
        {
            Id = news.Id,
            Title = news.Title,
        };
    }

    private async Task Validate(RequestNewsJson request)
    {
        var validate = new NewsValidator();
        var result = await validate.ValidateAsync(request);
        if (result.IsValid is false)
            throw new ErrorOnValidatorException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }

}