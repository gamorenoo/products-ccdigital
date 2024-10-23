using Microsoft.AspNetCore.Mvc;
using products_ccdigital.Application.Products;
using products_ccdigital.Application.Products.Commands;
using products_ccdigital.Application.Products.Queries;
using products_ccdigital.Domain.Common;

namespace products_ccdigital.Api.Controllers
{
    /// <summary>
    /// Controlador de productos
    /// </summary>
    [Route("api/product/")]
    public class ProductController : ApiControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        /// <summary>
        /// Builder
        /// </summary>
        /// <param name="logger"></param>
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Crear producto
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Producto creado</returns>
        /// <response code="200">Producto Creado</response>
        /// <response code="401">No autorizado</response>
        /// <response code="500">Error interno del servidor</response>
        /// <response code="400">Request invalido</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductCreateDto product)
        {
            return await Mediator.Send(new CreateProductCommand() { Product = product });
        }

        /// <summary>
        /// Actualizar producto
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Producto creado</returns>
        /// <response code="200">Producto Actualizado</response>
        /// <response code="401">No autorizado</response>
        /// <response code="500">Error interno del servidor</response>
        /// <response code="400">Request invalido</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult<ProductDto>> Put([FromBody] ProductDto product)
        {
            return await Mediator.Send(new UpdateProductCommand() { Product = product });
        }

        /// <summary>
        /// Eliminar producto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Bandera que indica si se elimina el producto o no</returns>
        /// <response code="200">Producto Eliminado</response>
        /// <response code="401">No autorizado</response>
        /// <response code="500">Error interno del servidor</response>
        /// <response code="400">Request invalido</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteProductCommand() { Id = id });
        }

        /// <summary>
        /// Obtener Productos
        /// </summary>
        /// <returns>lista de productos registrados</returns>
        /// <response code="200">Lista de Productos</response>
        /// <response code="401">No autorizado</response>
        /// <response code="500">Error interno del servidor</response>
        /// <response code="400">Request invalido</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            return await Mediator.Send(new GetAllQuery());
        }

        /// <summary>
        /// Obtener producto por Id
        /// </summary>
        /// <returns>Productos que coincide</returns>
        /// <response code="200">Usuario</response>
        /// <response code="401">No autorizado</response>
        /// <response code="500">Error interno del servidor</response>
        /// <response code="400">Request invalido</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ProductDto> Get(Guid id)
        {
            return await Mediator.Send(new GetByIdQuery() { Id = id });
        }

        /// <summary>
        /// Obtener productos paginados
        /// </summary>
        /// <returns>lista de productos registrados en la pagina indicada</returns>
        /// <response code="200">Productos paginados</response>
        /// <response code="401">No autorizado</response>
        /// <response code="500">Error interno del servidor</response>
        /// <response code="400">Request invalido</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("get-paginated")]
        public async Task<PaginatedResult<ProductDto>> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return await Mediator.Send(new GetPaginatedQuery() { Page = page, PageSize = pageSize });
        }
    }
}
