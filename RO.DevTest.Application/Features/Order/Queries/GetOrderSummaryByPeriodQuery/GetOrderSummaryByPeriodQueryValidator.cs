
using FluentValidation;

namespace RO.DevTest.Application.Features.Order.Queries.GetOrderSummaryByPeriodQuery;

public class GetOrderSummaryByPeriodQueryValidator : AbstractValidator<GetOrderSummaryByPeriodQuery> {

    public GetOrderSummaryByPeriodQueryValidator() {
        RuleFor(o => o.StartDate)
            .NotNull()
            .WithMessage("A data inicio não pode ser em vazia");
        
        RuleFor(o => o.EndDate)
            .NotNull()
            .WithMessage("A data fim não pode ser em vazia");
    }
}