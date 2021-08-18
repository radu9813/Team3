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

        public ArticleRecord(int id, string authors, string title, string topics, string downloadUrl)
        {
            this.Id = id;
            this.Authors = authors;
            this.Title = title;
            this.Topics = topics;
            this.DownloadUrl = downloadUrl;
        }

        public int Id { get; set; }
        public string Authors { get; set; }
        public string Title { get; set; }
        public string Topics { get; set; }
        public string DownloadUrl { get; set; }
    }

   
}
