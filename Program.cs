using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasbaraProcessor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var startDate = GetStartDate(args);
            var endDate = GetEndDate(args);
            var processor = new Processor(@"C:\Users\ariel\source\repos\PersonalProjects\PersonalProjects\HasbaraSearcher4-8\HasbaraSearcher4-8\bin\Debug\XML\hasbara", startDate, endDate);
            processor.GetReports();            
            processor.GenerateReport();
        }

        private static DateTime GetEndDate(string[] args)
        {
            if (args.Length > 1)
            {
                if (DateTime.TryParse(args[1], out DateTime dateTime))
                    return dateTime;
            }

            return DateTime.Now;
        }

        private static DateTime GetStartDate(string[] args)
        {
            if (args.Length > 0)
            {
                if (DateTime.TryParse(args[0], out DateTime dateTime))
                    return dateTime;
            }

            return DateTime.MinValue;
        }
    }
}
