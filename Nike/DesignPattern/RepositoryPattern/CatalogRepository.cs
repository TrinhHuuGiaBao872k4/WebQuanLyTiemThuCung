using Nike.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace Nike.DesignPattern.RepositoryPattern
{
    public class CatalogRepository : IRepository<Catalog>
    {
        private QuanLySanPhamEntities _db;

        // Constructor: Truyền DbContext từ ngoài vào (dependency injection)
        public CatalogRepository(QuanLySanPhamEntities db)
        {
            _db = db;
        }

        public IEnumerable<Catalog> GetAll() => _db.Catalogs.ToList();

        public Catalog GetById(int id) => _db.Catalogs.Find(id);

        public void Add(Catalog entity)
        {
            _db.Catalogs.Add(entity);
        }

        public void Update(Catalog entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Catalog entity)
        {
            _db.Catalogs.Remove(entity);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
 }