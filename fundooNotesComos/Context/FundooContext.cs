using fundooNotesCosmos.Entities;
using Microsoft.EntityFrameworkCore;

namespace fundooNotesCosmos.Context
{
    public class FundooContext : DbContext
    {

        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<UserEntity> User { get; set; }

        public DbSet<NoteEntity> Note { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToContainer("User").HasNoDiscriminator().HasManualThroughput(400).HasPartitionKey(x => x.id).HasKey(x=>x.id);
            modelBuilder.Entity<NoteEntity>().ToContainer("Note").HasNoDiscriminator().HasManualThroughput(400).HasPartitionKey(x => x.UserId).HasKey(x=>x.id);

        }

    }
}