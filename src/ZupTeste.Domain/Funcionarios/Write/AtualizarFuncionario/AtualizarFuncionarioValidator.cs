using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ZupTeste.DomainValidation.Extensions;
using ZupTeste.Repository.Repository;

namespace ZupTeste.Domain.Funcionarios.Write.AtualizarFuncionario;

public class AtualizarFuncionarioValidator : AbstractValidator<AtualizarFuncionarioCommand>
{
    public AtualizarFuncionarioValidator(IReadOnlyRepository<Funcionario> repository)
    {
        RuleFor(x => x)
            .CustomAsync(async (command, context, cancellationToken) =>
            {
                if (await repository
                        .GetQuery()
                        .AsNoTracking()
                        .AnyAsync(x => x.NumeroChapa == command.NumeroChapa && x.Id != command.Id, cancellationToken))
                    context.AddFailure(nameof(AtualizarFuncionarioCommand.NumeroChapa), "Número de chapa já existente");
                
                if (await repository
                        .GetQuery()
                        .AsNoTracking()
                        .AnyAsync(x => x.Email == command.Email && x.Id != command.Id, cancellationToken))
                    context.AddFailure(nameof(AtualizarFuncionarioCommand.Email), "Já existe um funcionário com esse email cadastrado");
            });
        
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