﻿using Microsoft.AspNetCore.Authentication;

namespace WebRexErpAPI.Business.Email.Dto
{
    public class EmailDto
    {

        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? ToAddress { get; set; }

    }
}
