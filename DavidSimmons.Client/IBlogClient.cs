using DavidSimmons.Contracts;
using System.Collections.Generic;
using System.IO;

namespace DavidSimmons.Client
{
    public interface IBlogClient
    {
        //retrieve
        List<BlogEntry> GetFrontPageEntries();

        //post
        void PostBlogEntry(BlogEntryPosted entry);
        void UpdateBlogEntry(BlogEntryUpdated entry);
        BlogEntry GetEntry(string month, string key);
        List<Archive> GetArchive();
        List<BlogEntry> GetEntriesByMonth(string monthKey);
        void UploadBlob(Stream inputStream, string blobName, string mimeType);
    }
}
