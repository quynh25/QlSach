using QlSach.Model;

namespace QlSach.Interfaces
{
    public interface ISachRepository
    {
        ICollection<Sach> GetAll();
        Sach GetById(int id);
        bool Exits (int id);
        bool Create (Sach sach);
        bool Update (Sach sach);
        bool Delete (Sach sach);
        bool Save();
        //List<Sach> GetSachByChuDeId(int maCD);
    }
}
