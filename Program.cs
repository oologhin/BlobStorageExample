using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Threading.Tasks;

namespace BlobStorageExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Define your connection string (make sure to replace this with your actual connection string)
            string connectionString = "";

            // Create a BlobServiceClient to interact with your storage account
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            Console.WriteLine("Listing containers in storage account:");

            // List all containers in the storage account
            await foreach (BlobContainerItem containerItem in blobServiceClient.GetBlobContainersAsync())
            {
                Console.WriteLine($"- {containerItem.Name}");

                // Get the BlobContainerClient for each container
                BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerItem.Name);

                Console.WriteLine($"Listing blobs and metadata in container: {containerItem.Name}");

                // List all blobs in the container
                await foreach (BlobItem blobItem in blobContainerClient.GetBlobsAsync())
                {
                    Console.WriteLine($"  Blob Name: {blobItem.Name}");

                    // Get a reference to the blob to access its properties
                    BlobClient blobClient = blobContainerClient.GetBlobClient(blobItem.Name);

                    // Fetch blob properties to access metadata
                    BlobProperties properties = await blobClient.GetPropertiesAsync();
                    PrintBlobMetadata(properties);
                }
            }

            Console.WriteLine("Connected to Blob Storage successfully!");
        }

        // Helper function to print blob metadata
        static void PrintBlobMetadata(BlobProperties properties)
        {
            // Print metadata
            if (properties.Metadata.Count > 0)
            {
                Console.WriteLine("    Metadata:");
                foreach (var metadata in properties.Metadata)
                {
                    Console.WriteLine($"      {metadata.Key}: {metadata.Value}");
                }
            }
            else
            {
                Console.WriteLine("    No metadata found for this blob.");
            }
        }
    }
}
