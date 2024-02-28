using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QlSach.Dto;
using QlSach.Interfaces;
using QlSach.Model;
using QlSach.Repository;

namespace QlSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NXBController:Controller
    {
        private readonly INXBRepository _nXBRepository;
        private readonly IMapper _mapper;
        private readonly ISachRepository _sachRepository;

        public NXBController(INXBRepository nXBRepository, IMapper mapper, ISachRepository sachRepository) {
            _nXBRepository = nXBRepository;
            _mapper = mapper;
            _sachRepository = sachRepository;
        }
        [HttpGet]
        public IActionResult GetAll() {
            try
            {
                var nxb = _nXBRepository.GetAll();
                if (nxb == null || nxb.Count == 0)
                {
                    return NotFound();
                }
                var nxbList = _mapper.Map<List<NXBDto>>(nxb);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(nxbList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
           
        }
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NhaXuatBan))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(int id)
        {
            try
            {
                if (!_nXBRepository.Exits(id)){
                    return NotFound();
                }
                var nxb = _nXBRepository.GetById(id);
                if(nxb== null)
                {
                    return NotFound();
                }
                var nxbDto = _mapper.Map<NXBDto>(nxb);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(nxbDto);

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }

        }
        [HttpGet("Sach/{nxbId}")]
        [ProducesResponseType(200,Type = typeof(NhaXuatBan))]
        [ProducesResponseType(400)]
        public IActionResult GetSachByNXB(int nxbId)
        {
            try
            {
                var nxb = _nXBRepository.GetSachByNXB(nxbId);
                if(nxb == null || nxb.Count == 0)
                {
                    return NotFound();
                }
                var sach = _mapper.Map<List<SachDto>>(_nXBRepository.GetSachByNXB(nxbId));
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(sach);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromQuery] int sachId, [FromForm] NXBDto nXBCreate)
        {
            try
            {
                if(nXBCreate == null || !ModelState.IsValid)
                {
                    return NotFound();
                }
                var sach = _sachRepository.GetById(sachId);
                if(sach == null)
                {
                    return NotFound("Khong tim thay sach");
                }
                var nxb = _mapper.Map<NhaXuatBan>(nXBCreate);
                nxb.Sach= sach;
                _nXBRepository.Create(nxb);
                return NoContent();

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromForm] NXBDto updateNXB)
        {
            try
            {
                if (!_nXBRepository.Exits(id))
                {
                    return NotFound();
                }
                if(updateNXB == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var nxb = _nXBRepository.GetById(id);
                if(nxb == null)
                {
                    return NotFound();
                }
                nxb.TenNXB = updateNXB.TenNXB;
                nxb.DienThoai = updateNXB.DienThoai;
                nxb.DiaChi = updateNXB.DiaChi;
                _nXBRepository.Update(nxb);
                return NoContent();

            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                var nxb = _nXBRepository.GetById(id);
                if(nxb == null)
                {
                    return NotFound();
                }
                _nXBRepository.Delete(nxb);
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }
    }
}
