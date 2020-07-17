using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarWars.Data.Models
{
    public class SWContext : DbContext
    {
        protected SWContext()
        { }
        public SWContext(DbContextOptions<SWContext> options) : base(options)
        { }

        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbQuery<V_Character> V_Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cast>(entity => {
                entity.HasKey(x => new { x.CharacterId, x.EpisodeId });
                entity.HasOne(x => x.Character).WithMany(x => x.Casts).HasForeignKey(x => x.CharacterId);
                entity.HasOne(x => x.Episode).WithMany(x => x.Casts).HasForeignKey(x => x.EpisodeId);
            });
            modelBuilder.Entity<Relationship>(entity => {
                entity.HasKey(x => new { x.CharacterId, x.FriendId });
                entity.HasOne(x => x.Character).WithMany(x => x.Relationships).HasForeignKey(x => x.CharacterId);
            });
            modelBuilder.Query<V_Character>(query => {
                query.ToView("V_CHARACTERS");
                query.Property(x => x.Friends).HasConversion(x => JsonConvert.SerializeObject(x), x => JsonConvert.DeserializeObject<string[]>(x));
                query.Property(x => x.Episodes).HasConversion(x => JsonConvert.SerializeObject(x), x => JsonConvert.DeserializeObject<string[]>(x));
            });
        }
    }
}
