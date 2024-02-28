using System.ComponentModel.DataAnnotations;

namespace QlSach.Model
{
    public class TacGia
    {
        [Key]
        public int MaTacGia { get; set; }
        public string TenTacGia { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi {  get; set; }
        public ICollection<ThamGia> ThamGias { get; set;}

    }
}
