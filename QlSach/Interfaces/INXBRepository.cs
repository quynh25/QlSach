using QlSach.Model;

namespace QlSach.Interfaces
{
    public interface INXBRepository
    {
        ICollection<NhaXuatBan> GetAll();
        ICollection<Sach> GetSachByNXB(int sachId);
        NhaXuatBan GetById(int id);
        bool Exits(int id);
        bool Create(NhaXuatBan nhaXuatBan);
        bool Delete(NhaXuatBan nhaXuatBan);
        bool Update(NhaXuatBan nhaXuatBan);
        bool Save();
    }
}
