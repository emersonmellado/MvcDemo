using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Access;
using Web.Context;

namespace Web.Areas.Admin.Controllers
{
    [AccessControl]
    public abstract class BaseController : Controller
    {
        public string ActionName { get; private set; }
        public string ControllerName { get; private set; }

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    filterContext.ExceptionHandled = true;

        //    if (filterContext.Exception is UnauthorizedAccessException)
        //    {
        //        if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
        //        {
        //            filterContext.RequestContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

        //            filterContext.Result = new JsonResult
        //            {
        //                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
        //                ContentEncoding = Encoding.UTF8
        //            };
        //            return;
        //        }

        //        filterContext.Result = RedirectToAction("Acesso", "Home");
        //        return;
        //    }
        //    var codigoErro = OnExceptionLog(filterContext);
        //    filterContext.Result = RedirectToAction("Erro", "Home", new { codigoErro });
        //}

        //public static string OnExceptionLog(ExceptionContext exceptionContext)
        //{
        //    var log = new LogErro()
        //    {
        //        Data = DateTime.Now,
        //        Mensagem = exceptionContext.Exception.Message,
        //        TipoExcecao = exceptionContext.Exception.GetType().ToString(),
        //        PilhaChamada = exceptionContext.Exception.StackTrace,
        //        Metodo = exceptionContext.Exception.TargetSite.Name,
        //        Modulo = exceptionContext.Exception.TargetSite.Module.Name,
        //    };
        //    using (var db = new HiperClinContext())
        //    {
        //        db.LogErro.Add(log);
        //        db.SaveChanges();
        //    }
        //    return log.LogErroId.ToString();
        //}

        //internal static string DirectoryErrosApp = $"{AppDomain.CurrentDomain.BaseDirectory}{"../../ErrosApp"}";

        protected override void Execute(RequestContext requestContext)
        {
            requestContext.HttpContext.Response.Write("BaseController::Execute()<br>");
            base.Execute(requestContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionDescriptor = filterContext.ActionDescriptor;
            ActionName = actionDescriptor.ActionName;
            ControllerName = actionDescriptor.ControllerDescriptor.ControllerName;

            var permissionsCore = (filterContext.HttpContext.Session?["permissions"] as List<ModulesViewModel>);
            if (permissionsCore != null)
            {
                var funcionalidades = permissionsCore.SingleOrDefault(x => x.Functionalities.Any(m => m.Url == ControllerName));
                var funcExistente = funcionalidades?.Functionalities.FirstOrDefault(x => x.Url == ControllerName);
                if (funcExistente != null)
                {
                    using (var db = new MvcDemoContext())
                    {
                        var userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.Name);

                        var userPerfil = this.LoadUserProfileLoggedIn();
                        var permiss = db.FunctionalityPermissions.FirstOrDefault(x => x.FunctionalityId == funcExistente.FunctionalityId && x.PermissionGroup.ProfileTypeId == userPerfil);

                        if (permiss != null)
                        {
                            var grupo = db.UserGroupPermissions.FirstOrDefault(x => x.UserId == userId && x.PermissionGroupId == permiss.PermissionGroupId);


                            if (grupo != null)
                            {
                                filterContext.HttpContext.Session["permissionsCore"] = permiss;
                            }
                        }
                    }
                }
            }
            filterContext.Controller.TempData.Keep();
            base.OnActionExecuting(filterContext);
        }

        public ProfileType LoadUserProfileLoggedIn()
        {
            var userLegal = (ClaimsPrincipal)System.Web.HttpContext.Current.User;
            var claim = userLegal.Claims.First(x => x.Type.Contains("/role"));

            ProfileType userProfile;
            Enum.TryParse(claim.Value, out userProfile);

            return userProfile;
        }

        public void UiOrderingManager(OrderQuery order, string tableName, string pkName = "Id")
        {
            string query = null;
            if (order.ToPosition > order.FromPosition)
            {
                query = @"Update " + tableName + " set [Order] = [Order]-1 where [Order] between " + order.FromPosition + "+1 and " + order.ToPosition + "";
            }
            if (order.ToPosition < order.FromPosition)
            {
                query = @"Update " + tableName + " set [Order] = [Order]+1 where [Order] between " + order.ToPosition + " and " + order.FromPosition + "-1";
            }

            using (var db = new MvcDemoContext())
            {
                if (query != null)
                {
                    db.Database.ExecuteSqlCommand(query);
                    query = @"UPDATE " + tableName + " SET [Order]=" + order.ToPosition + " WHERE " + pkName + " = " +
                            order.Id + "";
                    db.Database.ExecuteSqlCommand(query);
                }
                db.SaveChanges();
            }
        }

        public class OrderQuery
        {
            public int Id { get; set; }
            public int FromPosition { get; set; }
            public int ToPosition { get; set; }
            public string Direction { get; set; }
            public string Group { get; set; }
        }

    }
}
