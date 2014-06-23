using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Moq;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
   [TestClass]
   public class PageLinksTest
   {
      HtmlHelper pageLinkHelper;
      PagingInfo pagingInfo;
      Func<int, string> pageUrlDelegate;

      [TestMethod]
      public void CanGeneratePageLinks()
      {
         PageLinkArrange();
         MvcHtmlString resultHtmlString = PageLinkAct();
         PageLinkAssert(resultHtmlString);
      }

      private void PageLinkArrange()
      {
         pageLinkHelper = null;

         pagingInfo = new PagingInfo
         {
            CurrentPage = 2,
            TotalItems = 28,
            ItemsPerPage = 10
         };

         pageUrlDelegate = i => "Page" + i;
      }

      private MvcHtmlString PageLinkAct()
      {
         return pageLinkHelper.PageLinks(pagingInfo, pageUrlDelegate);
      }

      private void PageLinkAssert(MvcHtmlString htmlString)
      {
         Assert.AreEqual(htmlString.ToString(), @"<a href=""Page1"">1</a>"
               + @"<a class=""selected"" href=""Page2"">2</a>"
               + @"<a href=""Page3"">3</a>");
      }
   }
}
