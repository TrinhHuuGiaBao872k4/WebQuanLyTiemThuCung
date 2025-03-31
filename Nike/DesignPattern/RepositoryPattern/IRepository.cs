using Nike.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nike.DesignPattern.RepositoryPattern
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
