namespace Presentation.Api.V1;

public sealed record LoginDto(
    string? Email,
    string? Username,
    string Password);