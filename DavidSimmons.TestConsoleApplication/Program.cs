using DavidSimmons.Contracts;
using DavidSimmons.Core.Cloud;
using Microsoft.Practices.Unity;
using Rebus.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DavidSimmons.TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = null;

            using(UnityContainer container = new UnityContainer())
            {
                var adapter = new UnityContainerAdapter(container);

                #region get embeded resource text
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "DavidSimmons.TestConsoleApplication.SampleBlogText.txt";

                string blogText;

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    blogText = reader.ReadToEnd();
                }
                #endregion

                using (var bus = new BusFactory().CreateBus(container, true))
                {
                    Console.WriteLine("DavidSimmons.com test harness, type x to bail");
                    Console.WriteLine("Awaiting Input");
                    while (string.IsNullOrEmpty(input))
                    {
                        input = Console.ReadLine();
                        for (int i = 1; i <= 12; i++)
                        {
                            bus.Send(new BlogEntryPosted()
                            {
                                Title = "Dave Was Here at " + Guid.NewGuid(),
                                PostDate = new DateTime(DateTime.Now.Year, i, 1),
                                Text = blogText,
                                Summary = "Summary Text Goes Here",
                                PostedByUser = "Dave Simmons",
                                PostImage = new Image()
                                {
                                    ImageFileName = "Dave.jpg",
                                    ImageThumbnailFileName = "Dave-Thumb.jpg"
                                },
                                Attachments = new List<Attachment>(
                                        new Attachment[]
                                        {
                                        new Attachment() { AttachmentName="Source Code", FileName="code.zip" },
                                        new Attachment() { AttachmentName="Documentation", FileName="README.TXT" }
                                        }
                                    )
                            });
                        }
                    }
                }
            }
        }
    }
}
