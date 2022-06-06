using System;
using Newtonsoft.Json;

namespace S22_MarsPhotosReviewer
{
    public class MarsPhoto
    {
        public int id { get; set; }

        [JsonProperty("img_src")]
        public string image_url { get; set; }

        public Camera camera { get; set; }

        public MarsPhoto()
        {
        }
    }

    public class Camera
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
