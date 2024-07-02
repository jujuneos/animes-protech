# Base de dados de Animes

Este projeto realiza um CRUD de animes. É possível cadastrar um anime usando Nome, Diretor e Resumo, além de realizar consulta por todos os animes cadastrados ou utilizando filtros.

Também é possível atualizar e deletar logicamente o anime.

A exclusão é feita marcando um anime como Deletado. As listagens retornam apenas animes cuja flag Deletado esteja marcada como 'false'.

Os endpoints estão protegidos por autenticação via JWT. É necessário cadastrar um usuário, fazer o login e informar o token no Swagger para utilizar os métodos.

Cada execução de uma função do CRUD registra um log no console.

Existem testes unitários para todas as operações utilizando Xunit.
