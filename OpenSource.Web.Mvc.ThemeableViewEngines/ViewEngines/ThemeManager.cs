using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public static class ThemeManager
    {
        /// <summary>
        /// Virtual path of Theme CSS and Images
        /// </summary>
        public const string ThemesRootVirtualFolder = "~/Content/Themes";

        /// <summary>
        /// Get the theme NAME by first looking for a 'theme' session variable. If that is empty/null
        /// then look for the theme selection in the web.config system.web/pages[@theme] attribute. If
        /// that is also empty default to an empty theme name (not null).
        /// So it is possible to override the web.config theme by means of a session variable
        /// </summary>
        /// <param name="helper">helper class needed to access the context</param>
        /// <returns>selected theme name or empty</returns>
        public static string GetThemeName(HttpContextBase context)
        {
            // First try with Session[]
            string sessionTheme = (string)context.Session["theme"];
            if (!string.IsNullOrEmpty(sessionTheme))
            {
                return sessionTheme;
            }
            else
            {
                // look in web.config for a switch that allows the Session theme to be set to empty
                string setting = Configuration.WebConfigurationManager.AppSettings.Get("AllowEmptyTheme");
                
                // Now try with web.config
                System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

                // IF there is an AllowEmptyTheme appSetting AND it is a valid boolean (true/false) AND is set to true
                // given that the Session theme variable is Null/Empty we allow a themeless rendering.
                if (!string.IsNullOrEmpty(setting))
                {
                    bool validBoolean;
                    if (Boolean.TryParse(setting, out validBoolean) && validBoolean)
                        return String.Empty;
                }
               
                // Attempt to use the theme defined in the system.web/pages element's 'theme' attribute
                System.Web.Configuration.PagesSection section = (System.Web.Configuration.PagesSection)config.GetSection("system.web/pages");
               
                if (!string.IsNullOrEmpty(section.Theme))
                {
                    return section.Theme;
                }
            }
            return string.Empty;    // if not defined in web.config there is nothing we can do
        }

        public static string GetThemeName(HttpContext context)
        {
            return GetThemeName(new HttpContextWrapper(context));
        }

        /// <summary>
        /// Returns true if theme has been disabled which is accomplished by setting the theme name
        /// to either null, "None" (case independent) or an empty string.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static bool IsThemeDisabled(HttpContext ctx)
        {
            string themeName = GetThemeName(ctx);
            return (themeName == null || "none".Equals(themeName.ToLower()) || String.Empty.Equals(themeName));
        }

        /// <summary>
        /// Returns true if theme has been disabled which is accomplished by setting the theme name
        /// to either null, "None" (case independent) or an empty string.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static bool IsThemeDisabled(HttpContextBase ctx)
        {
            string themeName = GetThemeName(ctx);
            return (themeName == null || "none".Equals(themeName.ToLower()) || String.Empty.Equals(themeName));
        }

        #region Extensions
        /// <summary>
        /// Get the theme name currently in use
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string GetThemeName(this HtmlHelper helper)
        {
            return GetThemeName(helper.ViewContext.HttpContext);
        }
        #endregion
    }
}