namespace PCApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PCDBContext : DbContext
    {
        public PCDBContext()
            : base("name=PCDBContext")
        {
        }

        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Deck> Decks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .Property(e => e.CardType)
                .IsUnicode(false);

            modelBuilder.Entity<Card>()
                .Property(e => e.Deck)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Decks)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
