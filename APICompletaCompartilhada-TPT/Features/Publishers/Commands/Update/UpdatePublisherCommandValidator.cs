// BRANCH: AULA
using FluentValidation;

namespace Univali.Api.Features.Publishers.Commands.UpdatePublisher;

public class UpdatePublisherCommandValidator :
    AbstractValidator<UpdatePublisherCommand>
 {
    public UpdatePublisherCommandValidator() 
    {
        RuleFor(c => c.PublisherId)
        .NotEmpty()
        .WithMessage("Fill a Id")      
        .GreaterThan(0)
        .WithMessage("The PublisherId must be greater than zero.");

        RuleFor(c => c.CNPJ)
        .NotEmpty()
        .WithMessage("Fill out a CNPJ")
        .Length(14)
        .WithMessage("CNPJ must have 14 characters")
        .Must(ValidateCNPJ)
        .When(c => !string.IsNullOrEmpty(c.CNPJ))
        .WithMessage("CNPJ must be valid");
    }

    private bool ValidateCNPJ(string cnpj) // GPT
    {
        cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
        if (cnpj.Length != 14) {
            return false;
        }
        int[] multiplicators1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicators2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int sum = 0;
        for (int i = 0; i < 12; i++) {
            sum += int.Parse(cnpj[i].ToString()) * multiplicators1[i];
        }
        int remainder = sum % 11;
        int verificationDigit1 = remainder < 2 ? 0 : 11 - remainder;
        if (int.Parse(cnpj[12].ToString()) != verificationDigit1) {
            return false;
        }
        sum = 0;
        for (int i = 0; i < 13; i++) {
            sum += int.Parse(cnpj[i].ToString()) * multiplicators2[i];
        }
        remainder = sum % 11;
        int verificationDigit2 = remainder < 2 ? 0 : 11 - remainder;
        if (int.Parse(cnpj[13].ToString()) != verificationDigit2) {
            return false;
        }
        return true;
    }
 }