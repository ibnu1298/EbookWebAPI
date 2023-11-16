using AutoMapper;
using EbookWebAPI.DAL;
using EbookWebAPI.Dtos.Ebook;
using EbookWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbookWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EBookController : ControllerBase
    {
        private readonly IEbook _ebook;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public EBookController(IEbook ebook, IMapper mapper, DataContext context)
        {
            _ebook = ebook;
            _mapper = mapper;
            _context = context;
        }
        ReadMultipleEBookDTO ebook = new ReadMultipleEBookDTO();
        [HttpPost("Delete")]
        public async Task<ActionResult> DisableLink(ReadSKU sku)
        {
            try
            {
                var results = await _ebook.DisableLinkEBook(sku.SKU);
                var eBook = _mapper.Map<ReadSingleEBookDTO>(results);
                if (results != null)
                {
                    eBook.IsSucceeded = true;
                    eBook.Message = "Link has been Successfully Disabled";
                }
                return Ok(eBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UnDelete")]
        public async Task<ActionResult> EnableLink(ReadSKU sku)
        {
            try
            {
                var results = await _ebook.EnableLinkEBook(sku.SKU);
                var eBook = _mapper.Map<ReadSingleEBookDTO>(results);
                if(results != null)
                {
                    eBook.IsSucceeded = true;
                    eBook.Message = "Link has been Successfully Activated";
                }
                return Ok(eBook);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("OrderByName")]
        public async Task<ActionResult> GetOrderByName()
        {
            try
            {
                var results = await _ebook.GetAllOrderByName();
                if (results != null)
                {
                    ebook.IsSucceeded = true;
                    ebook.Message = "Berhasil mengambil Link berdasarkan Nama";
                    ebook.EBooks = _mapper.Map<List<ReadEBookDTO>>(results);
                }
                return Ok(ebook);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("OrderBySKU")]
        public async Task<ActionResult> GetOrderBySKU()
        {
            try
            {
                var results = await _ebook.GetAllOrderBySKU();
                if (results != null)
                {
                    ebook.IsSucceeded = true;
                    ebook.Message = "Berhasil mengambil Link berdasarkan SKU";
                    ebook.EBooks = _mapper.Map<List<ReadEBookDTO>>(results);
                }
                return Ok(ebook);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddSingleEBook")]
        public async Task<ActionResult> AddLinkEbook(AddEBookDTO obj)
        {
            try
            {
                var newEbook = _mapper.Map<LinkEbook>(obj);
                var result = await _ebook.Insert(newEbook);
                var ebook = _mapper.Map<ReadSingleEBookDTO>(result);
                if(result != null)
                {
                    ebook.IsSucceeded = true;
                    ebook.Message = "Buku Berhasil Didaftarkan";
                }

                return Ok(ebook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddMultipleEBook")]
        public async Task<ActionResult> AddMultipleLinkEbook(AddMultipleEBookDTO obj)
        {
            try
            {                
                var data = _mapper.Map<LinkEbook[]>(obj.Data);
                var result = await _ebook.InsertMultiple(data);
                if(result != null)
                {
                    ebook.IsSucceeded = true;
                    ebook.Message = "Buku Berhasil Ditambahkan";
                    ebook.EBooks = _mapper.Map<List<ReadEBookDTO>>(result);
                }
                return Ok(ebook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
