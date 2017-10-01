using MvcApp.Models.FilesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Infrastructure.HtmlHelpers
{
    /// <summary>
    /// Provides custom html helpers
    /// </summary>
    public static class HelperMethods
    {
        #region Public methods
        /// <summary>
        /// Generate content of table from provided components.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="components">The components.</param>
        /// <returns></returns>
        public static MvcHtmlString FilesTableContent(this HtmlHelper html, IEnumerable<ComponentViewModel> components)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in components)
            {
                TagBuilder tagBuilder = new TagBuilder("tr");
                tagBuilder.AddCssClass("cursor-pointer");
                tagBuilder.Attributes["id"] = item.Id.ToString();
                if (item.ContentType == "folder")
                    tagBuilder.AddCssClass("folder");
                tagBuilder.InnerHtml = BuildColumns(item);
                sb.Append(tagBuilder.ToString());
            }
            return new MvcHtmlString(sb.ToString());
        } 
        #endregion

        #region Private methods

        private static string BuildColumns(ComponentViewModel item)
        {
            var sb = new StringBuilder();
            sb.Append(BuildFileName(item));
            sb.Append(BuildDateUploaded(item));
            sb.Append(BuildMemorySize(item));
            return sb.ToString();
        }

        private static string BuildFileName(ComponentViewModel item)
        {
            TagBuilder tb = new TagBuilder("td");
            if (item.ContentType == "folder")
                tb.InnerHtml += new TagBuilder("span") { Attributes = { { "class", "glyphicon glyphicon-folder-open" } } };
            tb.InnerHtml += (" " + item.Name);
            return tb.ToString();
        }

        private static string BuildDateUploaded(ComponentViewModel item)
        {
            TagBuilder tb = new TagBuilder("td");
            tb.SetInnerText(item.DateUploaded.ToLongDateString() + " " + item.DateUploaded.ToLongTimeString());
            return tb.ToString();
        }

        private static string BuildMemorySize(ComponentViewModel item)
        {
            TagBuilder tb = new TagBuilder("td");
            tb.SetInnerText(String.Format(new MvcApp.Infrastructure.Formatters.MemorySizeFormatter(), "{0:memory}", item.Size));
            return tb.ToString();
        } 
        #endregion
    }
}