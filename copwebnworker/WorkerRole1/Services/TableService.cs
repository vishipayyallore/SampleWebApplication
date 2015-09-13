using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSample.API.Media
{
    public class TableService
    {
        string connectionString;
        string tableName = "email";

        public TableService() 
        {
            this.connectionString = CloudConfigurationManager.GetSetting("StorageAccount.ConnectionString");
        }

        public void DeleteEmail(string thandle, double beforeDays)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(tableName);
            
            var projectionQuery = new TableQuery<DynamicTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, thandle))
                .Select(new string[] { "RowKey", "Timestamp" });

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
