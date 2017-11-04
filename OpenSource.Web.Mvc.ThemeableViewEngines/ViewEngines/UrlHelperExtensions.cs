using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace System.Web.Mvc
{
    /// <summary>
    /// Author. Didimo Grimaldo
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Get the theme name or empty string if not was defined. First we look in the Session bag for a 'theme'
        /// variable, if not then on the web configuration file.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string ThemeName(this UrlHelper helper)
        {
            return ThemeManager.GetThemeName(helper.RequestContext.HttpContext);
        }

        public static string ThemeStylesheetVirtual(this UrlHelper helper)
        {
            if (helper == null || helper.RequestContext.HttpContext == null)
                throw new InvalidOperationException("Context cannot be null");

            string result = string.Empty;
            string themeName = ThemeManager.GetThemeName(helper.RequestContext.HttpContext);
            if (!string.IsNullOrEmpty(themeName))
            {
                string themeCssDir = string.Format("~/Content/Themes/{0}", themeName);
                if (Directory.Exists(helper.RequestContext.HttpContext.Server.MapPath(themeCssDir)))
                {
                    string themedCss = string.Format("{0}/{1}.css", themeCssDir, themeName);
                    if (File.Exists(helper.RequestContext.HttpContext.Server.MapPath(themedCss)))
                    {
                        result = themedCss;
                    }
                    else
                    {
                        themedCss = string.Format("{0}/Site.css", themeCssDir);
                        if (File.Exists(helper.RequestContext.HttpContext.Server.MapPath(themedCss)))
                        {
                            result = themedCss;
                        }
                    }
                }
            }
            
            return result;
        }

        public static string ThemeStylesheetContent(this UrlHelper helper)
        {
            if (helper == null || helper.RequestContext.HttpContext == null)
                throw new InvalidOperationException("Context cannot be null");

            string result = string.Empty;
            string themeName = ThemeManager.GetThemeName(helper.RequestContext.HttpContext);
            if (!string.IsNullOrEmpty(themeName))
            {
                string themeCssDir = string.Format("~/Content/Themes/{0}", themeName);
                if (Directory.Exists(helper.RequestContext.HttpContext.Server.MapPath(themeCssDir)))
                {
                    string themedCss = string.Format("{0}/{1}.css", themeCssDir, themeName);
                    if (File.Exists(helper.RequestContext.HttpContext.Server.MapPath(themedCss)))
                    {
                        result = helper.Content(themedCss);
                    }
                    else
                    {
                        themedCss = string.Format("{0}/Site.css", themeCssDir);
                        if (File.Exists(helper.RequestContext.HttpContext.Server.MapPath(themedCss)))
                        {
                            result = helper.Content(themedCss);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get the URL of a theme(d) stylesheet (a CSS in a theme folder) by retrieving the current theme
        /// from the <see cref="ThemeName"/> property to assume the named stylesheet is in the 
        /// <see cref="ThemesRootVirtualFolder"/> virtual folder. The return value is in a format suitable
        /// for the @Url helpers (starts with ~/) unless the <paramref name="asVirtualPath"/> is false.
        /// </summary>
        /// <param name="themedCss">stylesheet name with or without .CSS extension</param>
        /// <param name="asVirtualPath">if true return path starts with ~/ otherwise just /</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">themedCss parameter was null or empty</exception>
        public static string ThemedStylesheet(this UrlHelper helper, string themedCss, bool asVirtualPath)
        {
            string css = string.Empty;
            if (!string.IsNullOrEmpty(themedCss))
            {
                themedCss = themedCss.Trim();
                if (!themedCss.ToLower().EndsWith(".css"))
                    themedCss += ".css";    // append CSS extension
                css = string.Format("{0}/{1}/{2}", ThemeManager.ThemesRootVirtualFolder, ThemeManager.GetThemeName(helper.RequestContext.HttpContext), themedCss);
                // remove the virtual path marker
                if (!asVirtualPath)
                    css = css.Replace("~", "");
            }
            else
                throw new ArgumentNullException("specified stylesheet name is null/empty");
            return css;
        }
    }

    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders HTML for a drop down list with a preselected value
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListPreselectByText(this HtmlHelper helper, IEnumerable<SelectListItem> list, string name, string text)
        {
            System.Text.StringBuilder sb = new Text.StringBuilder();
            sb.AppendFormat("<select id=\"{0}\" name=\"{0}\">", name, name);
            foreach (SelectListItem item in list)
            {
                if (text != null &&  item.Text.Equals(text))
                {
                    sb.AppendFormat("  <option value=\"{0}\" selected=\"selected\">{1}</option>", item.Value, item.Text);
                }
                else
                {
                    sb.AppendFormat("  <option value=\"{0}\">{1}</option>", item.Value, item.Text);
                }
            }
            sb.Append("</select>");
            return new MvcHtmlString(sb.ToString());
        }
    }
}