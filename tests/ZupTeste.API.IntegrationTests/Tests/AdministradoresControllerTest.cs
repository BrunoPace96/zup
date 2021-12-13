using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZupTeste.API.Authentication.Contracts;
using ZupTeste.Domain.Administradores.Read.ObterAdministradorPorEmailSenha;
using ZupTeste.Domain.Funcionarios.Write.CriarFuncionario;

namespace ZupTeste.API.IntegrationTests.Tests;

public class AdministradoresControllerTest : BaseHttpTest
{
    public AdministradoresControllerTest(CustomWebApplicationFactory factory, ITestOutputHelper output) : base(factory, output)
    {

    }
    
    [Fact]
    public async Task Autenticar_Administrador_Padrao()
    {
        var body = new ObterAdministradorPorEmailSenhaQuery("admin@admin.com", "admin");

        var data = await HttpPostAsync<RespostaToken>(
            "api/administradores/autenticar", 
            body, 
            statusCode: HttpStatusCode.OK);

        Assert.NotNull(data);
        Assert.NotNull(data.AccessToken);
        Assert.NotEmpty(data.AccessToken);
    }
    
    [Fact]
    public async Task Autenticar_Credenciais_Invalidas()
    {
        var body = new ObterAdministradorPorEmailSenhaQuery("admin@admin.com", "senha_invalida");

        await HttpPostAsync<RespostaToken>(
            "api/administradores/autenticar", 
            body, 
            statusCode: HttpStatusCode.BadRequest);
    }
}