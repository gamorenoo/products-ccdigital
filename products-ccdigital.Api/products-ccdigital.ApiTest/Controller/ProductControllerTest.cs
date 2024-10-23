using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using products_ccdigital.Api.Controllers;
using products_ccdigital.Application.Products;
using Microsoft.Extensions.DependencyInjection;
using products_ccdigital.Application.Common.Exceptions;
using products_ccdigital.Application.Products.Queries;

namespace products_ccdigital.ApiTest.Controller
{
    [TestCategory("UnitTest")]
    [TestClass]
    public class ProductControllerTest : BaseTest
    {
        private ProductController _productController;
        private readonly Mock<ISender> _mediatorMock;
        private readonly Mock<ILogger<ProductController>> _fakeLogger;

        public ProductControllerTest()
        {
            _mediatorMock = new Mock<ISender>();
            _fakeLogger = new Mock<ILogger<ProductController>>();
        }

        [TestInitialize]
        public void Initialize()
        {
            // Insertarun producto de prueba
            InsertarRegistro("INSERT INTO Product(Id, Code, Name, Description, Price, Stock, Created, RowVersion) VALUES ('553e2e51-cf4d-4923-b2e9-e97575cf7a68', 'PROD001', 'Producto 1', 'Descripción producto 1', 1500, 10, '2024-10-23 02:55:21.773', 'a8871de5-976b-48bc-b5dc-6c8f288bfd48');");
            _productController = ActivatorUtilities.CreateInstance<ProductController>(serviceProvider, _fakeLogger.Object);

            _productController.ControllerContext = new ControllerContext();
            _productController.ControllerContext.HttpContext = new DefaultHttpContext();
            _productController.ControllerContext.HttpContext.RequestServices = serviceProvider;
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            IEnumerable<ProductDto> products = await _productController.Get();

            Assert.IsNotNull(products);
            Assert.IsTrue(products.Any());
        }

        [TestMethod]
        public async Task GetByIdTest()
        {
            Guid id = Guid.Parse("553e2e51-cf4d-4923-b2e9-e97575cf7a68");
            ProductDto product = await _productController.Get(id);

            Assert.IsNull(product);
        }

        [TestMethod]
        public async Task UpdateTest()
        {
            // Primera forma de Assert con Excepciones
            IEnumerable<ProductDto> products = await _productController.Get();
            ProductDto? productDto = products.FirstOrDefault();
            if (productDto != null)
            {
                productDto.Name = "Test Update";
                try
                {
                    var actionResult = await _productController.Put(productDto);
                }
                catch (NotFoundException ex)
                {
                    Assert.IsNotNull(ex);
                }
            }
        }

        [TestMethod]
        public async Task DeleteNotFoundExceptionTest()
        {
            // Segunda forma de Assert con Excepciones
            Guid id = Guid.Parse("553e2e51-cf4d-4923-b2e9-e97575cf7a68");
            // var result = await _productController.Delete(id);
            Assert.ThrowsExceptionAsync<NotFoundException>(async () => await _productController.Delete(id));
        }

        [TestMethod]
        public async Task MoqSenderTest() {
            // Esta prueba es realizada haciendo un mock del Sender de Mediator, solo va hasta el controlador y devuelve datos fake
            // Es una forma de probar cada pieza del aplicativo
            IEnumerable<ProductDto> productList = await _productController.Get();

            _productController = ActivatorUtilities.CreateInstance<ProductController>(serviceProvider, _fakeLogger.Object);

            _productController.ControllerContext = new ControllerContext();
            _productController.ControllerContext.HttpContext = new DefaultHttpContext();

            _productController.ControllerContext.HttpContext.RequestServices = Mock.Of<IServiceProvider>(provider =>
                provider.GetService(typeof(ISender)) == _mediatorMock.Object);

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllQuery>(), default))
                         .ReturnsAsync(productList);


            var result = _productController.Get();

        }
    }
}
