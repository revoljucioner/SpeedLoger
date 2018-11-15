using System.Net.Http;
using System.Threading.Tasks;
using Log.Services.Models;
using Newtonsoft.Json;

namespace Log.Services.Controllers
{
    public class SpeedServerController
    {
        private string conTest = "[\r\n  {\r\n    \"location\": {\r\n      \"latitude\": -35.2807341,\r\n      \"longitude\": 149.1291511\r\n    },\r\n    \"time\": \"2018-02-18T01:00:00.0000000+00:00\"\r\n  },\r\n  {\r\n    \"location\": {\r\n      \"latitude\": -35.2807342,\r\n      \"longitude\": 149.1291512\r\n    },\r\n    \"time\": \"2018-02-18T01:01:00.0000000+00:00\"\r\n  },\r\n  {\r\n    \"location\": {\r\n      \"latitude\": -35.2807343,\r\n      \"longitude\": 149.1291513\r\n    },\r\n    \"time\": \"2018-02-18T01:02:00.0000000+00:00\"\r\n  },\r\n  {\r\n    \"location\": {\r\n      \"latitude\": -35.2807344,\r\n      \"longitude\": 149.1291514\r\n    },\r\n    \"time\": \"2018-02-18T01:03:00.0000000+00:00\"\r\n  },\r\n  {\r\n    \"location\": {\r\n      \"latitude\": -35.280736,\r\n      \"longitude\": 149.1293\r\n    },\r\n    \"time\": \"2018-02-18T01:04:00.0000000+00:00\"\r\n  }\r\n]";
        private readonly string _url = "http://localhost:57929/api/SpeedServer";
        public async Task<SnappedPointResponse[]> GetSnappedPointsArrayFromSpeedServer(SnappedPointRequest[] snappedPointsRequestArray)
        {
            // сериализация объекта с помощью Json.NET
            string json = JsonConvert.SerializeObject(snappedPointsRequestArray);
            //HttpContent content = new StringContent(json);
            HttpContent content = new StringContent(conTest);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("_url", content);
            var snappedPointsResponsetArray = response.Content;
            return new SnappedPointResponse[0];
        }       
    }
}