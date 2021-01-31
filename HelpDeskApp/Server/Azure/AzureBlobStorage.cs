using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HelpDeskApp.Shared;
using System.Collections.Generic;

namespace HelpDeskApp.Server.Azure
{
    public class AzureBlobStorage : IFileStorage
    {
        private readonly AzureStorageConfig _storageConfig;

        public AzureBlobStorage(IOptionsMonitor<AzureStorageConfig> storageConfig)
        {
            _storageConfig = storageConfig.CurrentValue;
        }

        public async Task Initialize()
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConfig.ConnectionString);
            // Create the container
            // If the container with the same name already exists, the operation fails.
            try{
                BlobContainerClient blobContainer = await blobServiceClient.CreateBlobContainerAsync(_storageConfig.BlobPictureContainer);
            }
            catch(RequestFailedException e) when( e.ErrorCode is "ContainerAlreadyExists")
            {
                /* nop */
            }
            return; 
        }

        public async Task Save(Stream fileStream, string name)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConfig.ConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_storageConfig.BlobPictureContainer);
            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(name);
            // Open the file and upload its data
            await blobClient.UploadAsync(fileStream, true);
        }

        public async Task Delete(string name)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConfig.ConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_storageConfig.BlobPictureContainer);
            // Get a reference to a blob
            await containerClient.DeleteBlobAsync(name);
        }

        public async Task<IEnumerable<string>> GetNames()
        {
            List<string> names = new List<string>();

            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConfig.ConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_storageConfig.BlobPictureContainer);

            // List all blobs in the container
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                names.Add(blobItem.Name);
            }

            return names;
        }

        public async Task<Stream> Load(string name)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConfig.ConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_storageConfig.BlobPictureContainer);
            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(name);
            // Download the blob's contents and save it to a file
            BlobDownloadInfo download = await blobClient.DownloadAsync();
            
    
            return download.Content;
        }
    }
}