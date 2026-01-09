using System.Collections.Generic;
using inscription.Models;

namespace inscription.Repository.Interfaces
{
    public interface IInscriptionRepositoryInterface
    {
        void Add(Inscription inscription);
        IEnumerable<Inscription> GetByClasse(int classeId);
        void Save();
        bool ExisteInscription(int etudiantId, int anneeScolaireId);

    }
}
