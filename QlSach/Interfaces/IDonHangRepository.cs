using QlSach.Model;

namespace QlSach.Interfaces
{
    public interface IDonHangRepository
    {
        ICollection<DonHang>GetAll();
        DonHang GetById(int id);
        bool Exits(int id);
        bool Create(DonHang donHang);
        bool Update(DonHang donHang);
        bool Delete(DonHang donHang);
        bool Save();
    }
}
