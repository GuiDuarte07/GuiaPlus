---

# Guia+ Backend

Este é o backend da aplicação Guia+, desenvolvido em .NET. O Guia+ é um sistema para gestão e emissão de guias de transporte para clientes de transportadoras, com funcionalidades como cadastro de clientes e gestão de guias.

## Requisitos

A API desenvolvida para essa aplicação roda através de uma instância de docker gerenciada pelo docker-compose junto com uma instância do SQL server.

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/)

## Como rodar o projeto localmente

Siga as etapas abaixo para clonar e rodar o projeto na sua máquina local:

### 1. Clonar o repositório

```bash
git clone https://github.com/GuiDuarte07/GuiaPlus.git
```

### 2. Entrar na pasta do projeto

```bash
cd guia-plus-backend
```

### 3. Construir e iniciar os containers

```bash
docker-compose up --build
```

### 4. Acessar a aplicação

Após rodar o comando anterior, a aplicação estará disponível em: `https://localhost:8001`, que seria a porta que o front-end usa para acessar a API.

Aqui está a seção "Estrutura do Projeto" atualizada com a sua organização de pastas:

---

## Estrutura do Projeto

O projeto está estruturado seguindo as boas práticas de desenvolvimento em .NET, com as seguintes pastas principais:

- **API**: Contém os controladores da API que gerenciam as requisições.
- **Application**: Contém a lógica de negócios e serviços da aplicação, incluindo os casos de uso.
- **Domain**: Contém as classes de modelo que representam as entidades do sistema e suas regras de negócio.
- **Infrastructure**: Contém o contexto do banco de dados, configurações do Entity Framework e implementações de acesso a dados.

---

Se precisar de mais ajustes, é só avisar!

## Docker Compose

O projeto utiliza Docker Compose para gerenciar a aplicação e a instância do SQL Server. O arquivo `docker-compose.yml` inclui configurações para:

- A aplicação backend .NET.
- Uma instância do SQL Server configurada com persistência de dados.

## Tecnologias Utilizadas

- **.NET 8**: Framework para desenvolvimento de aplicações web.
- **Entity Framework Core**: ORM para manipulação de dados no banco de dados.
- **SQL Server**: Banco de dados relacional utilizado na aplicação.
- **Docker**: Para containerização da aplicação e gerenciamento de dependências.
- **Docker Compose**: Para orquestração dos containers da aplicação e do banco de dados.
- **ASP.NET Core Identity**: Facilita o gerenciamento de usuários com rotas de login, registro, refresh, entre outras, automaticas.

## Oberservações
- Não foi usado nenhum script para criação do banco de dados, este sendo gerenciado pelo EFCORE.

