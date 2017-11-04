// ***********************************************************************
//             Copyright (c)2013 Coralys Design & Consultancy
//                        All Rights Reserved
// -----------------------------------------------------------------------
// This software is copyrighted by its owner and no part of it should be
// published or (re)distributed without prior written consent from its
// owner.
// -----------------------------------------------------------------------
// Project   : 
// Product   : 
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OpenSource.Web.Mvc.ThemeableViewEngines.ViewEngines
{
    /// <summary>
    /// A delegate that would be invoked when the view engine supports Localization and a view or
    /// Layout or file has been localized by the view engine.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LocalizedViewEventHandler(object sender, LocalizedViewEventArgs e);

    public class LocalizedViewEventArgs : System.EventArgs
    {
        /**************************************************************************************************
         *                     M E M B E R    V A R I A B L E S  &   C O N S T A N T S
         **************************************************************************************************/
        private bool isLocalized;
        private string controllerName;
        private string resourceFallback;
        private string resourceLocalized;

        /**************************************************************************************************
         *                     P R O P E R T I E S
         **************************************************************************************************/
        #region Properties
        /// <summary>
        /// get whether the file/view was localized (true), if not (false) then the originall fallback
        /// path was used.
        /// </summary>
        public bool IsLocalized
        {
            get { return this.isLocalized; }
        }

        public string FallbackPath
        {
            get { return this.resourceFallback; }
        }

        public string LocalizedPath
        {
            get { return this.resourceLocalized; }
        }
        #endregion

        /**************************************************************************************************
         *                     C O N S T R U C T O R ( S )
         **************************************************************************************************/
        #region Constructor(s)
        public LocalizedViewEventArgs(ControllerContext ctx, string fallback) : base()
        {
            this.isLocalized = false;
            this.controllerName = ctx.Controller.GetType().ToString();
            this.resourceFallback = fallback;
            this.resourceLocalized = string.Empty;
        }

        public LocalizedViewEventArgs(ControllerContext ctx, string fallback, string localized) : this(ctx, fallback)
        {
            this.isLocalized = true;
            this.resourceFallback = fallback;
            this.resourceLocalized = localized;
        }
        #endregion

    }
}
