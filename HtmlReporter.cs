using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasbaraProcessor
{
    internal class HtmlReporter
    {
        string _html;
        DateTime _date = DateTime.Now;
        int _counter = 0;

        public void Save()
        {
            if (!Directory.Exists(@".\Reports"))
                Directory.CreateDirectory(@".\Reports");

            File.WriteAllText($@".\Reports\Report{_date:yyyyMMddHHmmss}.htm", _html);
        }

        public void Init(DateTime startDate, DateTime endDate)
        {
            _html = $"<html><header><title>Hasbara Report {_date:yyyy/MM/dd hh:mm:ss}</title></header><body>";
            _html += $"<h1>Reporte {_date:dd/MM/yyyy HH:mm:ss}</h1>";
            _html += $"<h1>Desde {startDate:dd/MM/yyyy HH:mm:ss} - Hasta {endDate:dd/MM/yyyy HH:mm:ss}</h1><br/><br/>";
        }

        public void OpenResume()
        {
            _html += "<center>";
            _html += "<table border=1 cellpadding=\"10\" cellspacing=\"0\">";           
        }

        public void CloseResume()
        {
            _html += "</table>";
            _html += "</center>";
        }


        public void AddResume(string site, int numberOfLinks)
        {
            if (_counter == 0) _html += "<tr height=5>";

            _html += $"<td><h4><a href=\"#{site}\">{site} ({numberOfLinks} Notas)</a></h4></td>";
            
            if (_counter == 2)
            {
                _html += "</tr>";
                _counter = -1;
            }            

            _counter++;
        }

        public void AddSite(string site)
        {
            _html += $"<center><h2><a id=\"{site}\">{site}</a></h2>";
            _html += "<table border=1 cellpadding=\"10\" cellspacing=\"0\">";
        }

        public void AddLink(ReportXMLLink link)
        {
            _html += "<tr><h3>";
            _html += $"<td width=\"770px\"><a href=\"{link.Url}\">{link.Text}</a></td><td width=\"130px\">{link.StartTime:dd/MM/yyyy HH:mm} - {link.EndTime:dd/MM/yyyy HH:mm}</td><td width=\"30px\">{(link.EndTime - link.StartTime).TotalDays:n2} dias</td>";
            _html += "</h3></tr>";
        }

        public void CloseSite()
        {
            _html += "</table></center>";
        }

        public void Close()
        {
            _html += "</body></html>";
        }
    }
}
