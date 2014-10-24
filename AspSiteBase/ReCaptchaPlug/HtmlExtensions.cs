using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace AspSiteBase.ReCaptchaPlug
{
    public static class HtmlExtensions
    {
        public static string GenerateCaptcha(this HtmlHelper helper)
        {
            var captchaControl = new Recaptcha.RecaptchaControl
            {
                ID = "recaptcha",
                Theme = "white",
                PublicKey = "6LeWKfwSAAAAAFMCHEm60Z6J2McSed_hiSMfWJnR",
                PrivateKey = "6LeWKfwSAAAAACa7_Xgmgjh5wNYRnqvrJMkYRLdu"
            };

            var textWriter = new StringWriter();
            var htmlWriter = new HtmlTextWriter(textWriter);

            captchaControl.RenderControl(htmlWriter);

            return htmlWriter.InnerWriter.ToString();

        }
    }
}