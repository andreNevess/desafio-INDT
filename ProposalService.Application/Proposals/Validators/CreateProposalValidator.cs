using FluentValidation;
using ProposalService.Application.Proposals.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Application.Proposals.Validators
{
    public sealed class CreateProposalValidator : AbstractValidator<CreateProposalRequest>
    {
        public CreateProposalValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Premium).GreaterThan(0);
        }
    }
}
