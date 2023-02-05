using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using FireSharp;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using FireSharp.EventStreaming;

namespace Capstone_Program
{
    public class DataFetcher
    {
        IFirebaseConfig ifc;
        IFirebaseClient client;
        public string OriginalDate { get; set; }
        public DataFetcher()
        {
            ifc = new FirebaseConfig()
            {
                BasePath = "https://capstone327masterbase-default-rtdb.europe-west1.firebasedatabase.app/",
                AuthSecret = "da5ZCzqZlfvZHKHDggs5NXPvQDh0LqMqOoSXEbcA"
            };
            client = new FirebaseClient(ifc);
        }


        public (List<double>, List<double>) GetTimeTemperatureSeries()
        {
            FirebaseResponse res = client.Get(@"UsersData/4QJojUdQ61OtiaYiUh7ENMUmHgf1/readings");
            Dictionary<string, Dictionary<string, string>> entryDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(res.Body);
            Dictionary<string, string> rawData = new Dictionary<string, string>();
            foreach(KeyValuePair<string, Dictionary<string, string>> pair in entryDict)
            {
                string dataVal = (pair.Value)["data"];
                if (dataVal.Length <= 20)
                {
                    rawData.Add(pair.Key, dataVal);
                }
            }
            List<double> temperature = new List<double>();
            List<double> time = new List<double>();
            foreach(string s in rawData.Values)
            {
                temperature.Add(double.Parse(s.Split(';')[1]));
                
            }

            foreach(string s in rawData.Keys)
            {
                time.Add(DateTime.Parse(s).Subtract(DateTime.Parse(rawData.Keys.First())).TotalSeconds);
            }

            OriginalDate = rawData.Keys.First();
            return (time, temperature);
        }

        public (List<double>, List<double>) GetTimeTDSSeries()
        {
            FirebaseResponse res = client.Get(@"UsersData/4QJojUdQ61OtiaYiUh7ENMUmHgf1/readings");
            Dictionary<string, Dictionary<string, string>> entryDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(res.Body);
            Dictionary<string, string> rawData = new Dictionary<string, string>();
            foreach (KeyValuePair<string, Dictionary<string, string>> pair in entryDict)
            {
                string dataVal = (pair.Value)["data"];
                if (dataVal.Length <= 20)
                {
                    rawData.Add(pair.Key, dataVal);
                }
            }
            List<double> TDS = new List<double>();
            List<double> time = new List<double>();
            foreach (string s in rawData.Values)
            {
                TDS.Add(double.Parse(s.Split(';')[0]));

            }

            foreach (string s in rawData.Keys)
            {
                time.Add(DateTime.Parse(s).Subtract(DateTime.Parse(rawData.Keys.First())).TotalSeconds);
            }
            OriginalDate = rawData.Keys.First();
            return (time, TDS);
        }

        public (List<double>, List<double>) GetTimeCO2Series()
        {
            FirebaseResponse res = client.Get(@"UsersData/4QJojUdQ61OtiaYiUh7ENMUmHgf1/readings");
            Dictionary<string, Dictionary<string, string>> entryDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(res.Body);
            Dictionary<string, string> rawData = new Dictionary<string, string>();
            foreach (KeyValuePair<string, Dictionary<string, string>> pair in entryDict)
            {
                string dataVal = (pair.Value)["data"];
                if(dataVal.Length <= 20)
                {
                    rawData.Add(pair.Key, dataVal);
                } 
            }
            List<double> CO2 = new List<double>();
            List<double> time = new List<double>();
            foreach (string s in rawData.Values)
            {
                CO2.Add(double.Parse(s.Split(';')[2]));

            }

            foreach (string s in rawData.Keys)
            {
                time.Add(DateTime.Parse(s).Subtract(DateTime.Parse(rawData.Keys.First())).TotalSeconds);
            }
            OriginalDate = rawData.Keys.First();
            return (time, CO2);
        }

        public (List<double>, List<double>) GetTempTDSSeries()
        {
            FirebaseResponse res = client.Get(@"UsersData/4QJojUdQ61OtiaYiUh7ENMUmHgf1/readings");
            Dictionary<string, Dictionary<string, string>> entryDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(res.Body);
            Dictionary<string, string> rawData = new Dictionary<string, string>();
            foreach (KeyValuePair<string, Dictionary<string, string>> pair in entryDict)
            {
                string dataVal = (pair.Value)["data"];
                if (dataVal.Length <= 20)
                {
                    rawData.Add(pair.Key, dataVal);
                }
            }
            List<double> Temp = new List<double>();
            List<double> TDS = new List<double>();
            foreach (string s in rawData.Values)
            {
                Temp.Add(double.Parse(s.Split(';')[1]));
                TDS.Add(double.Parse(s.Split(';')[0]));

            }
            return (Temp, TDS);
        }

    }
}
