
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QlSach.Dto;
using QlSach.Interfaces;
using QlSach.Model;

namespace QlSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SachController:Controller
    {
        public readonly ISachRepository _sachRepository;
        public readonly IMapper _mapper;
        public SachController(ISachRepository sachRepository, IMapper mapper)
        {
            _sachRepository = sachRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll() {
            try
            {
                var sachList = _sachRepository.GetAll();
                if (sachList == null || sachList.Count == 0)
                {
                    return NotFound(); // Trả về 404 Not Found nếu không có dữ liệu sách
                }

                var sachDtoList = _mapper.Map<List<SachDto>>(sachList);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(sachDtoList);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SachDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(int id)
        {
            try
            {
                if (!_sachRepository.Exits(id))
                {
                    return NotFound(); // Trả về 404 Not Found nếu sách không tồn tại
                }

                var sach = _sachRepository.GetById(id);
                if (sach == null)
                {
                    return NotFound(); // Trả về 404 Not Found nếu không tìm thấy sách
                }

                var sachDto = _mapper.Map<SachDto>(sach);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Trả về 400 Bad Request nếu dữ liệu không hợp lệ
                }

                return Ok(sachDto); // Trả về sách nếu tất cả các điều kiện đều hợp lệ
            }
            catch (Exception ex)
            {
                // Log và trả về lỗi 500 nếu có ngoại lệ xảy ra
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromForm] SachDto sachCreate)
        {
            if (sachCreate == null)
            {
                return BadRequest("SachDto cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var sach = _mapper.Map<Sach>(sachCreate);
                _sachRepository.Create(sach);

                // Trả về mã trạng thái 204 khi tạo mới thành công
                return NoContent();
            }
            catch(Exception ex) 
            {
                  return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: " + ex.Message);
            }

        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromForm] SachDto sachUpdate)
        {
            try
            {
                if (!_sachRepository.Exits(id))
                {
                    return NotFound();
                }

                var sach = _sachRepository.GetById(id);
                if (sach == null)
                {
                    return NotFound();
                }

                //Cập nhật các trường của đối tượng Sach từ SachDto
                sach.TenSach = sachUpdate.TenSach;
                sach.SoLuong = sachUpdate.SoLuong;
                sach.NgayCapNhat = sachUpdate.NgayCapNhat;
                sach.GiaBan = sachUpdate.GiaBan;

                _sachRepository.Update(sach);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: " + ex.Message);
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
                var sach = _sachRepository.GetById(id);
                if (sach == null)
                {
                    return NotFound(); // Trả về 404 Not Found nếu không tìm thấy sách
                }

                _sachRepository.Delete(sach);

                return NoContent(); // Trả về 204 No Content khi xóa thành công
            }
            catch (Exception ex)
            {
                // Log exception và trả về lỗi 500
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: " + ex.Message);
            }

        }
    }
}
