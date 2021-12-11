using FluentValidation;
using ZupTeste.DomainValidation.Extensions;

namespace ZupTeste.Domain.Funcionarios.Write.CriarFuncionario;

public class CriarFuncionarValidator : AbstractValidator<CriarFuncionarioCommand>
{
    public CriarFuncionarValidator()
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
            
        RuleFor(x => x.Senha)
            .RequiredWithMessage();
            
        RuleFor(x => x.Senha)
            .RequiredWithMessage()
            .IsValidPassword();

        RuleFor(x => x.NumeroChapa)
            .RequiredWithMessage()
            .MaximumLengthWithMessage(30);
            
        RuleFor(x => x.LiderEmail)
            .EmailAddress()
            .WithMessage("O email informado no campo {PropertyName} é inválido");
    }
}