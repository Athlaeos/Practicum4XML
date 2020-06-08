using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace practicum_4_XML
{
    class Program
    {
        static void Main(string[] args)
        {
            CD cd1 = new CD(new List<Track>(), "cram city", "the milk men");
            Track t1 = new Track("Delia's Gone", "Johnny Cash", new TimeSpan(0, 1, 59));
            Track t2 = new Track("My father left me", "the milk men ft. Danny Devito", new TimeSpan(0, 0, 12));
            Track t3 = new Track("Where did the milk men go", "the milk men", new TimeSpan(0, 9, 54));
            cd1.addTrack(t1);
            cd1.addTrack(t2);
            cd1.addTrack(t3);

            var CDXML = new XDocument(new XElement("cd",
                new XAttribute("title", cd1.Title),
                new XAttribute("author", cd1.Author),
                from t in cd1.Tracks select 
                new XElement("track", 
                new XElement("title", t.Title), 
                new XElement("author", t.Author), 
                new XElement("duration", t.Duration.ToString()))));

            Console.WriteLine(CDXML.ToString());

            Console.WriteLine("==============================================");

            String xmlString;
            using (WebClient wc = new WebClient())
            {
                xmlString = wc.DownloadString(@"http://ws.audioscrobbler.com/2.0/?method=album.getInfo&album=American%20Recordings&artist=Johnny%20Cash&api_key=b5cbf8dcef4c6acfc5698f8709841949");
            }
            XDocument myXMLDoc = XDocument.Parse(xmlString);
            Console.WriteLine(myXMLDoc.ToString());

            List<Track> APItracks = (from cdtrack in myXMLDoc.Descendants("track")
                                     select new Track(
                                         cdtrack.Element("name").Value,
                                         myXMLDoc.Element("lfm").Element("album").Element("artist").Value,
                                         TimeSpan.FromSeconds(Double.Parse(cdtrack.Element("duration").Value)))).ToList();

            cd1.Tracks.AddRange(APItracks.Where(p => !cd1.Tracks.Any(p2 => (p2.Title == p.Title) && (p2.Author == p.Author))).ToList());

            foreach (Track track in cd1.Tracks)
            {
                Console.WriteLine("\"{0}\" by {1} lasts {2}\n", track.Title, track.Author, track.Duration);
            }
            Console.WriteLine("==============================================");
            CDXML = new XDocument(new XElement("cd",
                new XAttribute("title", cd1.Title),
                new XAttribute("author", cd1.Author),
                from t in cd1.Tracks
                select
                new XElement("track",
                new XElement("title", t.Title),
                new XElement("author", t.Author),
                new XElement("duration", t.Duration.ToString()))));

            Console.WriteLine(CDXML.ToString());
            Console.Read();
        }
    }
}
