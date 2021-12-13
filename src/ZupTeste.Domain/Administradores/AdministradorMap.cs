using AutoMapper;
using ZupTeste.DataContracts.Results;
using ZupTeste.Domain.Administradores.Read.ObterAdministradorPorEmailSenha;
using ZupTeste.Domain.Funcionarios.Read;
using ZupTeste.Domain.Funcionarios.Read.ObterFuncionarioPeloId;
using ZupTeste.Domain.Funcionarios.Read.ObterListaFuncionarios;
using ZupTeste.Domain.Funcionarios.Write;
using ZupTeste.Domain.Funcionarios.Write.AtualizarFuncionario;
using ZupTeste.Domain.Funcionarios.Write.CriarFuncionario;

namespace ZupTeste.Domain.Administradores
{
    public class AdministradorMap : Profile
    {
        public AdministradorMap()
        {
            CreateMap<Administrador, ObterAdministradorPorEmailSenhaResult>();
        }
    }
}