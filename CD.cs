using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practicum_4_XML
{
    class CD
    {
        private List<Track> tracks { get; set; } = null;
        private string title { get; set; }
        private string author { get; set; }

        public CD(List<Track> tracks, string title, string author)
        {
            this.tracks = tracks;
            this.title = title;
            this.author = author;
        }

        public List<Track> Tracks
        {
            get { return tracks; }
            set { tracks = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public void addTrack(Track t)
        {
            Tracks.Add(t);
        }
    }
}
