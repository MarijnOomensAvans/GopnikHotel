using Gopnik_Hotel.Models;
using System.Collections.Generic;

namespace Gopnik_Hotel.DomainModel.Repositories
{
    public interface IKamerRepository
    {
        Kamer GetKamerById(int KamerId);
        List<Kamer> GetAll();
        void Create(Kamer kamer);
        void Update(Kamer kamer);
        void Delete(Kamer kamer);
        void Save();
    }
}