using System;

namespace DavidSimmons.Contracts
{
    public class PageVisited
    {
        public string UrlReferrer
        {
            get; set;
        }
        public string RawUrl
        {
            get;
            set;
        }
        public string Controller
        {
            get;
            set;
        }
        public string Action
        {
            get;
            set;
        }
        public string Parameters
        {
            get;
            set;
        }
        public string VistorIP
        {
            get;
            set;
        }
        public string UserAgent
        {
            get;
            set;
        }
        public string Browser
        {
            get;
            set;
        }

        public DateTime VisitDate
        {
            get;
            set;
        }
    }
}
