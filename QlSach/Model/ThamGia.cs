using System.ComponentModel.DataAnnotations;

namespace QlSach.Model
{
    public class ThamGia
    {
        public int MaTacGia { get; set; }
        public int MaSach {  get; set; }
        public TacGia TacGia { get; set; }
        public Sach Sach {  get; set; }
        public string VaiTro {  get; set; }
        public string ViTri { get; set; }
    }
}
