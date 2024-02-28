using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QlSach.Dto;
using QlSach.Interface;
using QlSach.Interfaces;
using QlSach.Model;

namespace QlSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuDeController : Controller
    {
        public readonly IChuDeRepository _chuDeRepository;
        public readonly IMapper _mapper;
        public readonly ISachRepository _sachRepository;
        public ChuDeController(IChuDeRepository chuDeRepository, IMapper mapper, ISachRepository sachRepository)
        {
            _chuDeRepository = chuDeRepository;
            _mapper = mapper;
            _sachRepository = sachRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var chudeList = _chuDeRepository.GetAll();
                if(chudeList==null || chudeList.Count == 0)
                {
                    return NotFound();
                }
                var chudeDtoList = _mapper.Map<List<ChuDeDto>>(chudeList);
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(chudeDtoList);

            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "lỗi: " + ex.Message);
            }

        }
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(ChuDe))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(int id)
        {
            try
            {
                if (!_chuDeRepository.Exits(id))
                {
                    return NotFound();
                }
                var chude = _chuDeRepository.GetById(id);
                if(chude == null)
                {
                    return NotFound();
                }
                var chudeDto = _mapper.Map<ChuDeDto>(chude);
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(chudeDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"lỗi: "+ ex.Message);
            }

        }
        [HttpGet("Sach/{chudeId}")]
        [ProducesResponseType(200,Type = typeof(ChuDe))]
        [ProducesResponseType(400)]
        public IActionResult GetSachByChuDe(int chudeId)
        {
            
            try
            {
                var chude = _chuDeRepository.GetSachByChuDe(chudeId);
                if (chude == null || chude.Count == 0)
                {
                    return NotFound();
                }
                var sach = _mapper.Map<List<SachDto>>(_chuDeRepository.GetSachByChuDe(chudeId));
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(sach);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "lỗi: " + ex.Message);
            }

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromQuery] int sachId, [FromForm] ChuDeDto chudeCreate)
        {
            try
            {
                // Kiểm tra chu đề có tồn tại và ModelState hợp lệ không
                if (chudeCreate == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Trả về BadRequest nếu dữ liệu không hợp lệ
                }

                // Kiểm tra xem sách có tồn tại không
                var sach = _sachRepository.GetById(sachId);
                if (sach == null)
                {
                    return NotFound("Không tìm thấy sách."); // Trả về 404 Not Found nếu không tìm thấy sách
                }

                var chude = _mapper.Map<ChuDe>(chudeCreate);
                chude.Sach = sach;

                _chuDeRepository.Create(chude);

                return NoContent(); // Trả về 204 No Content nếu tạo thành công
            }
            catch (Exception ex)
            {
                // Log và trả về lỗi 500 nếu có ngoại lệ xảy ra
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromForm] ChuDeDto updateChuDe)
        {
            try
            {
                // Kiểm tra xem chủ đề có tồn tại không
                if (!_chuDeRepository.Exits(id))
                {
                    return NotFound();
                }

                // Kiểm tra tính hợp lệ của dữ liệu đầu vào
                if (updateChuDe == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Lấy chủ đề từ cơ sở dữ liệu
                var chude = _chuDeRepository.GetById(id);
                if (chude == null)
                {
                    return NotFound();
                }

                // Cập nhật thông tin chủ đề
                chude.TenChuDe = updateChuDe.TenChuDe;
                _chuDeRepository.Update(chude);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception và trả về lỗi 500 nếu có ngoại lệ xảy ra
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id) {
            try
            {
                var chude = _chuDeRepository.GetById(id);
                if(chude == null)
                {
                    return NotFound();
                }
                _chuDeRepository.Delete(chude);
                return NoContent();
            }
            catch(Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, "lỗi: " + ex.Message);
            }
        }


    }
}
