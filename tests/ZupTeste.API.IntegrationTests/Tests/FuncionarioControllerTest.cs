using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using ZupTeste.API.IntegrationTests.Common;
using ZupTeste.Domain.Funcionarios;
using ZupTeste.Domain.Funcionarios.Write;
using ZupTeste.Repository.Repository;

namespace ZupTeste.API.IntegrationTests.Tests;

public class FuncionarioControllerTest : BaseHttpTest
{
    
    private readonly IReadOnlyRepository<Funcionario> _readOnlyRepository;

    public FuncionarioControllerTest(CustomWebApplicationFactory factory, ITestOutputHelper output) : base(factory, output)
    {
        _readOnlyRepository = factory.ServiceProvider.GetService<IReadOnlyRepository<Funcionario>>();
    }
    
    [Fact]
    public async Task CriarFuncionario()
    {
        var data = await CriarFuncionarioAleatorio();

        Assert.NotNull(data);
        Assert.NotEqual(Guid.Empty, data.Id);

        var funcionarioDatabase = await _readOnlyRepository.FirstOrDefaultAsync(data.Id);
        
        Assert.NotNull(funcionarioDatabase);
        Assert.NotEqual("1@aaaBBB", funcionarioDatabase.Senha);
    }
    
    private async Task<CriarFuncionarioResult> CriarFuncionarioAleatorio()
    {
        var body = new Faker<CriarFuncionarioCommand>(LocaleConstants.Locale).Rules((f, o) =>
        {
            o.Nome = f.Person.FirstName;
            o.Sobrenome = f.Person.LastName;
            o.Email = f.Person.Email;
            o.NumeroChapa = f.Random.Number(100000, 99999999).ToString();
            o.Senha = "1@aaaBBB";
            o.Telefones = new List<string>
            {
                f.Phone.PhoneNumber(),
                f.Phone.PhoneNumber()
            };
        }).Generate();

        var data = await HttpPostAsync<CriarFuncionarioResult>($"api/funcionarios/", body);

        return data;
    }
}