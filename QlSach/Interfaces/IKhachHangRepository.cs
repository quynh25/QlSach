using QlSach.Model;

namespace QlSach.Interfaces
{
    public interface IKhachHangRepository
    {
        ICollection<KhachHang>GetAll();
        ICollection<DonHang> GetDHByKH(int KHId);
        KhachHang GetById(int KHId);
        bool Exits(int id);
        bool Create(KhachHang khachHang);
        bool Update(KhachHang khachHang);
        bool Delete(KhachHang khachHang);
        bool Save();
    }
}
