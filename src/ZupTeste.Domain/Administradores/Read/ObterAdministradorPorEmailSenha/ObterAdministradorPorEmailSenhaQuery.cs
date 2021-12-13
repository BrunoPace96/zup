using MediatR;

namespace ZupTeste.Domain.Administradores.Read.ObterAdministradorPorEmailSenha;

public record ObterAdministradorPorEmailSenhaQuery(string Email, string Senha): 
    IRequest<ObterAdministradorPorEmailSenhaResult>;