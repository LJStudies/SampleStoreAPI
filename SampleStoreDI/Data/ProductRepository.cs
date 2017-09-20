using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleStoreDI.Models
{
    /// <summary>
    /// Implementação do contrato IProductRepository.
    /// </summary>
    /// <seealso cref="SampleStoreDI.Models.IProductRepository" />
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// Objeto que contêm as informações referente ao contexto do acesso aos dados.
        /// </summary>
        private readonly ProductDbContext _context;

        /// <summary>
        /// Inicializa uma instância do repositório do produto <see cref="ProductRepository"/>.
        /// </summary>
        /// <param name="context">Injeção de dependência do contexto da base de dados.</param>
        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        IEnumerable<Product> IProductRepository.All => _context.Product;

        public void Delete(Product product)
        {
            _context.Product.Remove(product);
        }

        public bool Exists(int ID)
        {
            return _context.Product.Any(e => e.ID == ID);
        }

        public Product Find(int ID)
        {
            return _context.Product.SingleOrDefault(m => m.ID == ID);
        }

        public void Insert(Product product)
        {
            _context.Product.Add(product);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        //Implementações do Garbage Collector da interface IDisposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
