using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practicum_4_XML
{
    class Track
    {
        private string title { get; set; }
        private TimeSpan duration { get; set; }
        private string author { get; set; }

        public string Title{
            get { return title; }
            set { title = value; }
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public Track(string title, string author, TimeSpan duration)
        {
            this.title = title;
            this.author = author;
            this.duration = duration;
        }
    }
}
