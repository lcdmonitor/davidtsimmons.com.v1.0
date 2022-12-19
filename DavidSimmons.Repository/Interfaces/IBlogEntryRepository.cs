using DavidSimmons.Contracts;

namespace DavidSimmons.Repository.Interfaces
{
    public interface IBlogEntryRepository
    {
        void PostEntry(BlogEntryPosted entry);

        void UpdateEntry(BlogEntryUpdated blogEntryUpdatedEvent);

        void UpdateBlogEntriesByMonth(BlogEntryPosted entry);
    }
}
