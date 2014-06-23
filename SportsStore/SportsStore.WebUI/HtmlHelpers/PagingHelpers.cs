using System;
using System.Text;
using System.Web.Mvc;

using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.HtmlHelpers
{
   public static class PagingHelpers
   {
      public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
      {
         StringBuilder pageLinkResult = new StringBuilder();

         for (int pageNumber = 1; pageNumber <= pagingInfo.TotalPages; pageNumber++)
         {
            TagBuilder tagResult = new TagBuilder("a");
            tagResult.MergeAttribute("href", pageUrl(pageNumber));
            tagResult.InnerHtml = pageNumber.ToString();

            if (pageNumber == pagingInfo.CurrentPage)
               tagResult.AddCssClass("selected");

            pageLinkResult.Append(tagResult.ToString());
         }

         return MvcHtmlString.Create(pageLinkResult.ToString());
      }
   }
}