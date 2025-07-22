using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReciPick.Domain.DomainModels;
using ReciPick.Domain.Identity;
using System.Reflection.Emit;

namespace ReciPick.Repository;

public class ApplicationDbContext : IdentityDbContext<ReciPickUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Recipe-Ingredient relationship
        modelBuilder.Entity<Ingredient>()
            .HasOne(i => i.Recipe)
            .WithMany(r => r.Ingredients)
            .HasForeignKey(i => i.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);


        // ShoppingCart-User relationship
        modelBuilder.Entity<ShoppingCart>()
            .HasOne(sc => sc.User)
            .WithMany(u => u.ShoppingCarts)
            .HasForeignKey(sc => sc.UserId)
                            .OnDelete(DeleteBehavior.Cascade);


        // ShoppingCartItem relationships
        modelBuilder.Entity<ShoppingCartItem>()
            .HasOne(sci => sci.ShoppingCart)
            .WithMany(sc => sc.Items)
            .HasForeignKey(sci => sci.ShoppingCartId)
                            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<ShoppingCartItem>()
            .HasOne(sci => sci.Ingredient)
            .WithMany()
            .HasForeignKey(sci => sci.IngredientId)
                            .OnDelete(DeleteBehavior.Cascade);


        // Order-User relationship
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
                            .OnDelete(DeleteBehavior.Cascade);


        // OrderItem relationships
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Ingredient)
            .WithMany()
            .HasForeignKey(oi => oi.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

    }
}

