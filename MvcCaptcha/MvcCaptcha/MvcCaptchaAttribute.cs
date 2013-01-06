using System;
using System.Text;
using System.Web.Mvc;

namespace ImageDraw
{
  public class MvcCaptchaAttribute : ActionFilterAttribute, IActionFilter
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      var captchaActual = filterContext.HttpContext.Request.Form["CaptchaActual"];

      if (captchaActual == "")
      {
        filterContext.Controller.ViewData.ModelState.AddModelError("CaptchaActual", "Captcha is required.");
        return;
      }

      var captchaExpected = Encoding.Unicode.GetString(Convert.FromBase64String(filterContext.HttpContext.Request.Form["CaptchaExpected"]));

      if (captchaActual != captchaExpected)
        filterContext.Controller.ViewData.ModelState.AddModelError("CaptchaActual", "The numeric answer for your captcha is invalid");
    }
  }
}