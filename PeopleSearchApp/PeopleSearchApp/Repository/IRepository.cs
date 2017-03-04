using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearchApp.Repository
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(object Id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object Id);
        void Save();


    }
}