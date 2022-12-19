using DavidSimmons.Contracts;
using DavidSimmons.Repository.Entities;
using DavidSimmons.Repository.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using DavidSimmons.Core.Extensions;
using System.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;

namespace DavidSimmons.Repository
{
    public class WebRepository : IWebRepository
    {
        public List<BlogEntry> GetFrontPageEntries(int pageNumber)
        {
            //TODO do storage stuff here, and refactor
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("BlogEntries");
            table.CreateIfNotExists();

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<BlogEntryEntity> query = new TableQuery<BlogEntryEntity>();//.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, DateTime.Now.ToStorageKey()));

            var entries = new List<BlogEntry>();

            // Print the fields for each customer.
            foreach (BlogEntryEntity entity in table.ExecuteQuery(query))
            {
                var entry = JsonConvert.DeserializeObject<BlogEntry>(entity.BlogEntryData);
                entry.Key = entity.RowKey;
                entry.PartitionKey = entity.PartitionKey;
                entries.Add(entry);
            }

            return entries.OrderByDescending(e=>e.PostDate).ToList();
        }

        public List<BlogEntry> GetEntriesByMonth(string monthKey)
        {
            //TODO do storage stuff here, and refactor
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("BlogEntries");
            table.CreateIfNotExists();

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<BlogEntryEntity> query = new TableQuery<BlogEntryEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, monthKey));

            var entries = new List<BlogEntry>();

            // Print the fields for each customer.
            foreach (BlogEntryEntity entity in table.ExecuteQuery(query))
            {
                var entry = JsonConvert.DeserializeObject<BlogEntry>(entity.BlogEntryData);
                entry.Key = entity.RowKey;
                entry.PartitionKey = entity.PartitionKey;
                entries.Add(entry);
            }

            return entries.OrderByDescending(e=>e.PostDate).ToList();
        }

        public BlogEntry GetEntry(string month, string key)
        {
            //TODO do storage stuff here, and refactor
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(GetConnectionString());

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("BlogEntries");
            table.CreateIfNotExists();


            //TODO: FIND BETTER PARTITIONING SCHEME, MAYBE BY USERS
            //retrieve the record
            TableOperation retrieveOperation = TableOperation.Retrieve<BlogEntryEntity>(month, key);
           
            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            BlogEntryEntity entry = (BlogEntryEntity)retrievedResult.Result;

            if (entry != null)
            {
                return JsonConvert.DeserializeObject<BlogEntry>(entry.BlogEntryData);
            }
            else
            {
                throw new Exception("Sorry, Blog Entry Not Found");
            }
        }

        //TODO: REFACTOR TO BASE CLASS
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["StorageConnectionString"]) ? ConfigurationManager.AppSettings["StorageConnectionString"] : CloudConfigurationManager.GetSetting("StorageConnectionString");
        }

        public List<Archive> GetArchive()
        {
            //TODO do storage stuff here, and refactor
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("BlogEntriesByMonth");
            table.CreateIfNotExists();

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<BlogEntriesByMonthEntity> query = new TableQuery<BlogEntriesByMonthEntity>();//.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, DateTime.Now.ToStorageKey()));

            var entriesByMonth = new List<Archive>();

            // Print the fields for each customer.
            foreach (BlogEntriesByMonthEntity record in table.ExecuteQuery(query).OrderByDescending(x=>x.Year).ThenByDescending(x=>x.Month))
            {
                var entryByMonth = new Archive() { Label=record.Label, Count=record.PostCount, Key = record.RowKey };

                entriesByMonth.Add(entryByMonth);
            }

            return entriesByMonth;
        }

        public void UploadBlob(Stream inputStream, string blobName, string mimeType)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["ContentStorageConnectionString"]);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            blockBlob.Properties.ContentType = mimeType;
            
            blockBlob.UploadFromStream(inputStream);
        }
    }
}
