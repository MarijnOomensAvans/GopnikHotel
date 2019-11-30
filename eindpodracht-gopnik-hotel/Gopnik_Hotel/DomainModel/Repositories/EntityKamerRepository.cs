using Gopnik_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Gopnik_Hotel.DomainModel.Repositories
{
    public class EntityKamerRepository : IKamerRepository
    {
        GopnikHotelDBEntities Context;

        public EntityKamerRepository(GopnikHotelDBEntities dbContext)
        {
            Context = dbContext;
        }

        public void Create(Kamer kamer)
        {
            if (Context.Kamers.Find(Context.Kamers.Count()) != null)
            {
                kamer.KamerId = Context.Kamers.OrderByDescending(i => i.KamerId).Select(o => o.KamerId).FirstOrDefault() + 1;
            }
            else
            {
                kamer.KamerId = 1;
            }
            Context.Kamers.Add(kamer);
        }

        public void Delete(Kamer kamer)
        {
            Kamer deleteKamer = Context.Kamers.Find(kamer.KamerId);
            Context.Kamers.Remove(deleteKamer);
        }

        public Kamer GetKamerById(int KamerId)
        {
            return Context.Kamers.Find(KamerId);
        }

        public List<Kamer> GetAll()
        {
            return Context.Kamers.ToList();
        }

        public void Update(Kamer kamer)
        {
            Context.Entry(kamer).State = EntityState.Modified;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}