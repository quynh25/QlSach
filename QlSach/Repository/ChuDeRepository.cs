using QlSach.Data;
using QlSach.Interface;
using QlSach.Model;

namespace QlSach.Repository
{
    public class ChuDeRepository : IChuDeRepository
    {
        private readonly DataContext _context;

        public ChuDeRepository(DataContext context) {
            _context = context;
        }
        public bool Create(ChuDe chuDe)
        {
            _context.Add(chuDe);
            return Save();
        }

        public bool Delete(ChuDe chuDe)
        {
            _context.Remove(chuDe);
            return Save();
        }

        public bool Exits(int id)
        {
            return _context.ChuDes.Any(x => x.MaChuDe == id);
        }

        public ICollection<ChuDe> GetAll()
        {
            return _context.ChuDes.OrderBy(p => p.MaChuDe).ToList();
        }

        public ChuDe GetById(int id)
        {
            return _context.ChuDes.Where(s => s.MaChuDe == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0 ? true:false;
        }

        public bool Update(ChuDe chuDe)
        {
            _context.Update(chuDe);
            return Save();
        }

        ICollection<Sach> IChuDeRepository.GetSachByChuDe(int chude)
        {
            return _context.ChuDes.Where(c=>c.MaChuDe== chude).Select(c=>c.Sach).ToList();
        }
    }
}
