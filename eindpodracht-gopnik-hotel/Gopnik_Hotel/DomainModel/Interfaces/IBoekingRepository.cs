using System;
using Gopnik_Hotel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopnik_Hotel.DomainModel.Repositories
{
    public interface IBoekingRepository
    {
        Boeking GetBoekingById(int BoekingId);
        List<Boeking> GetAll();
        void Create(Boeking boeking);
        void Update(Boeking boeking);
        void Delete(Boeking boeking);
        void Save();
    }
}
