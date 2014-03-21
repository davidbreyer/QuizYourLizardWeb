using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.CrossCutting
{
    public class Functions
    {
        public static string GetApiUri()
        {
            Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            return url.GetLeftPart(UriPartial.Authority);
        }
    }
}