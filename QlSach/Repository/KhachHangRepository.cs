using QlSach.Data;
using QlSach.Interfaces;
using QlSach.Model;

namespace QlSach.Repository
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly DataContext _context;

        public KhachHangRepository(DataContext context) {
            _context = context;
        }
        public bool Create(KhachHang khachHang)
        {
            _context.Add(khachHang);
            return Save();
        }

        public bool Delete(KhachHang khachHang)
        {
            _context.Remove(khachHang);
            return Save();
        }

        public bool Exits(int id)
        {
            return _context.KhachHangs.Any(x => x.MaKH == id);
        }

        public ICollection<KhachHang> GetAll()
        {
            return _context.KhachHangs.OrderBy(x => x.MaKH).ToList();
        }

        public KhachHang GetById(int KHId)
        {
            return _context.KhachHangs.Where(x => x.MaKH == KHId).FirstOrDefault();
        }

        public ICollection<DonHang> GetDHByKH(int KHId)
        {
            return _context.KhachHangs.Where(x => x.MaKH == KHId).Select(x => x.DonHang).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0 ? true: false;
        }

        public bool Update(KhachHang khachHang)
        {
            _context.Update(khachHang);
            return Save();
        }
    }
}
