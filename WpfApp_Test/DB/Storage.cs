using System.ComponentModel.DataAnnotations;

namespace WpfApp_Test.DB
{
    public class Storage
    {
        [Key]
        public int StorageId { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}