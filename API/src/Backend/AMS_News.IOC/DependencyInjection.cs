using System.Reflection;
using AMS_News.Application.Login;
using AMS_News.Application.Services.AutoMapper;
using AMS_News.Application.UseCases.Like;
using AMS_News.Application.UseCases.Like.DoLike;
using AMS_News.Application.UseCases.Like.GetMoreLiked;
using AMS_News.Application.UseCases.Login;
using AMS_News.Application.UseCases.News;
using AMS_News.Application.UseCases.News.Delete;
using AMS_News.Application.UseCases.News.Edit;
using AMS_News.Application.UseCases.News.Get;
using AMS_News.Application.UseCases.News.GetById;
using AMS_News.Application.UseCases.News.GetByUser;
using AMS_News.Application.UseCases.News.Register;
using AMS_News.Application.UseCases.User.ChangePassword;
using AMS_News.Application.UseCases.User.Profile;
using AMS_News.Application.UseCases.User.Registrer;
using AMS_News.Application.UseCases.User.Update;
using AMS_News.Domain.Contracts;
using AMS_News.Domain.Contracts.Like;
using AMS_News.Domain.Contracts.News;
using AMS_News.Domain.Contracts.Storage;
using AMS_News.Domain.Contracts.Token;
using AMS_News.Domain.Contracts.User;
using AMS_News.Infraestructure.Data;
using AMS_News.Infraestructure.Repositories;
using AMS_News.Infraestructure.Security.Encrypter;
using AMS_News.Infraestructure.Security.Token.Access.Generate;
using AMS_News.Infraestructure.Security.Token.Access.Validate;
using AMS_News.Infraestructure.Services.Blob;
using AMS_News.Infraestructure.Services.LoggedUser;
using AMS_News.Infrastructure.Services.Storage;
using AutoMapper;
using Azure.Storage.Blobs;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AMS_News.IOC;

public static class DependencyInjection
{
    public static void AddAllServices(this IServiceCollection services,IConfiguration configuration)
    {
        AddFluentMigration(services,configuration);
        AddDbContext(services,configuration);
        AddRepositories(services);
        AddAutoMapper(services);
        AddTokens(services,configuration);
        AddAzureBlob(services,configuration);
    }
    
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IPasswordEncrypter>(ops => new PasswordEncrypter());
        services.AddScoped<IUnityOfWork, UnityOfWork>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetProfileUserUseCase, GetProfileUserUseCase>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<INewsReadOnlyRepository, NewsRepository>();
        services.AddScoped<IGetNewsUseCase, GetNewsUseCase>();
        services.AddScoped<IRegisterNewsUseCase, RegisterNewsUseCase>();
        services.AddScoped<INewsWriteOnlyRepository, NewsRepository>();
        services.AddScoped<IGetByIdNewsUseCase, GetByIdNewsUseCase>();
        services.AddScoped<IEditNewsUseCase, EditNewsUseCase>();
        services.AddScoped<IGetByUserNewsUseCase, GetByUserNewsUseCase>();
        services.AddScoped<INewsDeleteOnlyRepository, NewsRepository>();
        services.AddScoped<IDeleteNewsUseCase, DeleteNewsUseCase>();
        services.AddScoped<ILikeWriteOnlyRepository, LikeRepository>();
        services.AddScoped<IDoLikeUseCase, DoLikeUseCase>();
        services.AddScoped<ILikeReadOnlyRepository, LikeRepository>();
        services.AddScoped<IGetMoreLiked, GetMoreLiked>();
    }

    private static void AddDbContext(IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AmsNewsContext>(opts =>
        {
            opts.UseSqlite(connectionString);
        });
    }
    
    public static void RunMigrations(this IApplicationBuilder services)
    {
        var serviceProvider = services.ApplicationServices;
        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();
        runner.MigrateUp();
    }
    
    private static void AddTokens(IServiceCollection service, IConfiguration configuration)
    {
        var expirationTokenInMinutes = configuration.GetValue<uint>("Settings:JwtToken:ExpirationTokenInMinutes");
        var signKey = configuration.GetValue<string>("Settings:JwtToken:SignKey");

        service.AddScoped<ITokenGenerator>(option => new JwtAccessTokenGenerator(expirationTokenInMinutes, signKey!));
        service.AddScoped<ITokenValidator>(option => new JwtTokenValidator(signKey!));
        service.AddScoped<ILoggedUser, LoggedUser>();
    }
    
    private static void AddAzureBlob(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var app = serviceCollection.BuildServiceProvider();
        var isDevelopment = app.GetRequiredService<IHostEnvironment>().IsDevelopment();
        if (isDevelopment)
        {
            var baseUrl = configuration.GetValue<string>("Storage:BaseUrl");
            serviceCollection.AddScoped<IBlobStorageService>(opts => new LocalStorageService(opts.GetRequiredService<IWebHostEnvironment>(),baseUrl));
        }
        else
        {
         serviceCollection.AddSingleton(x => new BlobServiceClient(configuration.GetValue<string>("Settings:Azure:BlobStorage")));
         serviceCollection.AddScoped<IBlobStorageService, AzureBlobStorageService>();
        }

    }
    
    private static void AddFluentMigration(IServiceCollection service,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        service.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("AMS_News.Infraestructure")).For.Migrations());
    }
    
    private static void AddAutoMapper(IServiceCollection service)
    {
        var autoMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        }).CreateMapper();
        service.AddScoped(opts => autoMapper);
    }
}