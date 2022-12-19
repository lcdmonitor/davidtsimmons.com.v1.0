using System;
using DavidSimmons.Repository.Interfaces;
using DavidSimmons.Contracts;
using DavidSimmons.Repository.Entities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Diagnostics;
using Microsoft.Azure;
using DavidSimmons.Core.Extensions;
using Newtonsoft.Json;

namespace DavidSimmons.Repository
{
    public class BlogEntryRepository : IBlogEntryRepository
    {
        private IWebRepository _webRepository;

        public BlogEntryRepository(IWebRepository webRepository)
        {
            this._webRepository = webRepository;
        }

        public void UpdateEntry(BlogEntryUpdated blogEntryUpdatedEvent)
        {
            try
            {
                //TODO: Refactor me, intent is to update existing entry
                var entryToUpdate = _webRepository.GetEntry(blogEntryUpdatedEvent.PartitionKey, blogEntryUpdatedEvent.Key);

                entryToUpdate.Title = blogEntryUpdatedEvent.Title;
                entryToUpdate.Summary = blogEntryUpdatedEvent.Summary;
                entryToUpdate.Text = blogEntryUpdatedEvent.Text;
                
                //TODO: do storage stuff here, and refactor
                // Retrieve the storage account from the connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

                // Create the table client.
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                // Create the table if it doesn't exist.
                CloudTable table = tableClient.GetTableReference("BlogEntries");
                table.CreateIfNotExists();

                var entryToSave = new BlogEntryEntity()
                {
                    BlogEntryData = JsonConvert.SerializeObject(entryToUpdate),
                    PartitionKey = blogEntryUpdatedEvent.PartitionKey,
                    RowKey = blogEntryUpdatedEvent.Key
                };

                TableOperation insertOperation = TableOperation.InsertOrReplace(entryToSave);

                // Execute the insert operation.
                table.Execute(insertOperation);
            }
            catch (Exception ex)
            {
                //TODO: BETTER ERROR HANDLING
                Debug.WriteLine(ex.Message);
            }
        }


        public void PostEntry(BlogEntryPosted blogEntryPostedEvent)
        {
            try
            {
                //TODO do storage stuff here, and refactor
                // Retrieve the storage account from the connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

                // Create the table client.
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                // Create the table if it doesn't exist.
                CloudTable table = tableClient.GetTableReference("BlogEntries");
                table.CreateIfNotExists();

                var entryToSave = new BlogEntryEntity(blogEntryPostedEvent);

                TableOperation insertOperation = TableOperation.Insert(entryToSave);

                // Execute the insert operation.
                table.Execute(insertOperation);
            }
            catch(Exception ex)
            {
                //TODO: BETTER ERROR HANDLING
                Debug.WriteLine(ex.Message);
            }
        }

        public void UpdateBlogEntriesByMonth(BlogEntryPosted blogEntryPostedEvent)
        {
            //TODO: FIX CONNCURRENCY Problem here.... see web log repository
            //TODO do storage stuff here, and refactor
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("BlogEntriesByMonth");
            table.CreateIfNotExists();

            //retrieve the record
            TableOperation retrieveOperation = TableOperation.Retrieve<BlogEntriesByMonthEntity>(blogEntryPostedEvent.PostDate.ToMonthAndYearStorageKey(), blogEntryPostedEvent.PostDate.ToMonthAndYearStorageKey());

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            BlogEntriesByMonthEntity entryToSave;

            if (retrievedResult.Result == null)
            {
                entryToSave = new BlogEntriesByMonthEntity(blogEntryPostedEvent);
            }
            else
            {
                entryToSave = retrievedResult.Result as BlogEntriesByMonthEntity;
                entryToSave.PostCount++;
            }

            TableOperation insertOperation = TableOperation.InsertOrReplace(entryToSave);

            // Execute the insert operation.
            table.Execute(insertOperation);
        }
    }
}
