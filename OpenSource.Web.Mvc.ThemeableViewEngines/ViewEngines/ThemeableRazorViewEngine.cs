using System;
using System.Web.Mvc;

namespace OpenSource.Web.Mvc.ThemeableViewEngines
{
    public class ThemeableRazorViewEngine : ThemeableBuildManagerViewEngine
    {
        public ThemeableRazorViewEngine()
        {
            AreaViewLocationFormats = new[] {
                                                "~/Areas/{2}/Views/Shared/Themes/{3}/{0}.cshtml",
                                                "~/Areas/{2}/Views/Shared/Themes/{3}/{0}.vbhtml",

                                                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                                                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                                                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                                                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
                                            };

            AreaMasterLocationFormats = new[] {
                                                  "~/Areas/{2}/Views/Shared/Themes/{3}/{0}.cshtml",
                                                  "~/Areas/{2}/Views/Shared/Themes/{3}/{0}.vbhtml",

                                                  "~/Areas/{2}/Views/{1}/{0}.cshtml",
                                                  "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                                                  "~/Areas/{2}/Views/Shared/{0}.cshtml",
                                                  "~/Areas/{2}/Views/Shared/{0}.vbhtml"
                                              };

            AreaPartialViewLocationFormats = new[] {
                                                       "~/Areas/{2}/Views/Shared/Themes/{3}/{0}.cshtml",
                                                       "~/Areas/{2}/Views/Shared/Themes/{3}/{0}.vbhtml",

                                                       "~/Areas/{2}/Views/{1}/{0}.cshtml",
                                                       "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                                                       "~/Areas/{2}/Views/Shared/{0}.cshtml",
                                                       "~/Areas/{2}/Views/Shared/{0}.vbhtml"
                                                   };

            ViewLocationFormats = new[] {
                                            "~/Views/Shared/Themes/{2}/{0}.cshtml",
                                            "~/Views/Shared/Themes/{2}/{0}.vbhtml",

                                            "~/Views/{1}/{0}.cshtml",
                                            "~/Views/{1}/{0}.vbhtml",
                                            "~/Views/Shared/{0}.cshtml",
                                            "~/Views/Shared/{0}.vbhtml"
                                        };

            MasterLocationFormats = new[] {
                                              "~/Views/Shared/Themes/{2}/{0}.cshtml",
                                              "~/Views/Shared/Themes/{2}/{0}.vbhtml",

                                              "~/Views/{1}/{0}.cshtml",
                                              "~/Views/{1}/{0}.vbhtml",
                                              "~/Views/Shared/{0}.cshtml",
                                              "~/Views/Shared/{0}.vbhtml"
                                          };

            PartialViewLocationFormats = new[] {
                                                   "~/Views/Shared/Themes/{2}/{0}.cshtml",
                                                   "~/Views/Shared/Themes/{2}/{0}.vbhtml",

                                                   "~/Views/{1}/{0}.cshtml",
                                                   "~/Views/{1}/{0}.vbhtml",
                                                   "~/Views/Shared/{0}.cshtml",
                                                   "~/Views/Shared/{0}.vbhtml"
                                               };

            ViewStartFileExtensions = new[] { "cshtml", "vbhtml", };
#if LOCALIZE
            PageExtensions = new[] {
                                    ".cshtml",
                                    ".vbhtml"
            };
#endif
        }

        public string[] ViewStartFileExtensions { get; set; }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new RazorView(controllerContext, partialPath, null, false, ViewStartFileExtensions);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new RazorView(controllerContext, viewPath, masterPath, true, ViewStartFileExtensions);
        }

        protected override bool IsValidCompiledType(ControllerContext controllerContext, string virtualPath, Type compiledType)
        {
            return typeof(ControllerContext).IsAssignableFrom(compiledType);
            //return typeof(WebViewPage).IsAssignableFrom(compiledType);    // DEGT MVC3
        }
    }
}
/*=====================================================================================
 *                          H  I  S  T  O  R  Y
 *=====================================================================================
 * 01.dec.2015 DEGT Adapted to work with MVC 5
 */
