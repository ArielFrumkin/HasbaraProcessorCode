using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HasbaraProcessor
{
    public class XmlParser
    {
        string _path { get; }

        public XmlParser(string path)
        {
            _path = path;
        }               

        public ReportXML Deserialize()
        {
            var serializer = new XmlSerializer(typeof(ReportXML));           

            using (var reader = new FileStream(_path, FileMode.Open))
            {               
                return (ReportXML)serializer.Deserialize(reader);
            }
        }
    }


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class ReportXML
    {

        /// <remarks/>
        public System.DateTime DateTime { get; set; }
       

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Link", IsNullable = false)]
        public ReportXMLLink[] Links { get; set; }        
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ReportXMLLink
    {     
        /// <remarks/>
        public string Text { get; set; }
 
        /// <remarks/>
        public string Url { get; set; }
 
        /// <remarks/>
        public ReportXMLLinkSite Site { get; set; }

        [NonSerialized]
        public DateTime StartTime;

        [NonSerialized]
        public DateTime EndTime;
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ReportXMLLinkSite
    {
        /// <remarks/>
        public string Url { get; set; }
     
        /// <remarks/>
        public string Uri { get; set; }

        /// <remarks/>
        public string Name { get; set; }
        
        /// <remarks/>
        public string Pais { get; set; }

        public override string ToString()
        {
            return $@"{Name} de {Pais} - {Url}";
        }

    }


}
