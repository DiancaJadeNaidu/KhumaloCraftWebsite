using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KhumaloCraft_Part2.Models;
using static NuGet.Packaging.PackagingConstants;

namespace KhumaloCraft_Part2.Data
{
    public class KhumaloCraft_Part2Context : DbContext
    {
        public KhumaloCraft_Part2Context (DbContextOptions<KhumaloCraft_Part2Context> options)
            : base(options)
        {
        }

        public DbSet<KhumaloCraft_Part2.Models.Products> Products { get; set; } = default!;
       // public DbSet<Orders> Orders { get; set; }
        public DbSet<KhumaloCraft_Part2.Models.CartItem> CartItem { get; set; } = default!;
        public DbSet<KhumaloCraft_Part2.Models.Orders> Orders { get; set; } = default!;

        }
    }

