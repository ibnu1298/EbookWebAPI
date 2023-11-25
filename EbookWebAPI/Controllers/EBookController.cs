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
        [HttpPost("Disable")]
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
        [HttpPost("Enable")]
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
                    ebook.Message = "Link E-Book Berhasil Didaftarkan";
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
                var dupDisable = await _ebook.CheckDuplicateSKUButDisable(data);
                if (dupDisable.Count() > 0)
                {
                    ebook.IsSucceeded = true;
                    ebook.Message = $"Insert Failed, {dupDisable.Count()} Link E-Book is registered but disabled";
                    ebook.EBooks = _mapper.Map<List<ReadEBookDTO>>(dupDisable);
                    return BadRequest(ebook);
                }
                var duplicate = await _ebook.CheckDuplicate(data);
                if (duplicate.Count() > 0)
                {
                    ebook.IsSucceeded = true;
                    ebook.Message = $"Insert Failed, {duplicate.Count()} Link E-Book already registered";
                    ebook.EBooks = _mapper.Map<List<ReadEBookDTO>>(duplicate);
                    return BadRequest(ebook);
                }
                if (duplicate != null) { }
                var result = await _ebook.InsertMultiple(data);
                if(result != null)
                {
                    ebook.IsSucceeded = true;
                    ebook.Message = $"{result.Count()} Link E-Book Berhasil Ditambahkan";
                    ebook.EBooks = _mapper.Map<List<ReadEBookDTO>>(result);
                }
                return Ok(ebook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("GetMultipleEBookBySKU")]
        public async Task<ActionResult> GetMultipleLinkEbookBySKU(ReadSKU[] obj)
        {
            ReadMultipleSKU getSKU = new ReadMultipleSKU();
            try
            {
                var result = await _ebook.GetsBySKU(obj);
                if(result.Count() > 0)
                {
                    ebook.IsSucceeded = true;
                    ebook.Message = $"Berhasil Mengambil {result.Count()} Link E-Book";
                    ebook.EBooks = _mapper.Map<List<ReadEBookDTO>>(result);
                }
                else
                {
                    ebook.IsSucceeded = true;
                    ebook.Message = $"Link E-Book Not Found";
                    ebook.EBooks = null;
                    return NotFound(ebook);
                }
                return Ok(ebook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("GetMultipleEBookByName")]
        public async Task<ActionResult> GetMultipleLinkEbookByName(BookNameDTO obj)
        {
            try
            {
                var result = await _ebook.GetByName(obj.BookName);
                if (result.Count() > 0)
                {
                    ebook.IsSucceeded = true;
                    ebook.Message = $"Berhasil Mengambil {result.Count()} Link E-Book";
                    ebook.EBooks = _mapper.Map<List<ReadEBookDTO>>(result);
                }
                else
                {
                    ebook.IsSucceeded = false;
                    ebook.Message = $"Link E-Book Not Found";
                    ebook.EBooks = null;
                    return NotFound(ebook);
                }
                return Ok(ebook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Edit")]
        public async Task<ActionResult> EditEBook(AddEBookDTO obj)
        {
            try
            {
                var mapping = _mapper.Map<LinkEbook>(obj);
                var edit = await _ebook.UpdateEbook(mapping);
                var ebook = _mapper.Map<ReadSingleEBookDTO>(edit);
                if (edit != null)
                {
                    ebook.IsSucceeded = true;
                    ebook.Message = "Edit Data E-Book Berhasil";
                }
                return Ok(ebook);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("GetAllDuplicate")]
        public async Task<ActionResult> GetAllDup(AddMultipleEBookDTO obj)
        {
            try
            {
                var data = _mapper.Map<LinkEbook[]>(obj.Data);
                var results = await _ebook.CheckDuplicate(data);
                if (results.Count() > 0)
                {
                    ebook.IsSucceeded = true;
                    ebook.Message = $"Berhasil Mengambil {results.Count()} E-Book Duplicate";
                    ebook.EBooks = _mapper.Map<List<ReadEBookDTO>>(results);
                }
                else
                {
                    ebook.IsSucceeded = false;
                    ebook.Message = "Tidak Ada Data Duplicate";
                    ebook.EBooks = null;
                    return NotFound(ebook);
                }
                return Ok(ebook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Paging/{page}/{take}")]
        public async Task<ActionResult> EbookPaging(BookNameDTO obj, int page, float take)
        {
            try
            {
                ReadMultipleEBookDTOPage readMultipleEBookDTOPage = new ReadMultipleEBookDTOPage();
                var results = await _ebook.GetByName(obj.BookName);
                if (_context.LinkEbooks == null)
                    return NotFound();
                var pageResults = take;
                var pageCount = Math.Ceiling(_context.LinkEbooks.Count() / pageResults);
                var ebooks = results.Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();
                readMultipleEBookDTOPage.Message = $"Berhasil Memngambil {results.Count()} Ebook";
                readMultipleEBookDTOPage.IsSucceeded = true;
                readMultipleEBookDTOPage.CurrentPage = page;
                readMultipleEBookDTOPage.Pages = (int)pageCount;
                readMultipleEBookDTOPage.EBooks = _mapper.Map<List<ReadEBookDTO>>(ebooks);
                return Ok(readMultipleEBookDTOPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
