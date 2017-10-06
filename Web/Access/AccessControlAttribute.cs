using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Access
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AccessControlAttribute : AuthorizeAttribute
    {
        public Permissions[] Permissions;
        //private static string ReqStatus { get; set; }
        public string PermissionList { get; set; }
        private static string ReqStatus { get; set; }
        private List<ModulesViewModel> _permissionsCore;

        public AccessControlAttribute(params object[] permissions)
        {
            if (permissions.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("permissions");

            PermissionList = string.Join(",", permissions.Select(r => Enum.GetName(r.GetType(), r)));
        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (httpContext.Session != null)
                _permissionsCore = (httpContext.Session["permissions"] as List<ModulesViewModel>);

            var isAuthorized = AuthorizeCore(httpContext);
            return (isAuthorized) ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var routeData = httpContext.Request.RequestContext.RouteData;
            var controller = routeData.GetRequiredString("controller");
            //var action = routeData.GetRequiredString("action");

            Permissions permissaoMinima;
            Enum.TryParse(PermissionList, out permissaoMinima);

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (ReqStatus == "permission")
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { action = "Access", Controller = "Home" }));
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
                _permissionsCore = (filterContext.HttpContext.Session["permissions"] as List<ModulesViewModel>);


            filterContext.Controller.TempData.Keep();
            base.OnAuthorization(filterContext);
        }
    }
}