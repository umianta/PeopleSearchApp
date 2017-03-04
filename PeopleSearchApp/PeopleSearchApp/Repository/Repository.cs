using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PeopleSearchApp.DataContext;
using System.Data.Entity;
namespace PeopleSearchApp.Repository
{
    public class Repository<T> :IRepository<T> where T :class
    {
        private UserContext db;
        private DbSet<T> dbSet;

        public Repository()
        {
            db = new UserContext();
            dbSet = db.Set<T>();

        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(object Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T obj)
        {
            dbSet.Add(obj); 
        }

        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object Id)
        {
            T obj = dbSet.Find(Id);
            dbSet.Remove(obj);

        }
        public void Save()
        {
            db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(this.db !=null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}