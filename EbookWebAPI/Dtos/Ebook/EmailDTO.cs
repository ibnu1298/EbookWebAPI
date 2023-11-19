using System.ComponentModel.DataAnnotations;

namespace EbookWebAPI.Dtos.Ebook
{
    public class EmailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class MultipleEmailDTO : BaseResponse
    {
        public List<EmailDTO> Data { get; set; }
    }
    public class ResponseCreateEmailDTO : BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class CreateEmailDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
    public class CreateMultipleEmailDTO
    {
        public List<CreateEmailDTO> Data { get; set; }
    }
    public class SendEmailDTO
    {
        public string SendTo { get; set; }
        public string SKU { get; set; }
    }
}
