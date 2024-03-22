namespace WebRexErpAPI.BusinessServices.ChatGPT.Dto
{
    public class ChatGPTUsageDto
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }
}
