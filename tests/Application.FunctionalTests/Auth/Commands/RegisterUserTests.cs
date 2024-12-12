using Application.Auth.Commands.RegisterUser;
using FluentValidation.TestHelper;

namespace Application.FunctionalTests.Auth.Commands;

public class RegisterUserTests
{
    private readonly RegisterUserCommandValidator _validator = new();

    [Fact]
    public async Task RegisterUserCommand_WithValidInputs_ShouldPassValidation()
    {
        // Arrange
        var command = new RegisterUserCommand("email@email.email", "username", "password");
        
        // Act
        var result = await _validator.TestValidateAsync(command);
        
        // Arrange
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.Username);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public async Task RegisterUserCommand_WithEmptyEmail_ShouldFailValidation()
    {
        // Arrange
        var command = new RegisterUserCommand("", "username", "password");

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Fact]
    public async Task RegisterUserCommand_WithEmptyUsername_ShouldFailValidation()
    {
        // Arrange
        var command = new RegisterUserCommand("email@email.email", "", "password");

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }
    
    [Fact]
    public async Task RegisterUserCommand_WithEmptyPassword_ShouldFailValidation()
    {
        // Arrange
        var command = new RegisterUserCommand("email@email.email", "username", "");

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
}