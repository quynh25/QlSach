using QlSach.Data;
using QlSach.Interfaces;
using QlSach.Model;
using System.Reflection.PortableExecutable;

namespace QlSach.Repository
{
    public class SachRepository : ISachRepository
    {
        private readonly DataContext _context;
        public SachRepository(DataContext context)
        {
            _context = context;
        }
        public bool Create(Sach sach)
        {
            _context.Add(sach);
            return Save();
        }

        public bool Delete(Sach sach)
        {
            _context.Remove(sach);
            return Save();
        }

        public bool Exits(int id)
        {
            return _context.Saches.Any(x => x.MaSach == id);
        }

        public ICollection<Sach> GetAll()
        {
            return _context.Saches.OrderBy(s => s.MaSach).ToList();
        }

        public Sach GetById(int id)
        {
            return _context.Saches.Where(s=>s.MaSach == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Sach sach)
        {
            _context.Update(sach);
            return Save();
        }
    }
}
