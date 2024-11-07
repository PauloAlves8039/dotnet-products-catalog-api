<h1 align="center">Products Catalog</h1>

<p align="center">
  <a href="https://learn.microsoft.com/pt-br/dotnet/"><img alt="DotNet 6" src="https://img.shields.io/badge/.NET-5C2D91?logo=.net&logoColor=white&style=for-the-badge" /></a>
  <a href="https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/"><img alt="C#" src="https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white&style=for-the-badge" /></a>
  <a href="https://www.microsoft.com/pt-br/sql-server/sql-server-downloads"><img alt="SQL Server" src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white" /></a>
  <a href="https://www.docker.com/"><img alt="Docker" src="https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white" /></a>
</p>

## :computer: Projeto

Repositório com uma WebAPI para gerenciar um `catálogo` de `produtos` aplicando conceitos da `Clean Architecture`, esse projeto poderá receber futuras alterações conforme necessário.

## :blue_book: Regra de Negócio

Funcionalidades implementadas `catálogo` e `produto` nessa aplicação:

- Adicionar registro.
- Exibir listas de registros.
- Selecionar registro.
- Atualizar registro.
- Excluir registro.

## ✔️ Recursos Utilizados

- `Clean Architecture`
- `.NET 6.0`
- `ASP.NET Core WebAPI`
- `C#`
- `SQL Server`
- `Entity Framework Core`
- `AutoMapper`
- `Microsoft Identity`
- `JWT`
- `Swagger`
- `XUnit`
- `Moq`
- `FluentAssertions`
- `Docker`

## :white_check_mark: Decisões Técnicas

- Adicionei o uso de containers com o `Docker`.
- Fiz a implementação de alguns testes de unidade com o `XUnit` e recursos auxiliares como o `FluentAssertions` e `Moq`.
- Procurei manter uma estrutura mais objetiva e próxima dos conceitos da `Clean Architecture`.

## :wrench: Utilização do Projeto

- Após baixar ou clonar o projeto navegue até [appsettings.json](https://github.com/PauloAlves8039/dotnet-products-catalog-api/blob/master/src/ProductCatalog.WebAPI/appsettings.json) e atualize a sua `string de conexão` de acordo com as suas credenciais do `SQL Server`.
- Em seguida selecione o projeto `ProductCatalog.Infra.Data` caso esteja usando o `Visual Studio IDE` abra o `Package Manager Console` execute o comando `Update-Database`, caso esteja usando o `Visual Studio Code` execute o comando `dotnet ef database update` para a geração da base de dados.
- Caso deseje usar o `Docker` também é possível executando o `Docker Compose` pelo `Visual Studio IDE`, com container do `SQL Server` em execução criei um banco de dados com chamado `ProductCatalogWebAPIDB` em seguida execute o script [Script-ProductCatalog.sql](https://github.com/PauloAlves8039/dotnet-products-catalog-api/blob/master/Resources/Script%20Database/Script-ProductCatalog.sql) para montar toda a estrutura das tabelas do banco de dados.
- Com o projeto configurado e sendo executado pode ser feita a criação de usuários para utilização da WebAPI, para testar todas funcionalidade recomendo a criação de um usuário chamado `admin@localhost` a senha pode ser a de sua preferência, um exemplo, `SuaSenha@2014`

## :floppy_disk: Clonar Repositório

```bash
git clone https://github.com/PauloAlves8039/dotnet-products-catalog-api.git
```

## :camera: Screenshots

<p align="center"> <img src="https://github.com/PauloAlves8039/dotnet-products-catalog-api/blob/master/src/ProductCatalog.WebAPI/assets/images/screenshot1.png" /></p>
<p align="center"> <img src="https://github.com/PauloAlves8039/dotnet-products-catalog-api/blob/master/src/ProductCatalog.WebAPI/assets/images/screenshot2.png" /></p>

## :boy: Author

<a href="https://github.com/PauloAlves8039"><img src="https://avatars.githubusercontent.com/u/57012714?v=4" width=70></a>
[Paulo Alves](https://github.com/PauloAlves8039)
