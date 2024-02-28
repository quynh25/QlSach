using QlSach.Data;
using QlSach.Interfaces;
using QlSach.Model;

namespace QlSach.Repository
{
    public class NXBRepository : INXBRepository
    {
        private readonly DataContext _context;
        public NXBRepository(DataContext context)
        {
            _context = context;
        }
        public bool Create(NhaXuatBan nhaXuatBan)
        {
            _context.Add(nhaXuatBan);
            return Save();
        }

        public bool Delete(NhaXuatBan nhaXuatBan)
        {
            _context.Remove(nhaXuatBan);
            return Save();
        }

        public bool Exits(int id)
        {
            return _context.NhaXuatBans.Any(x => x.MaNXB == id);
        }

        public ICollection<NhaXuatBan> GetAll()
        {
            return _context.NhaXuatBans.OrderBy(x=>x.MaNXB).ToList();
        }

        public NhaXuatBan GetById(int id)
        {
            return _context.NhaXuatBans.Where(x => x.MaNXB == id).FirstOrDefault();
        }

        public ICollection<Sach> GetSachByNXB(int nxbId)
        {
            return _context.NhaXuatBans.Where(x=>x.MaNXB==nxbId).Select(x=>x.Sach).ToList();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0 ? true : false;
        }

        public bool Update(NhaXuatBan nhaXuatBan)
        {
            _context.Update(nhaXuatBan);
            return Save();
        }
    }
}
