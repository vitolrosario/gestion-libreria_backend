using Features.Books;
using Features.BookSales;
using Features.Clients;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookSale> BookSales { get; set; }
    public DbSet<BookSaleDetail> BookSalesDetails { get; set; }
}
