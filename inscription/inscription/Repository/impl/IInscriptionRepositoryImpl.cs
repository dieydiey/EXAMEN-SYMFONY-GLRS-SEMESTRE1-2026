using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using inscription.Data;
using inscription.Models;
using inscription.Repository.Interfaces;

namespace inscription.Repositoriy.impl
{
    public class InscriptionRepositoryImpl : IInscriptionRepositoryInterface
    {
        private readonly AppDbContext _context;

        public InscriptionRepositoryImpl(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Inscription inscription)
        {
            _context.Inscriptions.Add(inscription);
        }

      public IEnumerable<Inscription> GetByClasse(int classeId)
        {
            if (classeId <= 0) return new List<Inscription>();

            return _context.Inscriptions
                .Include(i => i.Etudiant)
                .Include(i => i.Classe)
                .Include(i => i.AnneeScolaire)
                .Where(i => i.ClasseId == classeId)
                .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool ExisteInscription(int etudiantId, int anneeScolaireId)
        {
            return _context.Inscriptions.Any(i =>
                i.EtudiantId == etudiantId &&
                i.AnneeScolaireId == anneeScolaireId);
        }

    }
}
