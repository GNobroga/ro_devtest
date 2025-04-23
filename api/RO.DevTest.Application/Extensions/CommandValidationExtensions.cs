using FluentValidation;
using MediatR;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Extensions;

public static class CommandValidationExtensions {

    public static async Task ThrowIfInvalidCommandAsync<T>(this IBaseRequest command, AbstractValidator<T> validator)  {
        var validationResult = await validator.ValidateAsync((T) command) ;
        if (validationResult.IsValid) return;
        throw new BadRequestException(validationResult);
    }
}