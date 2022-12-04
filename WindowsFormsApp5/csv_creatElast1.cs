using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace WindowsFormsApp5
{
    public class JobItem
    {
        public string text { get; set; }
        public string created_date { get; set; }
        public string rubrics { get; set; }
       public string ID { get; set; }
        public static List<JobItem> LoadFile(String fileName)
        {
            List<JobItem> items = new List<JobItem>();
            int i = 0;
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                BadDataFound = null,
            };
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var jobItem = new JobItem
                    {
                        text = csv.GetField("text"),
                        created_date = csv.GetField("created_date"),
                        rubrics = csv.GetField("rubrics"),
                        ID = i++.ToString(),
                       
                    };
                    items.Add(jobItem);
                   
                }
            } 
            Elastic.MakeIndexEl();
            Elastic.CreateDocumentResponse(items);return items;
        }
    }
}
