using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Features.Clients
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identification { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }

        [NotMapped]
        public int Client_Id { get; set; }
    }
}