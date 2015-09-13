using System;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WorkerRole1.Services
{
    public class BlobService
    {
        private readonly string _connectionString;
        private readonly string _containerName = "images";
        private readonly string _defaultImage = "image.jpg";

        public BlobService() 
        {
            _connectionString = CloudConfigurationManager.GetSetting("StorageAccount.ConnectionString");
        }

        public MemoryStream GetImage(string handle) 
        {
            CloudStorageAccount storageAcct = CloudStorageAccount.Parse(_connectionString);
            CloudBlobClient blobClient = storageAcct.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(_containerName);

            string imageName = String.Concat(handle, ".jpg");
            CloudBlockBlob blob = container.GetBlockBlobReference(imageName);
            if (!blob.Exists()) blob = container.GetBlockBlobReference(_defaultImage);

            MemoryStream mStream = new MemoryStream();
            blob.DownloadToStream(mStream);
            return mStream;
        }
    }
}
