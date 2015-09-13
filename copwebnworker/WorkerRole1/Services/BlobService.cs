using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSample.API.Media
{
    public class BlobService
    {
        private string connectionString;
        private string containerName = "images";
        private string defaultImage = "image.jpg";

        public BlobService() 
        {
            this.connectionString = CloudConfigurationManager.GetSetting("StorageAccount.ConnectionString");
        }

        public MemoryStream GetImage(string handle) 
        {
            CloudStorageAccount storageAcct = CloudStorageAccount.Parse(this.connectionString);
            CloudBlobClient blobClient = storageAcct.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            string imageName = String.Concat(handle, ".jpg");
            CloudBlockBlob blob = container.GetBlockBlobReference(imageName);
            if (!blob.Exists()) blob = container.GetBlockBlobReference(defaultImage);

            MemoryStream mStream = new MemoryStream();
            blob.DownloadToStream(mStream);
            return mStream;
        }
    }
}
