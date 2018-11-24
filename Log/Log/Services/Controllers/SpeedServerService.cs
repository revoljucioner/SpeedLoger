using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Log.Services.Models;
using Newtonsoft.Json;

namespace Log.Services.Controllers
{
    public class SpeedServerService
    {
        private string conTest = "[\r\n  {\r\n    \"location\": {\r\n      \"latitude\": -35.2807341,\r\n      \"longitude\": 149.1291511\r\n    },\r\n    \"time\": \"2018-02-18T01:00:00.0000000+00:00\"\r\n  },\r\n  {\r\n    \"location\": {\r\n      \"latitude\": -35.2807342,\r\n      \"longitude\": 149.1291512\r\n    },\r\n    \"time\": \"2018-02-18T01:01:00.0000000+00:00\"\r\n  },\r\n  {\r\n    \"location\": {\r\n      \"latitude\": -35.2807343,\r\n      \"longitude\": 149.1291513\r\n    },\r\n    \"time\": \"2018-02-18T01:02:00.0000000+00:00\"\r\n  },\r\n  {\r\n    \"location\": {\r\n      \"latitude\": -35.2807344,\r\n      \"longitude\": 149.1291514\r\n    },\r\n    \"time\": \"2018-02-18T01:03:00.0000000+00:00\"\r\n  },\r\n  {\r\n    \"location\": {\r\n      \"latitude\": -35.280736,\r\n      \"longitude\": 149.1293\r\n    },\r\n    \"time\": \"2018-02-18T01:04:00.0000000+00:00\"\r\n  }\r\n]";

        private string UrlTest = "https://roads.googleapis.com/v1/snapToRoads?path=-35.2807341,149.1291511|-35.2807342,149.1291512|-35.2807343,149.1291513&interpolate=True&key=AIzaSyAKAnsBhmAZCXLGW65jzgLItk4DmXkuMt4 ";
        private readonly string Url = "http://revoljucioner-001-site1.btempurl.com/api/SpeedServer";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<SpeedModel> GetSnappedPointsArrayFromSpeedServer(IEnumerable<SnappedPointRequest> snappedPointsRequestArray)
        {
            //snappedPointsRequestArray = JsonConvert.DeserializeObject<SnappedPointRequest[]>(conTest);

            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(snappedPointsRequestArray),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content.ToString());

            var contentString = await response.Content.ReadAsStringAsync();
            SpeedModel value = null;
            try
            {
                value = JsonConvert.DeserializeObject<SpeedModel>(contentString);
            }
            catch (Exception exception)
            {
                throw new Exception("Server return wrong data. Please, try later.");
            }
            return value;
        }

        //public async Task<SnappedPointResponse[]> GetSnappedPointsArrayFromSpeedServer(SnappedPointRequest[] snappedPointsRequestArray)
        //{
        //    snappedPointsRequestArray = JsonConvert.DeserializeObject<SnappedPointRequest[]>(conTest);
        //    //
        //    HttpClient client = GetClient();
        //    try
        //    {
        //        var response = await client.GetAsync(UrlTest);
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return new SnappedPointResponse[0];
        //}
    }
}