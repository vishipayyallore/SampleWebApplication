using System;
using System.Collections.Generic;
using copwebapplication.Entities;
using Microsoft.WindowsAzure.Storage.Table;

namespace copwebapplication.Services
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
                PartitionKey = "author",
                RowKey = thandle,
                To = to,
                Message = message,
                Status = status
            });

            emailTable.Execute(insertOperation);
        }

        public IEnumerable<Email> GetEmails(string fromDate, string toDate)
        {
            CloudTable emailTable = GetTableReference(tableName);
            if (emailTable == null) return new List<Email>();

            TableQuery<Email> query = default(TableQuery<Email>);

            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                var fromDateTime = DateTime.SpecifyKind(Convert.ToDateTime(fromDate), DateTimeKind.Utc);
                var toDateTime = DateTime.SpecifyKind(Convert.ToDateTime(toDate), DateTimeKind.Utc);

                string fromDateFilter = TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.GreaterThanOrEqual, fromDateTime);
                string toDateFilter = TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.LessThanOrEqual, toDateTime);
                string finalFilter = TableQuery.CombineFilters(fromDateFilter, TableOperators.And, toDateFilter);

                query = new TableQuery<Email>().Where(finalFilter);
            }
            else
            {
                query = new TableQuery<Email>();
            }


            return emailTable.ExecuteQuery<Email>(query);
        }
    }
}
