﻿using EbookWebAPI.Dtos.Ebook;
using EbookWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EbookWebAPI.DAL
{
    public interface IEbook : ICrud<LinkEbook>
    {
        Task<LinkEbook[]> InsertMultiple(LinkEbook[] obj);
        Task<IEnumerable<LinkEbook>> GetAllOrderByName();
        Task<IEnumerable<LinkEbook>> GetAllOrderBySKU();
        Task<LinkEbook> DisableLinkEBook(int sku);
        Task<LinkEbook> EnableLinkEBook(int sku);
        Task<LinkEbook> UpdateEbook(LinkEbook linkEbook);
        Task<List<LinkEbook>> GetsBySKU(ReadSKU[] obj);
        Task<IEnumerable<LinkEbook>> CheckDuplicate(LinkEbook[] obj);
        Task<IEnumerable<LinkEbook>> CheckDuplicateSKUButDisable(LinkEbook[] obj);

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
        public async Task<IEnumerable<LinkEbook>> GetByName(string name)
        {
            try
            {
                var result = await _context.LinkEbooks.Where(x => x.BookName.Contains(name)).ToListAsync();
                if (result.Count <= 0) throw new Exception($"Error : Buku dengan nama {name} tidak ditemukan");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
        public async Task<LinkEbook> UpdateEbook(LinkEbook linkEbook)
        {
            try
            {
                var edit = await _context.LinkEbooks.FirstOrDefaultAsync(c => c.SKU == linkEbook.SKU && linkEbook.RowStatus == 0);
                if (edit == null)
                    throw new Exception($"Error: Data Tidak ditemukan");
                if (!string.IsNullOrEmpty(linkEbook.BookName)) edit.BookName = linkEbook.BookName;
                if (!string.IsNullOrEmpty(linkEbook.Link)) edit.Link = linkEbook.Link;
                await _context.SaveChangesAsync();
                return edit;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<LinkEbook>> GetsBySKU(ReadSKU[] obj)
        {
            try
            {
                List<LinkEbook> eBooks = new List<LinkEbook>();
                foreach (var item in obj)
                {
                    var result = await _context.LinkEbooks.FirstOrDefaultAsync(x => x.SKU == item.SKU && x.RowStatus == 0);
                    if (result != null)
                    {
                        if (eBooks.Contains(eBooks.FirstOrDefault(x => x.SKU == item.SKU))) eBooks.Remove(result);
                        eBooks.Add(result);                       
                    }
                }
                return eBooks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<LinkEbook>> CheckDuplicate(LinkEbook[] obj)
        {
            try
            {
                List<LinkEbook> emails = new List<LinkEbook>();
                foreach (var item in obj)
                {
                    var result = await _context.LinkEbooks.FirstOrDefaultAsync(x => x.SKU == item.SKU);
                    if (result != null) emails.Add(result);
                }
                return emails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<LinkEbook>> CheckDuplicateSKUButDisable(LinkEbook[] obj)
        {
            try
            {
                List<LinkEbook> emails = new List<LinkEbook>();
                foreach (var item in obj)
                {
                    var result = await _context.LinkEbooks.FirstOrDefaultAsync(x => x.SKU == item.SKU && x.RowStatus == 1);
                    if (result != null) emails.Add(result);
                }
                return emails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
