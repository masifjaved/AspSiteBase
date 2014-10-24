using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspSiteBase.ReCaptchaPlug
{
    public class CaptchaValidator : ActionFilterAttribute
    {
        private const string RECAPTCHA_CHALLENGE_FIELD = "recaptcha_challenge_field";
        private const string RECAPTCHA_RESPONSE_FIELD = "recaptcha_response_field";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var captchaChallengeValue = filterContext.HttpContext.Request.Form[RECAPTCHA_CHALLENGE_FIELD];
            var captchaResponseValue = filterContext.HttpContext.Request.Form[RECAPTCHA_RESPONSE_FIELD];

            var captchaValidator = new Recaptcha.RecaptchaValidator
            {
                PrivateKey = "6LeWKfwSAAAAACa7_Xgmgjh5wNYRnqvrJMkYRLdu",
                RemoteIP = filterContext.HttpContext.Request.UserHostAddress,
                Challenge = captchaChallengeValue,
                Response = captchaResponseValue
            };

            var recaptchaResponse = captchaValidator.Validate();

            filterContext.ActionParameters["captchaValid"] = recaptchaResponse.IsValid;

            base.OnActionExecuting(filterContext);
        }
    }
}