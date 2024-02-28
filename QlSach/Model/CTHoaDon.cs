using System.ComponentModel.DataAnnotations;

namespace QlSach.Model
{
    public class CTHoaDon
    {
        public int MaDH {  get; set; }
        public int MaSach { get; set; }
        public DonHang DonHang { get; set; }
        public Sach Sach { get; set; }
        public int SoLuong { get; set; }
        public float DonGia { get; set; }

    }
}
