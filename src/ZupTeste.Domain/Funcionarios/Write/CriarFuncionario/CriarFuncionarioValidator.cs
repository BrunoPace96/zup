using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ZupTeste.DomainValidation.Extensions;
using ZupTeste.Repository.Repository;

namespace ZupTeste.Domain.Funcionarios.Write.CriarFuncionario;

public class CriarFuncionarValidator : AbstractValidator<CriarFuncionarioCommand>
{
    public CriarFuncionarValidator(IReadOnlyRepository<Funcionario> repository)
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
            .RequiredWithMessage()
            .IsValidPassword();

        RuleFor(x => x.NumeroChapa)
            .RequiredWithMessage()
            .MaximumLengthWithMessage(30)
            .CustomAsync(async (numeroChapa, context, cancellationToken) =>
            {
                if (await repository
                        .GetQuery()
                        .AsNoTracking()
                        .AnyAsync(x => x.NumeroChapa == numeroChapa, cancellationToken))
                    context.AddFailure(nameof(CriarFuncionarioCommand.NumeroChapa), "Número de chapa já existente");
            });
            
        RuleFor(x => x.LiderEmail)
            .EmailAddress()
            .WithMessage("O email informado no campo {PropertyName} é inválido");
    }
}