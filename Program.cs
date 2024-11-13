using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string accountName = "ovidiu.loghin@gmail.com";

BlobServiceClient blobServiceClient = GetBlobServiceClient(accountName);

Console.WriteLine("Listing containers in storage account:");
await foreach (var container in blobServiceClient.GetBlobContainersAsync())
{
    Console.WriteLine($"- {container.Name}");
}

Console.WriteLine("Connected to Blob Storage successfully!");


BlobServiceClient GetBlobServiceClient(string accountName)
{
    BlobServiceClient client = new(
        new Uri($"https://{accountName}.blob.core.windows.net"),
        new DefaultAzureCredential());

    return client;
}