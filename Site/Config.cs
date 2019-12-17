using Newtonsoft.Json;

namespace Site
{
    public class Config
    {
        [JsonProperty("UploadPw")]
        public string UploadPassword { get; private set; }
    }
}