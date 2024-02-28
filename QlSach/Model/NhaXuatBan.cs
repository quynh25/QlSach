using System.ComponentModel.DataAnnotations;

namespace QlSach.Model
{
    public class NhaXuatBan
    {
        [Key]
        public int MaNXB {  get; set; }
        public string TenNXB { get; set; }
        [MaxLength(10)]
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public Sach Sach { get; set; }
    }
}
