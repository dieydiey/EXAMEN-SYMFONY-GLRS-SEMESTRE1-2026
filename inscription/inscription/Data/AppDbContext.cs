using Microsoft.EntityFrameworkCore;
using inscription.Models;
using inscription.Models.Enums;

namespace inscription.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<AnneeScolaire> AnneeScolaires { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
            modelBuilder.Entity<AnneeScolaire>().HasData(
                new AnneeScolaire
                {
                    Id = 1,
                    Libelle = "2025-2026",
                    Statut = StatutAnneeScolaire.EnCours
                }
            );

            modelBuilder.Entity<Classe>().HasData(
                new Classe { Id = 1, Code = "L1", Libelle = "Licence 1 Informatique" },
                new Classe { Id = 2, Code = "L2", Libelle = "Licence 2 Informatique" }
            );

            modelBuilder.Entity<Etudiant>().HasData(
                new Etudiant { Id = 1, Matricule = "ET001", Nom = "DIOP", Prenom = "Aliou" },
                new Etudiant { Id = 2, Matricule = "ET002", Nom = "NDIAYE", Prenom = "Aminata" }
            );

            modelBuilder.Entity<Inscription>()
                .HasIndex(i => new { i.EtudiantId, i.AnneeScolaireId })
                .IsUnique();

            }

    }
}
