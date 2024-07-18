
# MicroService CRUD User

Este projeto é um microserviço de CRUD de usuários implementado em .NET 6, com suporte a Docker e MySQL.

## Funcionalidades

- CRUD de usuários
- Geração de token seguro
- Troca de senha (sem envio de e-mail)
- Logs e Observabilidade
- API REST
- Cobertura de testes unitários acima de 80%
- Uso de banco de dados MySQL

## Pré-requisitos

- [Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/community/)
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

## Configuração do Projeto

### Clonar o Repositório

```bash
git clone https://github.com/RafaelMenezess/MicroServiceCrudUser.git
cd MicroServiceCrudUser
```

### Configurar o Banco de Dados

Certifique-se de que o Docker Desktop está rodando e execute o seguinte comando para iniciar o banco de dados MySQL e o microserviço:

```bash
docker-compose up --build
```

### Atualizar o Banco de Dados

Para aplicar as migrations e atualizar o banco de dados, use o Package Manager Console no Visual Studio:

1. Abra o Visual Studio Community 2022.
2. Configurar a String de Conexão no appsettings.json
3. Vá para `View > Other Windows > Package Manager Console`.
4. No console, execute os comandos:

```powershell
Update-Database
```

### Rodar a Aplicação

1. Abra a solução `MicroServiceCrudUser.sln` no Visual Studio.
2. Configure o projeto `MicroServiceCrudUser` como projeto de inicialização.
3. Pressione `F5` para rodar a aplicação.

## Testes

Para rodar os testes unitários, siga os passos abaixo:

1. Abra a solução `MicroServiceCrudUser.sln` no Visual Studio.
2. Vá para `Test > Test Explorer`.
3. Clique em `Run All` para rodar todos os testes.

## Logs e Observabilidade

Os logs da aplicação são gerenciados pelo pacote de observabilidade configurado. Verifique os logs usando o seguinte comando:

```bash
docker logs <nome-ou-id-do-container-microservice-crud-user>
```

## Contribuições

Sinta-se à vontade para contribuir com este projeto. Por favor, abra um "pull request" com suas alterações.

