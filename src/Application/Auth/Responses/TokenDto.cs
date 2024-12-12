namespace Application.Auth.Responses;

public sealed record TokenDto(string AccessToken, string RefreshToken);