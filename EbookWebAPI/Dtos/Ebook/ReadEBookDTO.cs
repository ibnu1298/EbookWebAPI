namespace EbookWebAPI.Dtos.Ebook
{
    public class ReadMultipleEBookDTO : BaseResponse
    {
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
    public class ReadSKU
    {
        public int SKU { get; set; }
    }

}
