using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace S22_MarsPhotosReviewer
{
    public class NetworkingManager
    {
        HttpClient httpClient = new HttpClient();
        public async Task<List<MarsPhoto>> getMarsPhotos(string rover_name, string earthdate)
        {
            string marsapi = "https://api.nasa.gov/mars-photos/api/v1/rovers/"
                +rover_name+"/photos?earth_date="+earthdate+
                "&api_key=wArFt3VHK2bJ853DS1uac88LIsGSaFyF4lfgIrkB";
           var response = await httpClient.GetAsync(marsapi);
           if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<MarsPhoto>();
            } else
            {
               var jsonString = await response.Content.ReadAsStringAsync();
               var dic = JsonConvert.DeserializeObject<Dictionary<string,List<MarsPhoto>>>(jsonString);
                var array = dic.ElementAt(0).Value;
                return array;
            }

        }
    }
}
