using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Features.Books
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Author { get; set; }
        public string Editorial { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
    }
}
