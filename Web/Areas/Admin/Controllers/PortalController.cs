using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Access;
using Web.Context;
using Web.Models.Access;

namespace Web.Areas.Admin.Controllers
{
    public class PortalController : BaseController
    {
        // GET: Admin/Portal
        public ActionResult Index()
        {
            var viewModel = GetPermissionsFromCore();
            if (viewModel.Count == 0)
            {
                return RedirectToAction("ExpiredSession");
            }
            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult ExpiredSession()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Error(string errorCode)
        {
            ViewBag.errorCode = errorCode;
            return View();
        }

        private List<ModulesViewModel> GetPermissionsFromCore()
        {
            //Só aciona o serviço caso não tenha a lista de permissões, assim agiliza.
            //Porém será necessário sair e entrar novamente caso a lista mude.
            if (Session["permissions"] != null)
            {
                var vm = (List<ModulesViewModel>)Session["permissions"];
                return vm;
            }
            var viewModel = BuildTree();
            Session["permissions"] = viewModel;
            return viewModel;
        }

        private List<ModulesViewModel> BuildTree()
        {
            var vm = new List<ModulesViewModel>();
            using (var db = new MvcDemoContext())
            {
                var modulos = db.Modules.OrderBy(x => x.Order).ToList();
                var usuario = CarregarUsuario(db);
                var usuarioGrupoPermissao = db.UserGroupPermissions
                    .FirstOrDefault(x => x.UserId == usuario.UserId);
                var permissaoFuncionalidade = db.FunctionalityPermissions.Where(x => x.PermissionGroupId == usuarioGrupoPermissao.PermissionGroupId && x.Read == true);
                var functionalities = new List<Functionality>();
                foreach (var item in permissaoFuncionalidade)
                    functionalities.Add(item.Functionality);

                foreach (var modulo in modulos)
                {
                    var model = new ModulesViewModel
                    {
                        Nome = modulo.Name,
                        ModuloId = (int)modulo.ModuleId,
                        Icone = modulo.Icon,
                        Ordem = modulo.Order,
                        Functionalities = functionalities.Where(x => x.ModuleId == modulo.ModuleId).OrderBy(x => x.Order).ToList()
                    };
                    vm.Add(model);
                }

            }
            return vm;
        }

        private User CarregarUsuario(MvcDemoContext db)
        {
            var userPerfil = this.LoadUserProfileLoggedIn();
            var user = this.GetLoggedUser();
            var usuario = db.Users.Find(user);
            return usuario;
        }
    }
}