using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web.Helpers
{
    public static class CustomValidation
    {
        public static IHtmlString CustomValidationSummary(this HtmlHelper helper, string validationMessage = "")
        {
            var html = new StringBuilder(string.Empty);
            if (!helper.ViewData.ModelState.IsValid)
            {
                html.Append(@"<div class=""row"">
                        <h4> Check this man: </h4>
                        <table class=""table table-checkbox""><tbody>");

                foreach (var key in helper.ViewData.ModelState.Keys.Where(key => key == "e"))
                {
                    foreach (var error in helper.ViewData.ModelState[key].Errors)
                    {
                        html.Append(@"<tr><td><i class=""material-icons text-danger error""> error</i></td><td>" +
                                    helper.Encode(error.ErrorMessage) + @"</td>");
                    }
                }
                html.Append(@"</tbody></table></div>");
            }
            return new HtmlString(html.ToString());
            //return null;
        }
    }
}