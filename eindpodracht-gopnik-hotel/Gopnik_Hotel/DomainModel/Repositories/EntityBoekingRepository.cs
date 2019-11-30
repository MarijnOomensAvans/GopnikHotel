using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Gopnik_Hotel.Models;

namespace Gopnik_Hotel.DomainModel.Repositories
{
    public class EntityBoekingRepository : IBoekingRepository
    {
        GopnikHotelDBEntities Context;
        int idc = 0;

        public EntityBoekingRepository(GopnikHotelDBEntities dbContext)
        {
            Context = dbContext;
        }

        public void Create(Boeking boeking)
        {
            if (Context.Boekings.SingleOrDefault(b => b.BoekingId == Context.Boekings.Count()) != null)
            {
                boeking.BoekingId = Context.Boekings.OrderByDescending(i => i.BoekingId).Select(o => o.BoekingId).FirstOrDefault() + 1 + idc;
                idc++;
            }
            else
            {
                boeking.BoekingId = 1;
            }
            Context.Boekings.Add(boeking);
        }

        public void Delete(Boeking boeking)
        {
            Boeking deleteBoeking = Context.Boekings.SingleOrDefault(b => b.BoekingId == boeking.BoekingId);
            Context.Boekings.Remove(deleteBoeking);
        }

        public Boeking GetBoekingById(int BoekingId)
        {
            return Context.Boekings.SingleOrDefault(b => b.BoekingId == BoekingId);
        }

        public List<Boeking> GetAll()
        {
            return Context.Boekings.ToList();
        }

        public void Update(Boeking boeking)
        {
            Context.Entry(boeking).State = EntityState.Modified;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}