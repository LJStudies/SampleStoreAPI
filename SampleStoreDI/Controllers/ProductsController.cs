using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleStoreDI.Models;
using Microsoft.AspNetCore.Cors;

namespace SampleStoreDI.Controllers
{
    /// <summary>
    /// 
    /// Classe de Controller dos Produtos.
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/products")]
    [EnableCors("AllowAllOrigin")]
    public class ProductsController : Controller
    {
        /// <summary>
        /// Instância que implementa o Repositorio de Produto.
        /// </summary>
        private readonly IProductRepository _productRepository;


        /// <summary>
        /// Incializa uma nova instância da classe <see cref="ProductsController"/>.
        /// </summary>
        /// <param 
        ///     name="productRepository">Injeção de dependência via construtor do repositório do produto.
        /// </param>
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Product> GetProduct()
        {
            return _productRepository.All;
        }

        // GET: api/Products/5
        /// <summary>
        /// Obtêm um produto (recurso) espefífico.
        /// </summary>
        /// <param name="id">O identificador do recurso.</param>
        /// <returns>
        ///     Um <see cref="Microsoft.AspNetCore.Http.StatusCodes"/> que podem ser:
        ///         400 (Bad Request): Caso o identificador não seja informado ou não seja de um tipo válido;
        ///         404 (Not Found)  : Caso o recurso não exista;
        ///         200 (Ok)         : Caso o recurso seja localizado.
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _productRepository.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        /// <summary>
        /// Atualiza um produto (recurso) específico.
        /// </summary>
        /// <param name="id">Identificador do recurso.</param>
        /// <param name="product">As novas informações do produto.</param>
        /// <returns>
        ///     Um <see cref="Microsoft.AspNetCore.Http.StatusCodes"/> que podem ser:
        ///         400 (Bad Request): Caso os dados do produto não sejam corretamente informados;
        ///         404 (Not Found)  : Caso o recurso não exista;
        ///         204 (No Content) : Caso o recurso seja atualizado com sucesso.
        /// </returns>
        [HttpPut("{id}")]
        public IActionResult PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ID)
            {
                return BadRequest();
            }

            _productRepository.Update(product);

            try
            {
                _productRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        /// <summary>
        /// Insere um novo produto (recurso).
        /// </summary>
        /// <param name="product">O produto.</param>
        /// <returns>
        ///     Um <see cref="Microsoft.AspNetCore.Http.StatusCodes"/> que podem ser:
        ///         400 (Bad Request): Caso os dados do produto não sejam corretamente informados;
        ///         201 (Created) : Caso o recurso seja criado com sucesso.
        /// </returns>
        [HttpPost]
        public IActionResult PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productRepository.Insert(product);     
            _productRepository.Save();

            return CreatedAtAction("GetProduct", new { id = product.ID }, product);
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Exclui um produto (recurso).
        /// </summary>
        /// <param name="id">O produto.</param>
        /// <returns>
        ///     Um <see cref="Microsoft.AspNetCore.Http.StatusCodes"/> que podem ser:
        ///         400 (Bad Request): Caso os dados do produto não sejam corretamente informados;
        ///         404 (Not Found)  : Caso o recurso não exista;
        ///         200 (Ok) : Caso o recurso seja excluído com sucesso.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _productRepository.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.Delete(product);
            _productRepository.Save();
            
            return Ok(product);
        }

        private bool ProductExists(int id)
        {
            return _productRepository.Exists(id);
        }
    }
}