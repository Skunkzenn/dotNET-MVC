# ASP.NET Core MVC Project with Entity Framework and MySQL

## Overview

Este projeto é uma **Aplicação Web** desenvolvida usando **ASP.NET Core MVC 2.0.1** e **MySQL 8.0.12**. Utiliza o **Entity Framework Core** para gestão de banco de dados, com o **Pomelo.EntityFrameworkCore.MySql** para comunicação com o banco de dados MySQL. A aplicação está estruturada segundo o padrão de design Model-View-Controller (MVC), garantindo uma clara separação de responsabilidades e facilidade de manutenção.

## Funcionalidades

- **ASP.NET Core MVC**: Um framework robusto para construção de aplicações web, criado pela Microsoft.
- **Entity Framework Core**: Um mapeador objeto-relacional para .NET, proporcionando uma maneira simplificada de trabalhar com bancos de dados utilizando objetos .NET.
- **Banco de Dados MySQL**: Gerenciado com o pacote Pomelo Entity Framework Core MySQL para integração perfeita com MySQL.
- **Operações CRUD**: Implementadas para entidades como `Departments` e `Sellers`, incluindo recursos avançados como **Eager Loading** e **Tratamento de Exceções Personalizadas**.
- **Operações Assíncronas**: Melhor performance com o uso de async/await para operações de banco de dados.
- **UI Responsiva**: Construída com Razor pages e Bootstrap, garantindo uma interface moderna e responsiva.

## Configuração do Projeto

### Pré-requisitos

- **.NET Core SDK 2.1** [.NET Core SDK 2.1 - Download](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-2.1.818-windows-x64-installer)
- **MySQL Server 8.0.12** ou superior
- **Visual Studio 2017 ** ou **Visual Studio 2019 **

### Instalação

1. Clone o repositório:
   ```bash
   git clone https://github.com/Skunkzenn/dotNET_2-1_MVC.git

2. Navegue até o diretório do projeto:
   ```bash
   cd your-repo-name

3. Configure o banco de dados MySQL:
      - Certifique-se de que o servidor MySQL está em execução.
      - Atualize a string de conexão em appsettings.json
   ```bash
     "ConnectionStrings": {
     "SalesWebMvcContext": "server=localhost;userid=youruser;password=yourpassword;database=saleswebmvcappdb"
   }

4. Aplique as migrações e inicialize o banco de dados:
   ```bash
   dotnet ef database update

5. Execute a aplicação:
   ```bash
   dotnet run

## Estrutura do Projeto

- Controllers: Lidam com requisições, recuperam dados dos modelos e retornam respostas adequadas.
- Models: Representam as entidades de domínio e incluem lógica de negócios.
- Views: Definem os componentes da interface do usuário e são renderizados como HTML.

## Funcionalidades Principais
Gestão de Departamentos: 
- Criar, ler, atualizar e excluir departamentos.
- Validação de dados e tratamento de erros.

Gestão de Vendedores:
- Gerenciar vendedores com associação a departamentos.
- Implementação de funcionalidades de pesquisa e consultas avançadas.

Registros de Vendas:
- Funcionalidade de pesquisa simples e agrupada de vendas.
- Operações assíncronas para melhor desempenho.

## Personalização

- **Temas**:
  - Altere facilmente o visual trocando o tema Bootstrap em `_Layout.cshtml`.

- **Localização**:
  - A aplicação suporta diferentes locais, com formatação de números e datas personalizáveis em `Startup.cs`.


