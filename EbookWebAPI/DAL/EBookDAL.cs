using EbookWebAPI.Dtos.Ebook;
using EbookWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EbookWebAPI.DAL
{
    public interface IEbook : ICrud<LinkEbook>
    {
        Task<LinkEbook[]> InsertMultiple(LinkEbook[] obj);
        Task<IEnumerable<LinkEbook>> GetAllOrderByName();
        Task<IEnumerable<LinkEbook>> GetAllOrderBySKU();
        Task<LinkEbook> DisableLinkEBook(int sku);
        Task<LinkEbook> EnableLinkEBook(int sku);

    }

    public class EBookDAL : IEbook
    {
        private readonly DataContext _context;

        public EBookDAL(DataContext context)
        {
            _context = context;
        }
        #region unused
        public async Task<IEnumerable<LinkEbook>> GetAll()
        {
            return null;
        }
        public Task<IEnumerable<LinkEbook>> GetByName(string name)
        {
            return GetAll();
        }
        public Task<LinkEbook> GetById(int id)
        {
            return GetById(id);
        }
        public Task<LinkEbook> Update(LinkEbook obj)
        {
            return Update(obj);
        }
        public Task Delete(int id)
        {
            return Delete(id);
        }
        #endregion

        public async Task<IEnumerable<LinkEbook>> GetAllOrderByName()
        {
            try
            {
                var results = await _context.LinkEbooks.OrderBy(x => x.BookName).Where(x => x.RowStatus == 0).ToListAsync();
                if (!(results.Count > 0))
                    throw new Exception("Error: Belum ada data");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<IEnumerable<LinkEbook>> GetAllOrderBySKU()
        {
            try
            {
                var results = await _context.LinkEbooks.OrderBy(x => x.SKU).Where(x => x.RowStatus == 0).ToListAsync();
                if (!(results.Count > 0))
                    throw new Exception("Error: Belum ada data");
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<LinkEbook> Insert(LinkEbook obj)
        {
            try
            {
                var result = await _context.LinkEbooks.FirstOrDefaultAsync(x => x.SKU == obj.SKU);
                if (result != null) throw new Exception($"Error: SKU {obj.SKU} - Sudah Ada \neBook Name: {obj.BookName}");
                _context.LinkEbooks.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<LinkEbook[]> InsertMultiple(LinkEbook[] obj)
        {
            try
            {
                foreach (var item in obj)
                {
                   var result = await _context.LinkEbooks.FirstOrDefaultAsync(x => x.SKU == item.SKU);
                   if (result != null) throw new Exception($"Error: SKU {item.SKU} - Sudah Ada \neBook Name: {item.BookName}");
                   _context.LinkEbooks.Add(item);                    
                }
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message );
            }
            
        }
        public async Task<LinkEbook> DisableLinkEBook(int sku)
        {
            try
            {
                var check = await _context.LinkEbooks.FirstOrDefaultAsync(x => x.SKU == sku && x.RowStatus == 0);
                if (check == null) throw new Exception($"Error: Data Tidak Ditemukan");
                check.RowStatus = 1;
                await _context.SaveChangesAsync();
                return check;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<LinkEbook> EnableLinkEBook(int sku)
        {
            try
            {
                var check = await _context.LinkEbooks.FirstOrDefaultAsync(x => x.SKU == sku && x.RowStatus == 1);
                if (check == null) throw new Exception($"Error: Data Tidak ditemukan");
                check.RowStatus = 0;
                await _context.SaveChangesAsync();
                return check;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }


    }
}
