namespace EbookWebAPI.Dtos.Ebook
{
    public class BaseResponse
    {
        public string Message { get; set; }
        public bool IsSucceeded { get; set; }
    }
    public class AddMultipleEBookDTO
    {      
        public List<AddEBookDTO> Data { get; set; }
    }
    public class AddEBookDTO
    {
        public int SKU { get; set; }
        public string BookName { get; set; }
        public string Link { get; set; }
    } 
    public class AddMultipleSKU
    {      
        public List<AddSKUDTO> Data { get; set; }
    }
    public class AddSKUDTO
    {
        public int SKU { get; set; }
    }
}
