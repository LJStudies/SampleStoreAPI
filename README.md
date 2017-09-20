# SampleStoreAPI
_Exemplo básico de Web API em ASP .NET Core 2.0_

Este exemplo traz uma Web API inspirada na arquitetura **REST (_Representational State Transfer_)** desenvolvida em **Microsoft Visual Studio ASP.NET Core 2.0**, que acessa uma **base de dados de produtos** fictícia através de **HTTP Methods Request (_GET, POST, PUT, DELETE_)**, utilizando o formato **JSON (_JavaScript Object Notation_)**.

## 1. Modelos de Dados

O produto segue o seguinte modelo, em formato JSON:
```
{ 
    "id" : Identificador do produto,
    "name" : "Nome do produto",
    "price" : Preço do Produto,
    "description" : "Descrição do produto",
    "imageUrl" : "Url para a imagem do produto"
}
```
Exemplo:
```
{
    "id" : 4,
    "name" : "Impressora",
    "description" : "Impressora Colorida",
    "price" : 1199.99,
    "imageUrl" : "https://imagem_impressora.jpg"
}
```
## 2. Design Pattern

Esta aplicação implementa um exemplo de Repository Pattern <sup>[1](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)
</sup>, que oferece maior desacoplamento entre o os dados (Model) e o controle da aplicação (controller), fazendo uso da injeção de dependência nativa do ASP.NET Core 2.0.

A aplicação está hospedada no ambiente da Microsoft Azure no endereço http://ljasmimsamplestore.azurewebsites.net/api/products e pode ser acessada da seguinte forma:

* **HTTP GET** _api/products_ - Recupera uma lista de produtos;
* **HTTP GET** _api/products/{id}_ - Recupera um produto específico;
* **HTTP POST** _api/products_ - Criar um novo produto;
* **HTTP PUT** _api/products/{id}_ - Atualiza um produto específico;
* **HTTP DELETE** _api/produtcs/{id}_ - Exclui um produto específico.

