namespace Presentation.Api.V1;

public sealed record UpdateUserDto(
    string? Email, 
    string? Username, 
    string? Password);