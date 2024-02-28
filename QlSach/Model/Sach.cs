using System.ComponentModel.DataAnnotations;

namespace QlSach.Model
{
    public class Sach
    {
        [Key]
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public float GiaBan { get; set; }
        public ICollection<ChuDe> ChuDes { get; set; }
        public ICollection<CTHoaDon> CTHoaDons { get; set; }
        public ICollection<ThamGia> ThamGias { get; set; }
        public ICollection<NhaXuatBan> NhaXuatBans { get; set; }
    }
}
