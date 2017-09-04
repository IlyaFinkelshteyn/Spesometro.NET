﻿using ComunicazioneFattureCorrispettivi.Common;
using FluentValidation;

namespace ComunicazioneFattureCorrispettivi.Validators
{
    public class DatiFatturaBodyValidator : AbstractValidator<DatiFatturaBody>
    {
        public DatiFatturaBodyValidator()
        {
            RuleFor(x => x.DatiGenerali)
                .SetValidator(new DatiGeneraliValidator());
            RuleForEach(x => x.DatiRiepilogo)
                .Must((body, riepilogo) => riepilogo.DatiIVA.Imposta > 0)
                .When(body => body.DatiGenerali.TipoDocumento != "TD07" && body.DatiGenerali.TipoDocumento != "TD08")
                .WithErrorCode("00433")
                .WithMessage("<Imposta> o <Aliquota> non presente a fronte di  <TipoDocumento> uguale a TD01, TD04  o TD05");
            RuleForEach(x => x.DatiRiepilogo)
                .Must(riepilogo => riepilogo.DatiIVA.Aliquota * riepilogo.ImponibileImporto == 0)
                .When(riepilogo => riepilogo.DatiGenerali.TipoDocumento != "TD07" && riepilogo.DatiGenerali.TipoDocumento != "TD08")
                .WithErrorCode("00433")
                .WithMessage("<Imposta> o <Aliquota> non presente a fronte di <TipoDocumento> uguale a TD01, TD04  o TD05");
            RuleForEach(x => x.DatiRiepilogo)
                .Must((body, riepilogo) => riepilogo.DatiIVA.Aliquota > 0)
                .When(body => body.DatiGenerali.TipoDocumento != "TD07" && body.DatiGenerali.TipoDocumento != "TD08")
                .WithErrorCode("00433")
                .WithMessage("<Imposta> o <Aliquota> non presente a fronte di <TipoDocumento> uguale a TD01, TD04 o TD05");
            RuleFor(x => x.DatiRiepilogo)
                .Must(items => items.Count >= 1 && items.Count <= 1000);

        }
    }
}
