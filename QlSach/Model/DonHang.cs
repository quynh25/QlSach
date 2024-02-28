using System.ComponentModel.DataAnnotations;

namespace QlSach.Model
{
    public class DonHang
    {
        [Key]
        public int MaDH { get; set; }
        public string TTDH { get; set; }    
        public DateTime NgayGiao { get; set; }
        public ICollection<KhachHang> KhachHangs { get; set; }
        public ICollection<CTHoaDon> CTHoaDons { get; set; }
    }
}
