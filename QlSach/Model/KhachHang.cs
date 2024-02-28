using System.ComponentModel.DataAnnotations;

namespace QlSach.Model
{
    public class KhachHang
    {
        [Key] public int MaKH { get; set; }
        public string TenKH { get; set; }
        [Required]
        public string TaiKhoan { get; set; }
        [Required]
        public string MatKhau { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        [MaxLength(10)]
        public string DienThoai { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email {  get; set; }
        public DonHang DonHang { get; set; }

    }
}
