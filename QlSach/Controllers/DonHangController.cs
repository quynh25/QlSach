using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QlSach.Dto;
using QlSach.Interfaces;
using QlSach.Model;

namespace QlSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangController:Controller
    {
        private readonly IDonHangRepository _donHangRepository;
        private readonly IMapper _mapper;

        public DonHangController(IDonHangRepository donHangRepository, IMapper mapper) {
            _donHangRepository = donHangRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll() {
            try
            {
                var donhangList = _donHangRepository.GetAll();
                if(donhangList==null || donhangList.Count==0) {
                    return NotFound();
                }
                var donhang = _mapper.Map<List<DonHangDto>>(donhangList);
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(donhang);

            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type= typeof(DonHang))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id) {
            try
            {
                if (!_donHangRepository.Exits(id))
                {
                    return NotFound();
                }
                var donhang = _donHangRepository.GetById(id);
                if(donhang==null)
                {
                    return NotFound();
                }
                var donhangDto = _mapper.Map<DonHangDto>(donhang);
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(donhangDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi " + ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromForm] DonHangDto donhangCreate)
        {
            try
            {
                if (donhangCreate == null)
                {
                    return BadRequest("ko được để trống");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var donhang = _mapper.Map<DonHang>(donhangCreate);
                _donHangRepository.Create(donhang);
                return NoContent();

            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromForm] DonHangDto donhangUpdate)
        {
            try
            {
                if (!_donHangRepository.Exits(id))
                {
                    return NotFound();
                }
                var donhang = _donHangRepository.GetById(id);
                if(donhang == null)
                {
                    return NotFound();
                }
                //
                donhang.TTDH = donhangUpdate.TTDH;
                donhang.NgayGiao = donhang.NgayGiao;
                _donHangRepository.Update(donhang);
                return NoContent();
            }catch(Exception ex)
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
                var donhang = _donHangRepository.GetById(id);
                if(donhang == null)
                {
                    return NotFound();
                }
                _donHangRepository.Delete(donhang);
                return NoContent();

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }

    }
}
