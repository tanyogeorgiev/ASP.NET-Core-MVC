
namespace HealthR.Web.Areas.Admin.ViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminUserListingsViewModel
    {
        public IEnumerable<AdminUserViewModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
