namespace WebRexErpAPI.Common.CommonDto
{
    public class Response<T> : ResposeMessage
    {
        public Response()
        {

        }
        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
      
    }

    public class ResposeMessage
    {
        public bool Succeeded { get; set; } = true;
        public string[]? Errors { get; set; } = null;
        public string? Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
