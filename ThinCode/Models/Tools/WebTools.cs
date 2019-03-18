using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThinCode
{
    public class WebTools
    {
        public static string GetPreviousLink()
         {
                string referencepage = "javascript:;";
                try
                {
                    referencepage = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
                }
                catch (Exception)
                {

                    referencepage = "javascript:;";
                }
            return referencepage;
            }
    }
}