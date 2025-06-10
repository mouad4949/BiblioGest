using Microsoft.EntityFrameworkCore;
using BiblioGest.Models;

namespace BiblioGest.Data
{
    public class BiblioGestContext : DbContext
    {
        public DbSet<Livre> Livres { get; set; }
        public DbSet<Adherent> Adherents { get; set; }
        public DbSet<Emprunt> Emprunts { get; set; }
        public DbSet<Categorie> Categories { get; set; }

        public BiblioGestContext(DbContextOptions<BiblioGestContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livre>()
                .HasOne(l => l.Categorie)
                .WithMany(c => c.Livres)
                .HasForeignKey(l => l.CategorieId);

            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Livre)
                .WithMany(l => l.Emprunts)
                .HasForeignKey(e => e.ISBN);

            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Adherent)
                .WithMany(a => a.Emprunts)
                .HasForeignKey(e => e.AdherentId);

            // Données initiales pour les catégories
            modelBuilder.Entity<Categorie>().HasData(
                new Categorie { Id = 1, Nom = "Roman", Description = "Littérature romanesque" },
                new Categorie { Id = 2, Nom = "Science-Fiction", Description = "Science-fiction et fantastique" }
            );
        }
    }
}