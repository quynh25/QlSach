using QlSach.Model;

namespace QlSach.Interfaces
{
    public interface ITacGiaRepository
    {
        ICollection<TacGia> GetAll();
        TacGia GetById(int id);
        bool Exits(int id);
        bool Create(TacGia tacGia);
        bool Update(TacGia tacGia);
        bool Delete(TacGia tacGia);
        bool Save();
    }
}
