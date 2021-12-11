using AutoMapper;
using ZupTeste.DataContracts.Results;
using ZupTeste.Domain.Funcionarios.Read;
using ZupTeste.Domain.Funcionarios.Read.ObterListaFuncionarios;
using ZupTeste.Domain.Funcionarios.Write;
using ZupTeste.Domain.Funcionarios.Write.CriarFuncionario;

namespace ZupTeste.Domain.Funcionarios
{
    public class FuncionarioMap : Profile
    {
        public FuncionarioMap()
        {
            CreateMap<CriarFuncionarioCommand, Funcionario>()
                .ForMember(x => x.Telefones,
                    x => x.MapFrom(mp => mp.Telefones.Select(t => new Telefone { Numero = t })));

            CreateMap<Funcionario, CriarFuncionarioResult>();
            
            CreateMap<Funcionario, ObterListaFuncionariosResult>()
                .ForMember(x => x.Telefones, x => x.MapFrom(mp => mp.Telefones.Select(s => s.Numero)));
            
            CreateMap<PaginatedResult<Funcionario>, PaginatedResult<ObterListaFuncionariosResult>>();
        }
    }
}