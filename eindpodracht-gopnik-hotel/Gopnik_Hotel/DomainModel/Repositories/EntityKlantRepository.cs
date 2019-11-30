using Gopnik_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Gopnik_Hotel.DomainModel.Repositories
{
    public class EntityKlantRepository : IKlantRepository
    {
        GopnikHotelDBEntities Context;
        int idc = 0;

        public EntityKlantRepository(GopnikHotelDBEntities dbContext)
        {
            Context = dbContext;
        }

        public void Create(Klant klant)
        {
            int klantAmount = Context.Klants.Count();
            if (Context.Klants.SingleOrDefault(b => b.KlantId == klantAmount) != null)
            {
                klant.KlantId = Context.Klants.OrderByDescending(i => i.KlantId).Select(o => o.KlantId).FirstOrDefault() + 1 + idc;
                idc++;
            }
            else
            {
                klant.KlantId = 1;
            }
            Context.Klants.Add(klant);
        }

        public void Delete(Klant klant)
        {
            Klant deleteKlant = Context.Klants.Find(klant.KlantId);
            Context.Klants.Remove(deleteKlant);
        }

        public Klant GetKlantById(int KlantId)
        {
            return Context.Klants.Find(KlantId);
        }

        public List<Klant> GetAll()
        {
            return Context.Klants.ToList();
        }

        public void Update(Klant klant)
        {
            Context.Entry(klant).State = EntityState.Modified;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}