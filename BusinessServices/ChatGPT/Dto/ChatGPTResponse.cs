

namespace WebRexErpAPI.BusinessServices.ChatGPT.Dto
{
    public class ChatGPTResponse
    {
        public string id { get; set; }
        public string @object { get; set; }
        public long created { get; set; }
        public string model { get; set; }
        public List<ChatGPTChoiceDto> choices { get; set; }
        public ChatGPTUsageDto usage { get; set; }
    }
}
