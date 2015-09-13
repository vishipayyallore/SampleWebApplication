using Microsoft.WindowsAzure.Storage.Table;

namespace WebRole1.Entities
{
    public class Email : TableEntity
    {
        public string To { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
