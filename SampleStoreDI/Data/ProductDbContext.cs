using Microsoft.EntityFrameworkCore;

namespace SampleStoreDI.Models
{
    /// <summary>
    /// Classe de contexto dos dados dos produtos.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class ProductDbContext : DbContext
    {

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ProductDbContext"/>.
        /// </summary>
        /// <param
        ///     name="options">Opção de configuração do acesso aos dados definidas na classe
        ///     <see cref="Startup.ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection)">,
        ///     tais como a string de conexão.
        /// </param>
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets ou Sets dos produtos. 
        /// </summary>
        public DbSet<SampleStoreDI.Models.Product> Product { get; set; }
    }
}
