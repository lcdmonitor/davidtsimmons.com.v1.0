using DavidSimmons.Repository.Interfaces;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Text;

namespace DavidSimmons.Repository
{
    public class RawLoggingRepository : IRawLoggingRepository
    {
        public void CreateLogEntryForException(Exception ex, string instanceID)
        {
            var container = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString"))
                .CreateCloudBlobClient().GetContainerReference("errors");
            container.CreateIfNotExists();
            container.GetBlockBlobReference(string.Format("error-{0}-{1}",
                instanceID, DateTime.UtcNow.Ticks)).UploadText(FormatExceptionForBlob(ex));
        }

        private string FormatExceptionForBlob(Exception ex)
        {
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.AppendLine("Exception Message:");
            messageBuilder.AppendLine(ex.Message);
            messageBuilder.AppendLine("Stack Trace:");
            messageBuilder.AppendLine(ex.StackTrace);

            if (ex.InnerException != null)
            {
                messageBuilder.AppendLine("Inner Exception Message:");
                messageBuilder.AppendLine(ex.InnerException.Message);
                messageBuilder.AppendLine("Stack Trace:");
                messageBuilder.AppendLine(ex.InnerException.StackTrace);
            }

            return messageBuilder.ToString();
        }
    }
}
