using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace copwebapplication.Services
{
    public class TableService
    {
        #region Fields

        string ConnectionString;

        #endregion

        #region Constructors

        public TableService() 
        {
            this.ConnectionString = ConfigurationManager.AppSettings["StorageAccount.ConnectionString"].ToString();
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
            catch 
            {
                table = default(CloudTable);
            }

            return table;
        }

        protected CloudTable GetTableReference(string tableName)
        {
            CloudTable friendTable = GetTableReference(this.ConnectionString, tableName);
            return friendTable == default(CloudTable) ? null : friendTable;
        }

        #endregion

    }
}
