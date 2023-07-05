// BRANCH: AULA
using FluentValidation;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator :
    AbstractValidator<UpdateCustomerCommand>
 {
    public UpdateCustomerCommandValidator() {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Fill a name")
            .MaximumLength(100)
            .WithMessage("The {propertyName} should not have more than 100 characters");

        RuleFor(c => c.Cpf)
            //.Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Fill out a CPF")
            .Length(11)
            .WithMessage("CPF must have more than 11 characters")
            .Must(ValidateCPF)
            .When(c => c.Cpf != null, ApplyConditionTo.CurrentValidator)
            .WithMessage("CPF must be valid");

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