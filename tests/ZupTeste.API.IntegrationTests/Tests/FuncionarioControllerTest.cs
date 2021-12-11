using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using Xunit;
using Xunit.Abstractions;
using ZupTeste.API.IntegrationTests.Common;
using ZupTeste.Domain.Funcionarios.Write;

namespace ZupTeste.API.IntegrationTests.Tests;

public class FuncionarioControllerTest : BaseHttpTest
{
    public FuncionarioControllerTest(CustomWebApplicationFactory factory, ITestOutputHelper output) : base(factory, output)
    {
    }
    
    [Fact]
    public async Task CriarFuncionario()
    {
        var data = await CriarFuncionarioAleatorio();

        Assert.NotNull(data);
        Assert.NotEqual(Guid.Empty, data.Id);
    }
    
    private async Task<CriarFuncionarioResult> CriarFuncionarioAleatorio()
    {
        var body = new Faker<CriarFuncionarioCommand>(LocaleConstants.Locale).Rules((f, o) =>
        {
            o.Nome = f.Person.FirstName;
            o.Sobrenome = f.Person.LastName;
            o.Email = f.Person.Email;
            o.NumeroChapa = f.Random.Number(100000, 99999999).ToString();
            o.Senha = f.Internet.Password();
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