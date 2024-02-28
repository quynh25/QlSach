using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QlSach.Dto;
using QlSach.Interfaces;
using QlSach.Model;

namespace QlSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacGiaController:Controller
    {
        private readonly ITacGiaRepository _tacGiaRepository;
        private readonly IMapper _mapper;

        public TacGiaController(ITacGiaRepository tacGiaRepository, IMapper mapper)
        {
            _tacGiaRepository= tacGiaRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll() {
            try
            {
                var tacgialist = _tacGiaRepository.GetAll();
                if( tacgialist == null || tacgialist.Count==0 )
                {
                    return NotFound();
                }
                var tacgia = _mapper.Map<List<TacGiaDto>>(tacgialist);
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(tacgia);

            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Lỗi: "+ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(TacGiaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id) {
            try
            {
                if (!_tacGiaRepository.Exits(id))
                {
                    return NotFound();
                }
                var tacgia = _tacGiaRepository.GetById(id);
                if(tacgia == null)
                {
                    return NotFound();
                }
                var tacgiaDto = _mapper.Map<TacGiaDto>(tacgia);
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(tacgiaDto);
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Lỗi: "+ ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromForm]  TacGiaDto tacgia)
        {
           
            try
            {
                if (tacgia == null)
                {
                    return BadRequest("khong thể null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var tacgiaDto = _mapper.Map<TacGia>(tacgia);
                _tacGiaRepository.Create(tacgiaDto);
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromForm] TacGiaDto tacGiaUpdate)
        {
            try
            {
                if (!_tacGiaRepository.Exits(id))
                {
                    return NotFound();
                }
                var tacgia = _tacGiaRepository.GetById(id);
                if(tacgia == null)
                {
                    return NotFound();
                }
                tacgia.TenTacGia = tacGiaUpdate.TenTacGia;
                tacgia.DienThoai = tacGiaUpdate.DienThoai;
                tacgia.DiaChi = tacGiaUpdate.DiaChi;

                _tacGiaRepository.Update(tacgia);
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
                var tacgia = _tacGiaRepository.GetById(id);
                if(tacgia == null)
                {
                    return NotFound();
                }
                _tacGiaRepository.Delete(tacgia);
                return NoContent();

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Lỗi: "+ ex.Message);
            }
        }
    }
}
