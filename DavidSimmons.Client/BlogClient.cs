using System.Collections.Generic;
using DavidSimmons.Contracts;
using DavidSimmons.Core.Cloud.Interfaces;
using Microsoft.Practices.Unity;
using DavidSimmons.Repository.Interfaces;
using System;
using System.IO;

namespace DavidSimmons.Client
{
    public class BlogClient : IBlogClient
    {
        private IBusFactory _busFactory;
        private IWebRepository _webRespository;

        //todo: not sure if I loke having a repo reference here could be abused.
        public BlogClient (IBusFactory busFactory, IWebRepository webRepository)
        {
            this._busFactory = busFactory;
            this._webRespository = webRepository;
        }

        public BlogEntry GetEntry(string month, string key)
        {
            return _webRespository.GetEntry(month, key);
        }

        public List<BlogEntry> GetFrontPageEntries()
        {
            return _webRespository.GetFrontPageEntries(1);
        }

        public List<Archive> GetArchive()
        {
            return _webRespository.GetArchive();
        }

        public List<BlogEntry> GetEntriesByMonth(string monthKey)
        {
            return _webRespository.GetEntriesByMonth(monthKey);
        }

        public void UploadBlob(Stream inputStream, string blobName, string mimeType)
        {
            _webRespository.UploadBlob(inputStream, blobName, mimeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public void PostBlogEntry(BlogEntryPosted entry)
        {
            //TODO: the way unity is being passed around is pretty sketchy, MAYBE CREATE A CONTAINER PROVIDER AS A constuctory,DEDPENDENCY TO BusFactory
            //TODO: SPLIT CREATE BUS AND CREATE BUS FOR SEND ONLY INTO 2 DIFFERENT METHODS, or make client/ server bus factories.
            using (var bus = this._busFactory.CreateBus(new UnityContainer(), true))
            {
                bus.Send(entry);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public void UpdateBlogEntry(BlogEntryUpdated entry)
        {
            //TODO: the way unity is being passed around is pretty sketchy, MAYBE CREATE A CONTAINER PROVIDER AS A constuctory,DEDPENDENCY TO BusFactory
            //TODO: SPLIT CREATE BUS AND CREATE BUS FOR SEND ONLY INTO 2 DIFFERENT METHODS, or make client/ server bus factories.
            using (var bus = this._busFactory.CreateBus(new UnityContainer(), true))
            {
                bus.Send(entry);
            }
        }
    }
}
