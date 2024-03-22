using Microsoft.CodeAnalysis.FlowAnalysis;

namespace WebRexErpAPI.BusinessServices.ChatGPT.Dto
{
    public class ChatGPTCompletionsDto
    {
        public string? Prompt { get; set; }
        public int? MaxTokens { get;internal set; } = 10;
         public string? Model { get; set; } = "gpt-3.5-turbo-instruct";
      //  public string? Model { get;internal set; } = "gpt-4";
        public string? FileName { get;internal set; }
        public float? Temperature { get;internal  set; } = (float)0.2;
    }
}
