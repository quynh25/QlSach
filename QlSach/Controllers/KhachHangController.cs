using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QlSach.Dto;
using QlSach.Interfaces;
using QlSach.Model;

namespace QlSach.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class KhachHangController:Controller
    {
        private readonly IKhachHangRepository _khachHangRepository;
        private readonly IDonHangRepository _donHangRepository;
        private readonly IMapper _mapper;

        public KhachHangController(IKhachHangRepository khachHangRepository,IMapper mapper, IDonHangRepository donHangRepository) {
            _khachHangRepository = khachHangRepository;
            _donHangRepository = donHangRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll() {
            try
            {
                var khachhang = _khachHangRepository.GetAll();
                if(khachhang==null|| khachhang.Count() == 0)
                {
                    return NotFound();
                }
                var khachhangList = _mapper.Map<List<KhachHangDto>>(khachhang);
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                return Ok(khachhangList);
            }catch (Exception ex)
            {
               return StatusCode(StatusCodes.Status500InternalServerError, "lỗi: " + ex.Message);
            }
        }
        [HttpGet("id")]
        public IActionResult GetById(int id) {
            try
            {
                if (!_khachHangRepository.Exits(id))
                {
                    return NotFound();
                }
                var kh = _khachHangRepository.GetById(id);
                if(kh == null)
                {
                    return NotFound();
                }
                var khdto = _mapper.Map<KhachHangDto>(kh);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(khdto);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }
        [HttpGet("DH/{KHId}")]
        public IActionResult GetDHByKh(int KHId)
        {
            try
            {
                var kh = _khachHangRepository.GetDHByKH(KHId);
                if(kh == null || kh.Count == 0)
                {
                    return NotFound();
                }
                var dh = _mapper.Map<List<DonHangDto>>(_khachHangRepository.GetDHByKH(KHId));
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(dh);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Create([FromQuery] int DhId, [FromForm]KhachHangDto khcreate)
        {
            try
            {
                if(khcreate == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var dh = _donHangRepository.GetById(DhId);
                if(dh == null)
                {
                    return NotFound("khong tim thay don hang");
                }
                var kh = _mapper.Map<KhachHang>(khcreate);
                kh.DonHang = dh;

                _khachHangRepository.Create(kh);
                return NoContent();

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] KhachHangDto updateKH)
        {
            try
            {
                if (!_khachHangRepository.Exits(id))
                {
                    return NotFound();
                }
                if(updateKH == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var kh = _khachHangRepository.GetById(id);
                if(kh == null)
                {
                    return NotFound();
                }
                kh.TenKH = updateKH.TenKH;
                kh.TaiKhoan = updateKH.TaiKhoan;
                kh.MatKhau = updateKH.MatKhau;
                kh.DiaChi = updateKH.DiaChi;
                kh.NgaySinh = updateKH.NgaySinh;
                kh.GioiTinh = updateKH.GioiTinh;
                kh.DienThoai = updateKH.DienThoai;
                kh.Email = updateKH.Email;

                _khachHangRepository.Update(kh);
                return NoContent();
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var kh = _khachHangRepository.GetById(id);
                if (kh == null)
                {
                    return NotFound();
                }
                _khachHangRepository.Delete(kh);
                return NoContent();

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }
    }
}
