using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team3Assig.Controllers
{
    public class ArticleRecord
    {
        public ArticleRecord()
        {
        }

        public ArticleRecord(int id, List<string> authors, string title, List<string> topics, string downloadUrl)
        {
            Id = id;
            Authors = authors;
            Title = title;
            Topics = topics;
            DownloadUrl = downloadUrl;
        }

        public int Id { get; set; }
        public List<string> Authors { get; set; }
        public string Title { get; set; }
        public List<string> Topics { get; set; }
        public string DownloadUrl { get; set; }
    }

   
}
