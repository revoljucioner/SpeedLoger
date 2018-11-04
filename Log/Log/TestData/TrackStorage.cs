using System;
using Log.Models;

namespace Log.TestData
{
    public static class TrackStorage
    {
        public static Track GetCommonSanFranciscoTrack => new Track
        {
            Imei = "862840034891966",
            DeviceId = "bbc29aac02853470",
            StartDateTime = DateTime.Parse("2018-01-01T10:00:01.00000Z"),
            EndDateTime = DateTime.Parse("2018-01-01T10:10:12.00000Z"),
            SnappedPointsArraySerialize = "[\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": 37.797534,\r\n      \"longitude\": -122.401827\r\n    },\r\n    \"time\": \"2018-01-01T10:00:00.00000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.797510\",\r\n      \"longitude\": \"-122.402060\"\r\n    },\r\n    \"time\": \"2018-01-01T10:00:01.00000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.790269\",\r\n      \"longitude\": \"-122.400589\"\r\n    },\r\n    \"time\": \"2018-01-01T10:00:01.50000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.790265\",\r\n      \"longitude\": \"-122.400474\"\r\n    },\r\n    \"time\": \"2018-01-01T10:00:02.00000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.790228\",\r\n      \"longitude\": \"-122.400391\"\r\n    },\r\n    \"time\": \"2018-01-01T10:00:03.00000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.790126\",\r\n      \"longitude\": \"-122.400360\"\r\n    },\r\n    \"time\": \"2018-01-01T10:00:04.00000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.789250\",\r\n      \"longitude\": \"-122.401451\"\r\n    },\r\n    \"time\": \"2018-01-01T10:00:05.00000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.788440\",\r\n      \"longitude\": \"-122.400396\"\r\n    },\r\n    \"time\": \"2018-01-01T10:00:06.00000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.787999\",\r\n      \"longitude\": \"-122.399780\"\r\n    },\r\n    \"time\": \"2018-01-01T10:00:07.00000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.786736\",\r\n      \"longitude\": \"-122.398202\"\r\n    },\r\n    \"time\": \"2018-01-01T10:00:08.00000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.786345\",\r\n      \"longitude\": \"-122.397722\"\r\n    },\r\n    \"time\": \"2018-01-01T10:00:09.00000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.785983\",\r\n      \"longitude\": \"-122.397295\"\r\n    },\r\n    \"time\": \"2018-01-01T10:00:10.00000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.785559\",\r\n      \"longitude\": \"-122.396728\"\r\n    },\r\n    \"time\": \"2018-01-01T10:00:11.00000Z\"\r\n  },\r\n  {\r\n    \"Position\": {\r\n      \"latitude\": \"37.780624\",\r\n      \"longitude\": \"-122.390541\"\r\n    },\r\n    \"time\": \"2018-01-01T10:10:12.00000Z\"\r\n  }\r\n]\r\n"
        };
    }
}



