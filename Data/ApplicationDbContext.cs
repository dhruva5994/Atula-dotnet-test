using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PracticalDemo.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<LoginViewModel> loginViewModels { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Many-to-Many relationship
        builder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products);

        // Data seeding for Categories
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Table" },
            new Category { Id = 2, Name = "Chair" },
            new Category { Id = 3, Name = "Sofa" }
        );

        // Data seeding for Products
        builder.Entity<Product>().HasData(
            new Product { Id = 1, Sku = "SKUA", Name = "Lorem Table" },
            new Product { Id = 2, Sku = "SKUB", Name = "Ipsum Table" },
            new Product { Id = 3, Sku = "SKUC", Name = "Dolor Table" },
            new Product { Id = 4, Sku = "SKUD", Name = "Sit Chair" },
            new Product { Id = 5, Sku = "SKUE", Name = "Amet Chair" },
            new Product { Id = 6, Sku = "SKUF", Name = "Consectetur Chair" },
            new Product { Id = 7, Sku = "SKUG", Name = "Adipiscing Sofa" },
            new Product { Id = 8, Sku = "SKUH", Name = "Elit Sofa" },
            new Product { Id = 9, Sku = "SKUI", Name = "Mauris Sofa" }
        );

        // Data seeding for Login
        builder.Entity <LoginViewModel>().HasData(
            new LoginViewModel {LoginID = 1, Email = "Testing@gmail.com", Password = "Test@1234" },
            new LoginViewModel {LoginID = 2, Email = "John@gmail.com", Password = "John@1234" },
            new LoginViewModel {LoginID = 3, Email = "Thomaz@gmail.com", Password = "Thomaz@1234" }
            );
    }
}