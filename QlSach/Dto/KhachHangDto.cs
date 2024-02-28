using QlSach.Model;
using System.ComponentModel.DataAnnotations;

namespace QlSach.Dto
{
    public class KhachHangDto
    {
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }

    }
}
