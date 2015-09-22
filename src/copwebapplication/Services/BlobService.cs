using System;
using System.Configuration;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace copwebapplication.Services
{
    public class BlobService
    {
        private string connectionString;
        private string containerName = "images";
        private string defaultImage = "image.jpg";

        public BlobService() 
        {
            this.connectionString = ConfigurationManager.AppSettings["StorageAccount.ConnectionString"].ToString();
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
            //File.Open(defaultImage, FileMode.Open).CopyTo(mStream);

            return mStream;
        }
    }
}
