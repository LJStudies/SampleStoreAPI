using System;
using System.Collections.Generic;

namespace SampleStoreDI.Models
{
    /// <summary>
    /// 
    /// Interface que provê os mecanismos para implementação do Repository Pattern,
    /// possibilitando a utilização da injeção de dependência sobre a classe de domínio
    /// Product, visando maior desacoplamento da solução. Seus métodos defininem toda a
    /// lógica no provimento dos dados.
    ///  
    /// O Repository Pattern tem como objetivo criar uma camada de abstração entre os dados 
    /// (Model) e a lógica de negócio da aplicação (Controller). Assim podemos ter, para
    /// cada classe de domínio uma interface (Repository Interface) e sua implementação 
    /// (Repository Class). Desta forma, ao implementarmos nossa camada de controle teremos
    /// como referência a interface, de modo que a camada de controle aceite qualquer objeto
    /// que implemente nossa interface, através da injeção de dependência.
    /// 
    /// Essa abordagem aumenta o desacoplamento da solução e facilita a automação de rotinas
    /// de testes (TDD, por exemplo).
    /// 
    /// Mais sobre Repository Pattern em:
    ///     https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    /// 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IProductRepository : IDisposable
    {
        /// <summary>
        /// Verifica a existência de um Produto.
        /// </summary>
        /// <param name="ID">O identificador do produto.</param>
        /// <returns>true, caso o produto exista; false, caso contrário.</returns>
        bool Exists(int ID);

        /// <summary>
        /// Obtém todos os Produtos.
        /// </summary>
        /// <value>
        /// Lista com todos os produtos cadastrados.
        /// </value>
        IEnumerable<Product> All { get; }

        /// <summary>
        /// Localiza um produto específico.
        /// </summary>
        /// <param name="ID">O identificador do produto buscado.</param>
        /// <returns>O produto localizado</returns>
        Product Find (int ID);

        /// <summary>
        /// Insere um produto espefíco.
        /// </summary>
        /// <param name="product">O produto.</param>
        void Insert(Product product);

        /// <summary>
        /// Atualiza um produto específico.
        /// </summary>
        /// <param name="product">O produto.</param>
        void Update(Product product);

        /// <summary>
        /// Deleta um produto específico.
        /// </summary>
        /// <param name="product">O produto.</param>
        void Delete(Product product);

        /// <summary>
        /// Salva as alterações realizadas.
        /// </summary>
        void Save();
    }
}
