namespace WorldDatabase.Models
{
    public struct NewsArticle
    {
        NewsImage[] _multimedia;
        public string Abstract { get; set; }
        public string Web_url { get; set; }
        public NewsImage[] Multimedia
        {
            get
            {
                return _multimedia ?? new NewsImage[1] {new NewsImage()};
            }
            set
            {
                if (value is NewsImage[] newsImageArray)
                {
                    if(newsImageArray.Length > 0)
                    {
                        _multimedia = value;
                    }
                    else
                    {
                        _multimedia = new NewsImage[1] { new NewsImage() };
                    }

                }
                else
                {
                    _multimedia = new NewsImage[1] { new NewsImage()};
                }
            }
        }
    }

    public struct NewsImage
    {
        private string _url;
        public string Url {
            get
            {
                if(!string.IsNullOrEmpty(_url))
                {
                    return _url;
                }
                return "https://developer.nytimes.com/files/poweredby_nytimes_30a.png?v=1583354208339";
            }

            set
            {
                _url = $"https://www.nytimes.com/{value}";
            }
        }
        public string Height { get; set; }
        public string Width { get; set; }
    }
}


