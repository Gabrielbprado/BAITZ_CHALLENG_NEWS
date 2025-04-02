using AMS_News.Domain.Contracts.Storage;
using AMS_News.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AMS_News.Infrastructure.Services.Storage;

public class LocalStorageService(IWebHostEnvironment environment,string baseUrl) : IBlobStorageService
{
    private readonly string _basePath =  Path.Combine(environment.WebRootPath ?? "wwwroot", "images");
    private readonly string _baseUrl = baseUrl;
    public async Task Upload(Customers user, Stream file, string fileName)
    {
        Directory.CreateDirectory(_basePath); 

        var userFolder = Path.Combine(_basePath, user.UserIdentifier.ToString());
        Directory.CreateDirectory(userFolder); // Garante que a pasta do usuário existe

        var filePath = Path.Combine(userFolder, fileName);
        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await file.CopyToAsync(fileStream);
    }

    public Task<string> GetUri(Customers user, string fileName)
    {
        Directory.CreateDirectory(_basePath); 

        var userFolder = Path.Combine(_basePath, user.UserIdentifier.ToString());
        var filePath = Path.Combine(userFolder, fileName);

        if (!File.Exists(filePath))
            return Task.FromResult(string.Empty);

        // Construa a URL corretamente usando o baseUrl definido no appsettings
        var fileUrl = $"{_baseUrl}/{user.UserIdentifier}/{fileName}";
        return Task.FromResult(fileUrl);
    }
}