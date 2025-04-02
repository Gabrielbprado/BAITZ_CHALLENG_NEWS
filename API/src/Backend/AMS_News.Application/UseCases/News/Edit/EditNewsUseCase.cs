using AMS_News.Application.Extensions;
using AMS_News.Application.UseCases.News.Register;
using AMS_News.Communication.Request.News;
using AMS_News.Domain.Contracts;
using AMS_News.Domain.Contracts.News;
using AMS_News.Domain.Contracts.Storage;
using AMS_News.Domain.Contracts.Token;
using AMS_News.Exceptions.BaseExceptions;
using AutoMapper;

namespace AMS_News.Application.UseCases.News.Edit;

public class EditNewsUseCase(INewsWriteOnlyRepository writeOnlyRepository,INewsReadOnlyRepository newsReadOnlyRepository,ILoggedUser loggedUser,IMapper mapper,IUnityOfWork unityOfWork,IBlobStorageService blob) : IEditNewsUseCase
{
    private readonly INewsWriteOnlyRepository _writeOnlyRepository = writeOnlyRepository;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IMapper _mapper = mapper;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IBlobStorageService _blob = blob;
    private readonly INewsReadOnlyRepository _newsReadOnlyRepository = newsReadOnlyRepository;
    
    public async Task ExecuteAsync(long id, RequestNewsJson request)
    {
        await Validate(request);
        var user = await _loggedUser.User();
        var news = await _newsReadOnlyRepository.GetNewsById(id);
        news = _mapper.Map(request, news);
        news.Author = user.Name;
        var topics = _mapper.Map<IList<Domain.Entities.Topics>>(request.Topics);
        news.Topics = topics;
        news.CustomerId = user.Id;
        if (request.Image is not null && request.Image.FileName != news.ImageIdentifier)
        {
            
            var fileStream = request.Image.OpenReadStream();

            (var isValidImage, var extension) = fileStream.ValidateAndGetImageExtension();

            if (isValidImage is false)
            {
                throw new ErrorOnValidatorException(["Only images are accepted"]);
            }

            news.ImageIdentifier = $"{Guid.NewGuid()}{extension}";

            await _blob.Upload(user, fileStream,news.ImageIdentifier);

        }
        
        foreach (var topic in request.Topics)
        {
            if (topic.Image is not null)
            {
                var topicFileStream = topic.Image.OpenReadStream();
                (var isValidImage, var extension) = topicFileStream.ValidateAndGetImageExtension();

                if (isValidImage is false)
                {
                    throw new ErrorOnValidatorException(["Only images are accepted for topics"]);
                }

                var imageIdentifier = $"{Guid.NewGuid()}{extension}"; 
                 await _blob.Upload(user, topicFileStream, imageIdentifier);
            }
        }

         _writeOnlyRepository.UpdateNews(news);
        await _unityOfWork.Commit();
    }
    
    private async Task Validate(RequestNewsJson request)
    {
        var validate = new NewsValidator();
        var result = await validate.ValidateAsync(request);
        if (result.IsValid is false)
            throw new ErrorOnValidatorException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}