using System;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace WorkerRole1.Services
{
    public class TableService
    {
        readonly string _connectionString;
        readonly string _tableName = "email";

        public TableService() 
        {
            _connectionString = CloudConfigurationManager.GetSetting("StorageAccount.ConnectionString");
        }

        public void DeleteEmail(string thandle, double beforeDays)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(_tableName);
            
            var projectionQuery = new TableQuery<DynamicTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, thandle))
                .Select(new[] { "RowKey", "Timestamp" });

            var batchOperation = new TableBatchOperation();

            foreach (var e in table.ExecuteQuery(projectionQuery))
            {
                if ((DateTimeOffset.UtcNow - e.Timestamp).TotalDays > beforeDays) 
                {
                    batchOperation.Delete(e);
                }
            }

            table.ExecuteBatch(batchOperation);
        }

    }
}
