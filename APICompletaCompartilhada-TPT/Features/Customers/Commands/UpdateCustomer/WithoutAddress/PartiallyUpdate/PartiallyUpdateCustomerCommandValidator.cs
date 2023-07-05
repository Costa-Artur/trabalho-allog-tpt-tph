using FluentValidation;
namespace Univali.Api.Features.Customers.Commands.PartiallyUpdateCustomer;

public class PartiallyUpdateCustomerCommandValidator : 
    AbstractValidator<PartiallyUpdateCustomerDto>
{
    public PartiallyUpdateCustomerCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty() //Não pode ser um valor vazio ou null.
            .WithMessage("You should fill out a Name") //Caso seja vazio ou nulo aparece a mensagem.
            .MaximumLength(100) //Máximo de caracteres permitido
            .WithMessage("The {PropertyName} shouldn't have more than 50 characteres"); //Caso tenha mais de 50 aparece essa mensagem

        RuleFor(c => c.Cpf)
            .NotEmpty() //Não pode ser um valor vazio ou null.
            .WithMessage("You sould fill out a CPF") //Caso seja vazio ou nulo aparece a mensagem.
            .Length(11) //O tamanho deve ser 11. 
            .WithMessage("The CPF should have 11 characters") //Caso não seja 11, aparecerá essa mensagem.
            .Must(ValidateCPF) //Validação do Cpf.
            .When(c => c.Cpf != null, ApplyConditionTo.CurrentValidator) 
            .WithMessage("The CPf should be valid number"); //Caso o CPF não seja válido, aparecerá essa mensagem
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