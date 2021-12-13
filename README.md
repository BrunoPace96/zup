# Teste Zup

Projeto de teste do processo seletivo da Zup

API desenvolvida com .NET 6.0 utilizando banco de dados Postgresql.

### Executar o projeto com docker


1. Execute o comando para rodar o banco de dados:

   ```
   docker-compose -f .\docker-compose.yml up database -d --build
    ```
2. Execute o comando para rodar a API
    ```
   docker-compose -f .\docker-compose.yml up zupteste.api -d --build
   ```

3. O projeto estará acessivel em https://localhost:5001/swagger/index.html


### Configurar HTTPS para o docker em localhost no windows

1. Abra o powershell e rode os seguintes comandos:
   1. ``dotnet dev-certs https --clean``
   2. ``dotnet dev-certs https --trust -ep``
   3. ``dotnet dev-certs https --trust -ep $env:USERPROFILE\.aspnet\https\aspnetapp.pfx -p DEVCERTPASSWORD``


### Documentação
Collection do postman com as requisições da API disponível na raiz do projeto ou pelo link https://www.postman.com/red-moon-7691/workspace/zup-teste/request/1213114-65f90e1d-7abd-4974-9a87-f035dd82a1a2

Documentação dos endpoints disponível em `/swagger/index.html`


### Arquitetura
Para a arquitetura da aplicação foi escolhida a **Arquitetura Limpa** com o domínio da aplicação no centro para as entidades, casos de uso, abstrações e alguns recursos comuns ao domínio. 

O projeto ZupTeste.Infra lida com os repositórios, providers externos, configurações da aplicação e injeção de dependência.

O projeto ZupTeste.API é a camada de portas e adaptadores da arquitetura limpa, ela lida com a entrada dos eventos externos para os casos de uso.

### CQRS

É utilizado o pattern **CQRS** (Command Query Responsibility Segregation) para separar a responsabilidade de escrita e leitura dos dados, com os **commands** e **queries**

### Pacotes

- **Automapper** para realizar o mapeamentos dos objetos, principalmente entre, commands, results e entidades.
- **MediatR** para desacoplar commands dos handlers e adicionar pipelines como o `FailFastBehavior` que valida as propriedades do command antes dela chegar no handler
- **Bogus** para gerar dados fakes para os testes
- **Microsoft.AspNetCore.Mvc.Testing** e **Microsoft.EntityFrameworkCore.InMemory** para os testes de integração
- E outros pacotes da Microsoft que fazem parte do ecossistema .NET


### Testes
O projeto `Zup.Teste.API.IntegrationTests` cuida dos testes de integração, a aplicação e o banco de dados são executados em memória e todos os endpoints da API são testados com requisições para esse servidor em memória

### Alguns dos patterns e princípios utilizados no projeto
   
   - SOLID
   - Unit of work
   - Domain Validation
   - Domain Events
   - Aggregates
   - Fail-Fast Principle
   - Mediator
   - Repository Pattern






#### Adicionar novas migrations
```
cd .\src\ZupTeste.Infra\
dotnet ef -s ../ZupTeste.API -v migrations add [MIGRATION] -c DatabaseContext -o "Data/Migrations"
```