using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using ZupTeste.API.IntegrationTests.Common;
using ZupTeste.API.IntegrationTests.Generator;
using ZupTeste.Core.Utils;
using ZupTeste.DataContracts.Results;
using ZupTeste.Domain.Funcionarios;
using ZupTeste.Domain.Funcionarios.Read.ObterFuncionarioPeloId;
using ZupTeste.Domain.Funcionarios.Read.ObterListaFuncionarios;
using ZupTeste.Domain.Funcionarios.Write.AtualizarFuncionario;
using ZupTeste.Domain.Funcionarios.Write.CriarFuncionario;
using ZupTeste.Repository.Repository;

namespace ZupTeste.API.IntegrationTests.Tests;

public class FuncionarioControllerTest : BaseHttpTest
{
    
    private readonly IReadOnlyRepository<Funcionario> _readOnlyRepository;
    private readonly FuncionarioGenerator _generator;

    public FuncionarioControllerTest(CustomWebApplicationFactory factory, ITestOutputHelper output) : base(factory, output)
    {
        _readOnlyRepository = factory.ServiceProvider.GetService<IReadOnlyRepository<Funcionario>>();
        _generator = factory.ServiceProvider.GetService<FuncionarioGenerator>();
    }
    
    [Fact]
    public async Task Criar_Funcionario()
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

        var data = await HttpPostAsync<CriarFuncionarioResult>("api/funcionarios/", body);

        Assert.NotNull(data);
        Assert.NotEqual(Guid.Empty, data.Id);

        var funcionarioDatabase = await _readOnlyRepository.FirstOrDefaultAsync(data.Id);
        
        Assert.NotNull(funcionarioDatabase);
        Assert.NotEqual("1@aaaBBB", funcionarioDatabase.Senha);
    }
    
    [Fact]
    public async Task Criar_Funcionario_Com_Lider()
    {
        var lider = await _generator
            .GenerateAndSaveAsync();

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
            o.LiderEmail = lider.Email;
        }).Generate();
        
        var data = await HttpPostAsync<CriarFuncionarioResult>("api/funcionarios/", body);

        Assert.NotNull(data);
        Assert.NotEqual(Guid.Empty, data.Id);
        Assert.Equal(lider.Id, data.LiderId);

        var funcionarioDatabase = await _readOnlyRepository.FirstOrDefaultAsync(data.Id);
        
        Assert.NotNull(funcionarioDatabase);
        Assert.Equal(lider.Id, funcionarioDatabase.LiderId);
    }
    
    [Fact]
    public async Task Listar_Funcionarios()
    {
        await _generator.GenerateAndSaveAsync(10);
        
        var data = await HttpGetAsync<PaginatedResult<ObterListaFuncionariosResult>>("api/funcionarios?pageSize=2");
        
        Assert.NotNull(data);
        Assert.Equal(2, data.Items.Count);
        Assert.True(data.TotalItems >= 10);
    }
    
    [Fact]
    public async Task Obter_Funcionario_Por_Id()
    {
        var funcionario = await _generator.GenerateAndSaveAsync();
        
        var data = await HttpGetAsync<ObterFuncionarioPeloIdResult>($"api/funcionarios/{funcionario.Id}");
        
        Assert.NotNull(data);
        Assert.Equal(funcionario.Id, data.Id);
        Assert.Equal(funcionario.Nome, data.Nome);
        Assert.Equal(funcionario.Sobrenome, data.Sobrenome);
        Assert.Equal(funcionario.NumeroChapa, data.NumeroChapa);
        
        Assert.All(funcionario.Telefones, x => Assert.Contains(data.Telefones, c => c.UnMask() == x.Numero.UnMask()));
    }
    
    [Fact]
    public async Task Obter_Funcionario_Por_Id_Com_Lider()
    {
        var lider = await _generator.GenerateAndSaveAsync();
        
        var funcionario = await _generator
            .WithRule(x => x.LiderId, lider.Id)
            .GenerateAndSaveAsync();
        
        var data = await HttpGetAsync<ObterFuncionarioPeloIdResult>($"api/funcionarios/{funcionario.Id}");
        
        Assert.NotNull(data);
        Assert.Equal(funcionario.Id, data.Id);
        Assert.NotNull(data.Lider);
        Assert.Equal(lider.Id, data.Lider.Id);
    }
    
    [Fact]
    public async Task Atualizar_Funcionario()
    {
        var lider = await _generator.GenerateAndSaveAsync();
        var funcionario = await _generator.GenerateAndSaveAsync();
        
        var body = new Faker<AtualizarFuncionarioCommand>(LocaleConstants.Locale).Rules((f, o) =>
        {
            o.Id = funcionario.Id;
            o.Nome = f.Person.FirstName;
            o.Sobrenome = f.Person.LastName;
            o.Email = f.Person.Email;
            o.NumeroChapa = f.Random.Number(100000, 99999999).ToString();
            o.Telefones = new List<string>
            {
                f.Phone.PhoneNumber(),
                f.Phone.PhoneNumber()
            };
            o.LiderEmail = lider.Email;
        }).Generate();

        var data = await HttpPutAsync<AtualizarFuncionarioResult>("api/funcionarios/", body);

        Assert.NotNull(data);
        Assert.NotEqual(Guid.Empty, data.Id);

        var funcionarioDatabase = await _readOnlyRepository
            .GetQuery()
            .AsNoTracking()
            .Include(x => x.Telefones)
            .FirstOrDefaultAsync(x => x.Id == data.Id);
        
        Assert.NotNull(funcionarioDatabase);
        Assert.Equal(funcionario.Id, data.Id);
        Assert.NotEqual(funcionario.Nome, funcionarioDatabase.Nome);
        Assert.NotEqual(funcionario.Sobrenome, funcionarioDatabase.Sobrenome);
        Assert.NotEqual(funcionario.NumeroChapa, funcionarioDatabase.NumeroChapa);

        var telefonesAntigos = funcionario.Telefones.Select(x => x.Numero.UnMask()).ToList();
        foreach (var telefone in funcionarioDatabase.Telefones)
        {
            Assert.DoesNotContain(telefone.Numero.UnMask(), telefonesAntigos);
        }
    }
}