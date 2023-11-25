using EbookWebAPI.Models;

namespace EbookWebAPI.Dtos.Ebook
{
    public class ReadMultipleEBookDTO : BaseResponse
    {
        public List<ReadEBookDTO> EBooks { get; set; }
    }
    public class ReadMultipleEBookDTOPage : BaseResponse
    {
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public List<ReadEBookDTO> EBooks { get; set; }
    }
    public class ReadEBookDTO
    {
        public int Id { get; set; }
        public int SKU { get; set; }
        public string BookName { get; set; }
        public string Link { get; set; }
    }
    public class ReadSingleEBookDTO : BaseResponse
    {
        public int Id { get; set; }
        public int SKU { get; set; }
        public string BookName { get; set; }
        public string Link { get; set; }
    }
    public class ReadMultipleSKU : BaseResponse
    {
        public List<ReadSKU> Data { get; set; }
    }
    public class ReadSKU
    {
        public int SKU { get; set; }
    }
    public class BookNameDTO
    {
        public string BookName { get; set; }
    }
    public class GetEBookBySKU
    {
        public int Id { get; set; }
        public int SKU { get; set; }
        public string BookName { get; set; }
        public string Link { get; set; }
        public GetEBookBySKU(LinkEbook ebook)
        {
            Id = ebook.Id;
            SKU = ebook.SKU;
            BookName = ebook.BookName;
            Link = ebook.Link;
        }
    }


}
