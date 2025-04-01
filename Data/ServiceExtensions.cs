using Microsoft.Extensions.DependencyInjection;
using Features.Clients;
using Features.Books;
using Features.BookSales;

namespace Data
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookSaleService, BookSaleService>();
            services.AddScoped<IBookSaleRepository, BookSaleRepository>();
        }
    }
}
