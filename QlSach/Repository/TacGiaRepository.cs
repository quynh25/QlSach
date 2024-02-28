using QlSach.Data;
using QlSach.Interfaces;
using QlSach.Model;

namespace QlSach.Repository
{
    public class TacGiaRepository : ITacGiaRepository
    {
        private readonly DataContext _context;

        public TacGiaRepository(DataContext context)
        {
            _context = context;
        }
        public bool Create(TacGia tacGia)
        {
            _context.Add(tacGia);
            return Save();
        }

        public bool Delete(TacGia tacGia)
        {
            _context.Remove(tacGia);
            return Save();
        }

        public bool Exits(int id)
        {
            return _context.TacGias.Any(x=>x.MaTacGia == id);
        }

        public ICollection<TacGia> GetAll()
        {
            return _context.TacGias.OrderBy(x=>x.MaTacGia).ToList();
        }

        public TacGia GetById(int id)
        {
            return _context.TacGias.Where(x => x.MaTacGia == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0 ? true: false;
        }

        public bool Update(TacGia tacGia)
        {
            _context.Update(tacGia);
            return Save();
        }
    }

}
