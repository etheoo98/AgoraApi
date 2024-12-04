using Application.Common.Interfaces;
using Application.Common.Models;
using Ardalis.Result;
using MediatR;

namespace Application.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(int Id) : IRequest<Result>, IHasId;