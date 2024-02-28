using QlSach.Model;

namespace QlSach.Interface
{
    public interface IChuDeRepository
    {
        ICollection<ChuDe> GetAll();
        ICollection<Sach>GetSachByChuDe(int chude);
        ChuDe GetById(int id);
        bool Exits(int id);
        bool Create (ChuDe chuDe);
        bool Update (ChuDe chuDe);
        bool Delete (ChuDe chuDe);
        bool Save();
    }
}
