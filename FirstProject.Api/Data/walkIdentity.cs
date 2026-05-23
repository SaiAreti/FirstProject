using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Api.Data
{
    public class walkIdentity : IdentityDbContext
    {
        public walkIdentity(DbContextOptions<walkIdentity> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "48011ab0-e28a-4e43-862b-f40d304bee36";
            var writerRoleId = "f2642b89-c243-44d9-8e1e-c81e59adc97f";


            var roles = new List<IdentityRole> {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "reader",
                    NormalizedName = "READER"

                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "writer",
                    NormalizedName = "WRITER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
