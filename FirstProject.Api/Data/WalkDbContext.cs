using FirstProject.Api.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Api.Data
{
    public class WalkDbContext : DbContext
    {
        public WalkDbContext(DbContextOptions<WalkDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var defficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-4a8b-9c0d-e1f2a3b4c5d6"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("b2c3d4e5-f6a7-4b9c-0d1e-f2a3b4c5d6e7"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("c3d4e5f6-a7b8-4c0d-1e2f-a3b4c5d6e7f8"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(defficulties);
            // Seed data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("e5f6a7b8-c9d0-4e1f-2a3b-4c5d6e7f8a9b"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("f6a7b8c9-d0e1-4f2a-3b4c-5d6e7f8a9bac"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("a7b8c9d0-e1f2-4a3b-4c5d-6e7f8a9bacd0"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("b8c9d0e1-f2a3-4b4c-5d6e-7f8a9bacde1f"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("c9d0e1f2-a3b4-4c5d-6e7f-8a9bacde1f2a"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}
