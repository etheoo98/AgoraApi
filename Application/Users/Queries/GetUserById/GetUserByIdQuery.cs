using Application.Common.Models;
using Ardalis.Result;
using MediatR;

namespace Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(int Id) : IRequest<Result<UserDto>>;