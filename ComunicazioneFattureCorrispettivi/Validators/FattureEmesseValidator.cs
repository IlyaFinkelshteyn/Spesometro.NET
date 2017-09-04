﻿using FluentValidation;

namespace ComunicazioneFattureCorrispettivi.Validators
{
    public class FattureEmesseValidator : AbstractValidator<FattureEmesse.FattureEmesse>
    {
        public FattureEmesseValidator()
        {
            RuleFor(x => x.CedentePrestatore)
                .SetValidator(new CedenteCessionarioValidator());
            RuleFor(x => x.CessionarioCommittente)
                .SetCollectionValidator(new CedenteCessionarioDatiFatturaBodyValidator())
                .NotEmpty();
            RuleFor(x => x.CessionarioCommittente)
                .Must(items => items.Count >= 1 && items.Count <= 1000);
            RuleFor(x => x.Rettifica.IdFile)
                .Must((fattureEmesse, _) => fattureEmesse.CessionarioCommittente.Count == 1 && fattureEmesse.CessionarioCommittente[0].DatiFatturaBody.Count == 1)
                .When(x => !x.IsEmpty())
                .WithErrorCode("00447")
                .WithMessage("Non ammesso piu di un documento in caso di rettifica");
        }
    }
}
