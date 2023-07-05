// TPT
using FluentValidation;

namespace Univali.Api.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator :
    AbstractValidator<CreateCustomerCommand>
 {
    public CreateCustomerCommandValidator() {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Fill a name")
            .MaximumLength(100)
            .WithMessage("The {propertyName} should not have more than 100 characters");

        RuleFor(c => c.CPF)
            .Cascade(CascadeMode.Stop)
            .Length(11)
            .WithMessage("CPF must have 11 characters")
            .Must(ValidateCPF)
            .When(c => !string.IsNullOrEmpty(c.CPF))
            .WithMessage("CPF must be valid");

        RuleFor(c => c.CNPJ)
            .Cascade(CascadeMode.Stop)
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

    private bool ValidateCPF(string cpf) 
    {
       cpf = cpf.Replace(".", "").Replace("-", "");
       if (cpf.Length != 11) {
           return false;
       }
       bool allDigitsEqual = true;
       for (int i = 1; i < cpf.Length; i++) {
           if (cpf[i] != cpf[0]) {
               allDigitsEqual = false;
               break;
           }
       }
       if (allDigitsEqual) {
           return false;
       }
       int sum = 0;
       for (int i = 0; i < 9; i++) {
           sum += int.Parse(cpf[i].ToString()) * (10 - i);
       }
       int remainder = sum % 11;
       int verificationDigit1 = remainder < 2 ? 0 : 11 - remainder;
       if (int.Parse(cpf[9].ToString()) != verificationDigit1) {
           return false;
       }
       sum = 0;
       for (int i = 0; i < 10; i++) {
           sum += int.Parse(cpf[i].ToString()) * (11 - i);
       }
       remainder = sum % 11;
       int verificationDigit2 = remainder < 2 ? 0 : 11 - remainder;
       if (int.Parse(cpf[10].ToString()) != verificationDigit2) {
           return false;
       }
       return true;
   }

 }