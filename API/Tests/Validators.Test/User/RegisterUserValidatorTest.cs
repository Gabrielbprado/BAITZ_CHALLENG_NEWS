using AMS_News.Application.UseCases.User.Register;
using AMS_News.Application.UseCases.User.Registrer;
using CommunUtilities.Request;
using FluentAssertions;

namespace Validators.Test.User;

public class RegisterUserValidatorTest
{

    [Fact]
    public async Task Success()
    {
        var request =  RequestRegisterUserJsonBuilder.Builder();
        var validate = new RegisterUserValidator();
        var result = await validate.ValidateAsync(request);
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Error_Name_Empty()
    {
        var request =  RequestRegisterUserJsonBuilder.Builder();
        var validate = new RegisterUserValidator();
        request.Name = string.Empty;
        var result = await validate.ValidateAsync(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task Password_Invalid(int lentgh)
    {
        var request =  RequestRegisterUserJsonBuilder.Builder(lentgh);
        var validate = new RegisterUserValidator();
        request.Name = string.Empty;
        var result = await validate.ValidateAsync(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
    }
    

    [Fact]

    public async Task Password_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Builder();
        var validate = new RegisterUserValidator();
        request.Password = string.Empty;
        var result = await validate.ValidateAsync(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
    }

    [Fact]

    public async Task Email_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Builder();
        var validate = new RegisterUserValidator();
        request.Email = string.Empty;
        var result = await validate.ValidateAsync(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
    }

    [Fact]
    public async Task Email_Invalid()
    {
        var request = RequestRegisterUserJsonBuilder.Builder();
        var validate = new RegisterUserValidator();
        request.Email = "Andrey";
        var result = await validate.ValidateAsync(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
    }



}
