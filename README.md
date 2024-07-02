# Base de dados de Animes

Esta API foi implementada em .NET Core 8.0 seguindo os princípios da Clean Architecture. Sua função é realizar um CRUD de animes. 

O projeto está dividido em seis camadas:

# Application

A camada Application contém os Services e suas Interfaces, além dos DTOs para autenticação.

# Domain

A camada Domain contém as Entidades do projeto e as Interfaces dos Repositórios.

# Infra.Data

A camada Infra.Data realiza a configuração das tabelas no banco de dados por meio do arquivo de contexto, e contém os Repositórios.

# Infrastructure.IoC

A camada Infrastructure.IoC realiza a injeção de dependência para fazer as conexões entre as interfaces e as implementações.

# Presentation

A camada de apresentação contém os Controladores e a classe principal, Program, que é usada para executar o projeto.

# Tests

A camada de testes contém duas classes, uma para testar os métodos de CRUD dos animes e outra para testar os métodos de autenticação.

Os testes unitários foram feitos utilizando Xunit.

# Funções CRUD

É possível cadastrar um anime usando Nome, Diretor e Resumo, além de realizar consulta por todos os animes cadastrados ou utilizando filtros.

Também é possível atualizar e deletar logicamente o anime.

A exclusão é feita marcando um anime como Deletado. As listagens retornam apenas animes cuja flag Deletado esteja marcada como 'false'.

# Autenticação

Os endpoints estão protegidos por autenticação via JWT. É necessário cadastrar um usuário, fazer o login e informar o token recebido no Swagger no formato "Bearer 'token'" para utilizar os métodos.

# Logs

Cada execução de uma função do CRUD registra um log no console.
