using Microsoft.WindowsAzure.Storage.Table;

namespace WebRole1.Entities
{
    public class Author : TableEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
