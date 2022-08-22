using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldDatabase.Models
{
    public struct NewsArticle
    {
        public string Abstract { get; set; }
        public string Web_url { get; set; }
        public NewsImage[] Multimedia { get; set; }
    }

    public struct NewsImage
    {
        private string _url;
        public string Url {
            get
            {
                return _url;
            }

            set
            {
                _url = "https://www.nytimes.com/" + value;
            }
        }
        public string Height { get; set; }
        public string Width { get; set; }
    }
}


