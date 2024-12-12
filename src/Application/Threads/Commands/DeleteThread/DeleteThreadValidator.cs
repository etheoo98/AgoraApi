using Application.Common.Validators;
using FluentValidation;

namespace Application.Threads.Commands.DeleteThread;

public class DeleteThreadValidator : IdValidator<DeleteThreadCommand>;