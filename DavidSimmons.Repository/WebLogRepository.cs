using DavidSimmons.Repository.Interfaces;
using System;
using DavidSimmons.Contracts;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using DavidSimmons.Repository.Entities;
using DavidSimmons.Core.Extensions;

namespace DavidSimmons.Repository
{
    public class WebLogRepository : RepositoryBase, IWebLogRepository
    {
        public void LogPageVisited(PageVisited pageVisitedEvent)
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
                CloudTable table = tableClient.GetTableReference("PageVisits");
                table.CreateIfNotExists();

                var entryToSave = new PageVisitEntity(pageVisitedEvent, pageVisitedEvent.RawUrl.ToStorageKey(), GetProcessingKey());

                TableOperation insertOperation = TableOperation.Insert(entryToSave);

                // Execute the insert operation.
                table.Execute(insertOperation);
            }
            catch (Exception ex)
            {
                //TODO: BETTER ERROR HANDLING
                Debug.WriteLine(ex.Message);
            }
        }

        public void UpdatePageVisitsByDay(PageVisited pageVisitedEvent)
        {
            //TODO do storage stuff here, and refactor
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("PageVisitsByDay");
            table.CreateIfNotExists();


            // Execute the insert operation.

            //retrieve the record
            TableOperation retrieveOperation = TableOperation.Retrieve<VisitsByDayEntity>(pageVisitedEvent.VisitDate.ToStorageKey(), GetProcessingKey());

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            VisitsByDayEntity entryToSave;
            TableOperation logOperation;

            if (retrievedResult.Result == null)
            {
                entryToSave = new VisitsByDayEntity(pageVisitedEvent, pageVisitedEvent.VisitDate.ToStorageKey(), GetProcessingKey());
                logOperation = TableOperation.Insert(entryToSave);
            }
            else
            {
                entryToSave = retrievedResult.Result as VisitsByDayEntity;
                entryToSave.VisitCount++;
                logOperation = TableOperation.Replace(entryToSave);
            }

            table.Execute(logOperation);
        }
    

        public void UpdatePageVisitsByURL(PageVisited pageVisitedEvent)
        {
            //TODO do storage stuff here, and refactor
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("PageVisitsByURL");
            table.CreateIfNotExists();

            // retrieve the record
            TableOperation retrieveOperation = TableOperation.Retrieve<VisitsByURLEntity>(pageVisitedEvent.RawUrl.ToStorageKey(), GetProcessingKey());

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            VisitsByURLEntity entryToSave;

            TableOperation logOperation;

            if (retrievedResult.Result == null)
            {
                entryToSave = new VisitsByURLEntity(pageVisitedEvent, pageVisitedEvent.RawUrl.ToStorageKey(), GetProcessingKey());
                logOperation = TableOperation.Insert(entryToSave);
            }
            else
            {
                entryToSave = retrievedResult.Result as VisitsByURLEntity;
                entryToSave.VisitCount++;
                logOperation = TableOperation.Replace(entryToSave);
            }

            Trace.WriteLine(string.Format("Logging {0} Visits to : {1} for: {2}", entryToSave.VisitCount, entryToSave.URL, GetProcessingKey()));

            // Execute the insert operation.
            table.Execute(logOperation);
        }
    }
}

