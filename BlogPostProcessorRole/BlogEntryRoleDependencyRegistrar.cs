using DavidSimmons.Core.Dependency;
using Microsoft.Practices.Unity;
using DavidSimmons.Repository.Interfaces;
using DavidSimmons.Repository;
using DavidSimmons.Core.Cloud.Interfaces;
using DavidSimmons.Core.Cloud;
using Rebus;
using DavidSimmons.Contracts;
using DavidSimmons.Processing.Handlers;

namespace BlogPostProcessorRole
{
    public class BlogEntryRoleDependencyRegistrar : IDependencyRegistrar
    {
        public void Configure(UnityContainer container)
        {
            container.RegisterType<IWebLogRepository, WebLogRepository>();
            container.RegisterType<IBlogEntryRepository, BlogEntryRepository>();
            container.RegisterType<IRawLoggingRepository, RawLoggingRepository>();
            container.RegisterType<IWebRepository, WebRepository>();
            container.RegisterType<IBusFactory, BusFactory>();

            //todo: needs to be dynamic wireup for all Ihandlemessages in assembly just not sure how to hook that up, once dynamic, remove reference to Processing DLL
            container.RegisterType<IHandleMessages<BlogEntryPosted>, HandleBlogEntry>("HandleBlogEntry");
            container.RegisterType<IHandleMessages<BlogEntryPosted>, UpdateBlogEntriesByMonth>("UpdateBlogEntriesByMonth");

            container.RegisterType<IHandleMessages<BlogEntryUpdated>, HandleBlogEntryUpdated>("HandleBlogEntryUpdated");

            container.RegisterType<IHandleMessages<PageVisited>, LogPageVisit>("LogPageVisit");
            container.RegisterType<IHandleMessages<PageVisited>, UpdatePageVisitsByURL>("UpdatePageVisitsByURL");
            container.RegisterType<IHandleMessages<PageVisited>, UpdatePageVisitsByDay>("UpdatePageVisitsByDay");
        }
    }
}
