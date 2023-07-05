using FluentValidation;
using Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddress;

namespace Univali.Api.Features.Addresses.Commands.UpdateCustomerWithAddress;

public class UpdateCustomerWithAddressCommandValidator : 
    AbstractValidator<UpdateCustomerWithAddressCommand>
{
    public UpdateCustomerWithAddressCommandValidator()
    {
        RuleFor(a => a.CustomerId)
            .NotEmpty()
            .WithMessage("The {PropertyName} cannot be null or empty");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("You should fill out a Name")
            .MaximumLength(100)
            .WithMessage("The {PropertyName} shouldn't have more than 100 characteres");

        RuleFor(c => c.Cpf)
            .NotEmpty()
            .WithMessage("You should fill out a CPF")
            .Length(11)
            .WithMessage("The CPF should have 11 characters")
            .Must(ValidateCPF)
            .When(c => c.Cpf != null, ApplyConditionTo.CurrentValidator)
            .WithMessage("The CPf should be valid number");

        RuleFor(c => c.Addresses)
            .NotEmpty()
            .WithMessage("Customer must have at least one address")
            .ForEach(addressRule =>
            {
                addressRule.SetValidator(new UpdateCustomerWithAddressValidator());
            })
            .When(c => c.Addresses != null, ApplyConditionTo.CurrentValidator);
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