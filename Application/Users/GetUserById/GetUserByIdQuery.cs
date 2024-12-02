using Ardalis.Result;
using MediatR;

namespace Application.Users.GetUserById;

public sealed record GetUserByIdQuery(int Id) : IRequest<Result<UserResponse>>;