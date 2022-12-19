using DavidSimmons.Contracts;
using System.Collections.Generic;
using System.IO;

namespace DavidSimmons.Repository.Interfaces
{
    public interface IWebRepository
    {
        List<BlogEntry> GetFrontPageEntries(int pageNumber);
        List<BlogEntry> GetEntriesByMonth(string monthKey);
        BlogEntry GetEntry(string month, string key);
        List<Archive> GetArchive();
        void UploadBlob(Stream inputStream, string blobName, string mimeType);
    }
}
