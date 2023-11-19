namespace EbookWebAPI.Models
{
    public class EmailCustomer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int RowStatus { get; set; }
    }
}
