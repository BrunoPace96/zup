using System.Text.RegularExpressions;
using FluentValidation;
using ZupTeste.Core;
using ZupTeste.OperationResult.Implementations;

namespace ZupTeste.DomainValidation.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeValueObject<T, TValueObject>(
            this IRuleBuilder<T, string> ruleBuilder,
            Func<string, ValueObjectResult<TValueObject>> factoryMethod)
            where TValueObject : ValueObject
        {
            return (IRuleBuilderOptions<T, string>)ruleBuilder.Custom((value, context) =>
            {
                var result = factoryMethod(value);

                if (result.Failure)
                    result.Errors.ForEach(context.AddFailure);
            });
        }
        
        public static IRuleBuilderOptions<T, string> RequiredWithMessage<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull()
                .WithMessage("O campo {PropertyName} não pode ser nulo")
                .NotEmpty()
                .WithMessage("O campo {PropertyName} não pode ser vazio");
        }
        
        public static IRuleBuilderOptions<T, string> LengthWithMessage<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max)
        {
            return ruleBuilder
                .Length(min, max)
                .WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
        }

        public static IRuleBuilderOptions<T, string> MaximumLengthWithMessage<T>(this IRuleBuilder<T, string> ruleBuilder, int max)
        {
            return ruleBuilder
                .MaximumLength(max)
                .WithMessage("O campo {PropertyName} não pode ter mais de {MaxLength} caracteres");
        }
        
        public static IRuleBuilderOptionsConditions<T, string> IsValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .LengthWithMessage(8, 128)
                .Custom((value, ctx) =>
                {
                    // Lowercase
                    if (!Regex.IsMatch(value, @"[a-z]"))
                        ctx.AddFailure(ctx.PropertyName,
                            $"O campo {ctx.PropertyName} deve ter pelo menos uma letra minúscula");

                    // Digits
                    if (!Regex.IsMatch(value, @"[0-9]"))
                        ctx.AddFailure(ctx.PropertyName, 
                            $"O campo {ctx.PropertyName} deve ter pelo menos um número");

                    // Symbols
                    if (!Regex.IsMatch(value, @"[!@#$%^&*\(\)_\+\-\={}<>,\.\|""'~`:;\\?\/\[\] ]"))
                        ctx.AddFailure(ctx.PropertyName, 
                            $"O campo {ctx.PropertyName} deve ter pelo menos um caracter especial");
                });
        }
    }
}