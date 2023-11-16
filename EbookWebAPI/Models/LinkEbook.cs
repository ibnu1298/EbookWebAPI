using System.ComponentModel.DataAnnotations;

namespace EbookWebAPI.Models
{
    public class LinkEbook
    {
        [Key]
        public int Id { get; set; }
        public int SKU { get; set; }
        public string BookName { get; set; }
        public string Link { get; set; }
        public int RowStatus { get; set; } = 0;
    }
}
