using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace WT.SimulatorService
{
    public static class PositionReader
    {

        public static Gpx SerializeFile(string xml)
        {
			XmlSerializer serializer = new XmlSerializer(typeof(Gpx));

            using (StringReader reader = new StringReader(xml))
            {
                return (Gpx)serializer.Deserialize(reader);
            }
        }

	}


	[XmlRoot(ElementName = "trkpt")]
	public class Trkpt
	{

		[XmlElement(ElementName = "ele")]
		public double Ele { get; set; }

		[XmlElement(ElementName = "time")]
		public DateTime Time { get; set; }

		[XmlAttribute(AttributeName = "lat")]
		public double Lat { get; set; }

		[XmlAttribute(AttributeName = "lon")]
		public double Lon { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "trkseg")]
	public class Trkseg
	{

		[XmlElement(ElementName = "trkpt")]
		public List<Trkpt> Trkpt { get; set; }
	}

	[XmlRoot(ElementName = "trk")]
	public class Trk
	{

		[XmlElement(ElementName = "trkseg")]
		public Trkseg Trkseg { get; set; }
	}

	[XmlRoot(ElementName = "gpx")]
	public class Gpx
	{

		[XmlElement(ElementName = "name")]
		public string Name { get; set; }

		[XmlElement(ElementName = "trk")]
		public Trk Trk { get; set; }

		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }

		[XmlAttribute(AttributeName = "creator")]
		public string Creator { get; set; }

		[XmlAttribute(AttributeName = "version")]
		public DateTime Version { get; set; }

		[XmlAttribute(AttributeName = "xsi")]
		public string Xsi { get; set; }

		[XmlAttribute(AttributeName = "schemaLocation")]
		public string SchemaLocation { get; set; }

		[XmlText]
		public string Text { get; set; }
	}
}
