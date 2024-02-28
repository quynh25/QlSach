using QlSach.Data;
using QlSach.Interfaces;
using QlSach.Model;

namespace QlSach.Repository
{
    public class DonHangRepository : IDonHangRepository
    {
        private readonly DataContext _context;

        public DonHangRepository(DataContext context) {
            _context = context;
        }
        public bool Create(DonHang donHang)
        {
            _context.Add(donHang);
            return Save();
        }

        public bool Delete(DonHang donHang)
        {
            _context.Remove(donHang);
            return Save();
        }

        public bool Exits(int id)
        {
            return _context.DonHangs.Any(x => x.MaDH == id);

        }

        public ICollection<DonHang> GetAll()
        {
            return _context.DonHangs.OrderBy(x => x.MaDH).ToList();
        }

        public DonHang GetById(int id)
        {
            return _context.DonHangs.Where(x => x.MaDH == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0 ? true: false;
        }

        public bool Update(DonHang donHang)
        {
            _context.Update(donHang);
            return Save();
        }
    }
}
