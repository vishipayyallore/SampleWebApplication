using System;
using Microsoft.WindowsAzure.Storage.Table;
using WebRole1.Entities;

namespace WebRole1.Services
{
    public class EmailService : TableService
    {
        private string tableName = "email";

        public EmailService() : base()
        {
        }

        public void Insert(string to, string message, string thandle, string status)
        {
            CloudTable emailTable = this.GetTableReference(tableName);
            if (emailTable == null) return;

            TableOperation insertOperation = TableOperation.Insert(new Email()
            {
                PartitionKey = thandle,
                RowKey = Guid.NewGuid().ToString(),
                To = to,
                Message = message,
                Status = status
            });

            emailTable.Execute(insertOperation);
        }
    }
}
