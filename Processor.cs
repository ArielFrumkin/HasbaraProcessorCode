using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasbaraProcessor
{
    internal class Processor
    {
        private readonly string _path;
        private readonly DateTime _startTime;
        private readonly DateTime _endTime;

        public IEnumerable<ReportXML> Reports { get; private set; }
        Dictionary<string, List<ReportXMLLink>> _result = new Dictionary<string, List<ReportXMLLink>>();


        public Processor(string path, DateTime startTime, DateTime endTime)
        {
            _path = path;
            _startTime = startTime;
            _endTime = endTime;
            Reports = new List<ReportXML>();
        }

        public void GetReports()
        {
            foreach (var file in Directory.GetFiles(_path))
            {
                try
                {
                    var xmlParser = new XmlParser(file);
                    var report = xmlParser.Deserialize();
                    if (report.DateTime >= _startTime && report.DateTime <= _endTime)
                        ((List<ReportXML>)Reports).Add(report);
                }
                catch
                { }
            }
        }

        public void GenerateReport()
        {           
            foreach (var report in Reports)
            {
                foreach (var link in report.Links)
                {
                    if (!_result.ContainsKey(link.Site.ToString()))
                    {
                        _result.Add(link.Site.ToString(), new List<ReportXMLLink>());
                    }

                    if (!_result[link.Site.ToString()].Any(l => ReplaceSlashes(l.Url) == ReplaceSlashes(link.Url) || l.Text == link.Text))
                    {
                        link.StartTime = report.DateTime;
                        link.EndTime = report.DateTime;
                        _result[link.Site.ToString()].Add(link);
                    }
                    else
                    {
                        var li = _result[link.Site.ToString()].First(l => ReplaceSlashes(l.Url) == ReplaceSlashes(link.Url) || l.Text == link.Text);
                        li.StartTime = report.DateTime < li.StartTime ? report.DateTime : li.StartTime;
                        li.EndTime = report.DateTime > li.EndTime ? report.DateTime : li.EndTime;
                    }
                }
            }

            Save();
        }

        private void Save()
        {
            HtmlReporter html = new HtmlReporter();
            html.Init(_startTime, _endTime);

            html.OpenResume();
            foreach (var item in _result)
            {
                html.AddResume(item.Key, item.Value.Count);            
            }
            html.CloseResume();

            foreach (var item in _result)
            {
                html.AddSite(item.Key);

                foreach (var link in item.Value)
                {
                    html.AddLink(link);
                }

                html.CloseSite();
            }
            html.Close();
            html.Save();
        }

        private string ReplaceSlashes(string url)
        {
            return url.Replace("/", "\\");
        }
    }
}

