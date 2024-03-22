namespace WebRexErpAPI.BusinessServices.ChatGPT.Dto
{
    public class ChatGPTChoiceDto
    {
        public string text { get; set; }
        public int index { get; set; }
        public object logprobs { get; set; }
        public string finish_reason { get; set; }
    }
}
