using System.Collections.Generic;
using System.Reflection.Metadata;
using Bogus;
using ZupTeste.API.IntegrationTests.Common;
using ZupTeste.Domain.Funcionarios;
using ZupTeste.Repository.Repository;
using ZupTeste.Repository.UnitOfWork.Factories;

namespace ZupTeste.API.IntegrationTests.Generator
{
    public class FuncionarioGenerator : BaseGenerator<Funcionario>
    {
        public FuncionarioGenerator(IUnitOfWorkScopeFactory unitOfWork, IRepository<Funcionario> repository) 
            : base(unitOfWork, repository)
        {
            Rules = new Faker<Funcionario>(LocaleConstants.Locale)
                .Rules((f, o) =>
                {
                    o.Nome = f.Person.FirstName;
                    o.Sobrenome = f.Person.LastName;
                    o.Email = f.Person.Email;
                    o.NumeroChapa = f.Random.Number(100000, 99999999).ToString();
                    o.Senha = "1@aaaBBB";
                    o.Telefones = new List<Telefone>
                    {
                        new Telefone
                        {
                            Numero = f.Phone.PhoneNumber()
                        }
                    };
                });
        }
    }
}
