using System;
using System.Collections.Generic;
using Web.Models.Access;

namespace Web.Access
{
    public enum ProfileType
    {
        Attendant = 0,
        Manager = 10,
        Administrator = 20
    }

    public class PermissionList
    {
        public string ControllerName { get; set; }
        public Permissions Permission { get; set; }
    }

    [Flags]
    public enum Permissions
    {
        None = 0,
        Delete = 1,
        Update = 2,
        Read = 4,
        Create = 8,
        All = 15
    }

    //public class Modulos
    //{
    //    public int Id { get; set; }
    //    public string Nome { get; set; }

    //    public string Icone
    //    {
    //        get
    //        {
    //            var icone = "add_circle_outline";
    //            if (Url.Contains("Relat") || Url.Contains("Consult"))
    //                icone = "search";
    //            if (Url.Contains("Param"))
    //                icone = "settings";
    //            return icone;
    //        }
    //    }

    //    public int IdModuloPai { get; set; }
    //    public int FlagPermissao { get; set; }
    //    public string Url { get; set; }
    //}

    public class ModulesViewModel
    {
        public int ModuloId { get; set; }
        public string Nome { get; set; }
        public string Icone { get; set; }
        public string Url { get; set; }
        public int Ordem { get; set; }
        public List<Functionality> Functionalities { get; set; } = new List<Functionality>();
    }

}
