using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using products_ccdigital.Application;
using products_ccdigital.Application.Products.Commands;
using products_ccdigital.Application.Products.Queries;
using products_ccdigital.Infrastructure;
using products_ccdigital.Infrastructure.Persistence;
using System;

namespace products_ccdigital.ApiTest
{
    public abstract class BaseTest
    {
        protected IServiceProvider serviceProvider { get; set; }
        protected ServiceCollection servicesCollection { get; set; }
        protected SqliteConnection sqliteConnection { get; set; }

        [TestInitialize]
        public void Init()
        {
            servicesCollection = new ServiceCollection();
            servicesCollection.AddMvc();

            sqliteConnection = new SqliteConnection("DataSource=:memory:");
            sqliteConnection.Open();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(sqliteConnection);
            var options = dbContextOptionsBuilder.Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            servicesCollection.AddSingleton<ApplicationDbContext>(context);

            servicesCollection.AddDbContext<ApplicationDbContext>(op => { op.UseSqlite(sqliteConnection); });

            var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configurationBuilder.Build();

            servicesCollection.AddSingleton<IConfiguration>(configuration);
            // Agregar Servicios al contenedor de dependencias
            servicesCollection.AddApplication();
            servicesCollection.AddInfrastructure(configuration);

            // Registrar MediatR y los handlers reales
            servicesCollection.AddMediatR(typeof(GetAllQueryHandler).Assembly);
            servicesCollection.AddMediatR(typeof(GetByIdQueryHandler).Assembly);
            servicesCollection.AddMediatR(typeof(CreateProductCommandHandler).Assembly);
            servicesCollection.AddMediatR(typeof(DeleteProductCommandHandler).Assembly);
            servicesCollection.AddMediatR(typeof(UpdateProductCommandHandler).Assembly);

            serviceProvider = servicesCollection.BuildServiceProvider();
        }

        public void ResetDbContext()
        {
            serviceProvider = serviceProvider.CreateScope().ServiceProvider;
        }

        public void InsertarRegistro(string script) {
            var dbContext = serviceProvider.GetService<ApplicationDbContext>();
            dbContext.Database.ExecuteSqlRaw(script);
        }

        [TestCleanup]
        public void CleanUp()
        {
            sqliteConnection.Close();
        }
    }
}