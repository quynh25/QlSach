using AutoMapper;
using QlSach.Dto;
using QlSach.Model;

namespace QlSach.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() {
            CreateMap<ChuDe, ChuDeDto>();
            CreateMap<ChuDeDto, ChuDe>();
            CreateMap<Sach, SachDto>();
            CreateMap<SachDto, Sach>();
            CreateMap<NhaXuatBan, NXBDto>();
            CreateMap<NXBDto, NhaXuatBan>();
            CreateMap<TacGia, TacGiaDto>();
            CreateMap<TacGiaDto, TacGia>();
            CreateMap<DonHang,DonHangDto>();
            CreateMap<DonHangDto,DonHang>();
            CreateMap<KhachHang,KhachHangDto>();
            CreateMap<KhachHangDto,KhachHang>();

        }
    }
}
