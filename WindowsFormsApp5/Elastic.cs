using Nest;
using System;
using System.Collections.Generic;
using System.Data;
namespace WindowsFormsApp5
{
    public static class Elastic
    {
        public static ElasticClient client;
        public static string CloudID { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string IndexName { get; set; }

     public static void Autorization(string text4,string text3,string text1)//подключение к эластику
      {
            CloudID=@text4;  UserName=text3;  Password=text1;
            client = new ElasticClient(CloudID, new Elasticsearch.Net.BasicAuthenticationCredentials(UserName, Password));

      }

     public static void MakeIndexEl() //создаем индекс в эластике
      {
            IndexName = "new_elast_id";
            var creatIndexResp = client.Indices.Create(IndexName, x => x.Map<JobItem>(m => m.AutoMap()));
      }

     public static void CreateDocumentResponse(List<JobItem> records)//заливаем в эластик бд(лист классов с полями бд)
      {
            var createDocResponse = client.Bulk(a => a.CreateMany(records).Index(IndexName));
      }

     public static List<DowloadElast> SearchELast(string input)//поиск по эластику по нашему индекчу,и выдачу кидаем в лист 
        {
            var searchResponse = client.Search<DowloadElast>(s => s.Index("new_elast_id")
           .From(0)
             .Size(20)
               .Query(q => q
                 .Match(m => m
                  .Field(f => f.text)
                    .Query(input)))) ;

           var documets = searchResponse.Documents;
             List<DowloadElast> results = new List<DowloadElast>();
    
            foreach (var i in documets)
            {
                DowloadElast dowloadElast = i;
                results.Add(dowloadElast);
            }
            results.Sort((x,y)=>x.created_date.CompareTo(y.created_date));

            resultSearchElast(results);
            return results;
        }

        public static void DeleteELast_ID(string input)
        {
            client.Delete<DowloadElast>(input, x=>x.Index("new_elast_id"));



        }
        public static void DeleteELast_INDEX()
        {
            client.Indices.Delete("new_elast_id");



        }


        public static DataTable resultSearchElast(List<DowloadElast> input)//лист с поиска эластика ,кидаем в бд (для вывода поиска)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("text");
            dt.Columns.Add("created_date");
            dt.Columns.Add("rubrics");
            dt.Columns.Add("ID");
            foreach (var item in input)
            {
                var row = dt.NewRow();
                row["ID"] = Convert.ToInt32(item.ID);
                row["created_date"] = Convert.ToDateTime(item.created_date);
                row["text"] = item.text;
                row["rubrics"] = item.rubrics;
                dt.Rows.Add(row);
            }
            return dt;
        }
    }

    public class DowloadElast //клас с полями для эластика (вывода)
        {
        public string ID { get; set; }
        public string text { get; set; }
        public string created_date { get; set; }
        public string rubrics { get; set; }
    }
}
