using System;
using Gopnik_Hotel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopnik_Hotel.DomainModel.Repositories
{
   public interface IKlantRepository
    {
        Klant GetKlantById(int KlantId);
        List<Klant> GetAll();
        void Create(Klant klant);
        void Update(Klant klant);
        void Delete(Klant klant);
        void Save();
    }
}
