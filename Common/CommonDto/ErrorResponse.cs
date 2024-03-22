namespace WebRexErpAPI.Common.CommonDto
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public ErrorSource[] Errors { get; set; }
    }
}
