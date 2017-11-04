using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace OpenSource.Web.Mvc.Controllers
{

    public class ThemeSwitcherController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            var theme = Session["theme"] as string ?? string.Empty;

            // Should be cached in real application
            //var themePath = Server.MapPath("~/Views/Shared/Themes");
            var themePath = Server.MapPath("~/Content/Themes"); // DEGT now look in themes rather than Views
            var themeDirectory = new DirectoryInfo(themePath);

            var themes = themeDirectory.GetDirectories()
                                       .Select(d => d.Name)
                                       .Select(d => new SelectListItem { Text = d, Value = Url.Action("Switch", "ThemeSwitcher", new { theme = d }), Selected = d.Equals(theme) })
                                       .OrderBy(t => t.Text)
                                       .ToList();

            // Add the "None" option to the dropdownlist
            themes.Insert(0, new SelectListItem { Text = "None", Value = Url.Action("Switch", "ThemeSwitcher", new { theme = string.Empty }), Selected = theme.Length == 0 });
           
            return PartialView(themes);
        }

        public ActionResult Switch(string theme)
        {
            Session["theme"] = theme ?? string.Empty;

            var url = Request.UrlReferrer != null ?
                      Request.UrlReferrer.ToString() :
                      Url.Action("Index", "Home");

            return Redirect(url);
        }
    }
}