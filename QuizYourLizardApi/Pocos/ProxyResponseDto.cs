using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Pocos
{
    public class ProxyResponseDto
    {
        public bool IsSuccessStatus { get; set; }
        public string Content { get; set; }
    }
}