using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace WebRole1.Services
{
    public class TableService
    {
        #region Fields

        string ConnectionString1;
        string ConnectionString2;

        #endregion

        #region Constructors

        public TableService() 
        {
            this.ConnectionString1 = CloudConfigurationManager.GetSetting("StorageAccount.ConnectionString");
            this.ConnectionString2 = CloudConfigurationManager.GetSetting("StorageAccount.ConnectionString.Fake");
        }

        #endregion

        #region Protected methods

        protected CloudTable GetTableReference(string conString, string tableName)
        {
            CloudTable table = default(CloudTable);

            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(conString);
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                table = tableClient.GetTableReference(tableName);
                table.CreateIfNotExists();
                table.GetPermissions();
            }
            catch (StorageException ex)
            {
                table = default(CloudTable);
            }

            return table;
        }

        protected CloudTable GetTableReference(string tableName)
        {
            CloudTable friendTable = GetTableReference(this.ConnectionString1, tableName);

            if (friendTable == default(CloudTable))
            {
                friendTable = GetTableReference(this.ConnectionString2, tableName);
                if (friendTable == default(CloudTable)) return null;
            }

            return friendTable;
        }

        #endregion

    }
}
