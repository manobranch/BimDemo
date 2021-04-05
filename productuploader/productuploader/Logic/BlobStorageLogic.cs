using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace productuploader.Logic
{
    public class BlobStorageLogic
    {
        private const string containerName = "products";

        public static string UploadBlob(HttpPostedFileBase file)
        {
            // Retrieve storage account from connection string.
            var connString = ConfigurationManager.AppSettings["StorageConnectionString"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Retrieve reference to a blob named "myblob".            
            var newFileName = Path.GetFileName(file.FileName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(newFileName);

            blockBlob.UploadFromStream(file.InputStream);

            return blockBlob.Uri.ToString();
        }
    }
}