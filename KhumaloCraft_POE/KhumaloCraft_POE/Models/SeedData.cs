using KhumaloCraft_Part2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace KhumaloCraft_Part2.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new KhumaloCraft_Part2Context(
            serviceProvider.GetRequiredService<
                DbContextOptions<KhumaloCraft_Part2Context>>()))
        {
            // Look for any products.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }
            context.Products.AddRange(
                new Products
                {
                    Name = "Shoes",
                    Price = 7.99M,
                    Category = "Clothing",
                    Availability = true
                },
                new Products
                {
                    Name = "Painting",
                    Price = 80.00M,
                    Category = "Art",
                    Availability = true
                },
                new Products
                {
                    Name = "Bracelet",
                    Price = 12.99M,
                    Category = "Jewellery",
                    Availability = true
                },
                new Products
                {
                    Name = "Necklace",
                    Price = 7.99M,
                    Category = "Jewellery",
                    Availability = true
                }
            );
            context.SaveChanges();
        }
    }
}