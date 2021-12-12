using FluentValidation;
using ZupTeste.DomainValidation.Extensions;

namespace ZupTeste.Domain.Funcionarios.Write.AtualizarFuncionario;

public class AtualizarFuncionarioValidator : AbstractValidator<AtualizarFuncionarioCommand>
{
    public AtualizarFuncionarioValidator()
    {
        RuleFor(x => x.Nome)
            .RequiredWithMessage()
            .MaximumLengthWithMessage(128);

        RuleFor(x => x.Sobrenome)
            .RequiredWithMessage()
            .MaximumLengthWithMessage(256);
            
        RuleFor(x => x.Email)
            .RequiredWithMessage()
            .MaximumLengthWithMessage(256);

        RuleFor(x => x.NumeroChapa)
            .RequiredWithMessage()
            .MaximumLengthWithMessage(30);
            
        RuleFor(x => x.LiderEmail)
            .EmailAddress()
            .WithMessage("O email informado no campo {PropertyName} é inválido");
    }
}