using AutoMapper;
using ZupTeste.Domain.Funcionarios.Write;

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
        }
    }
}