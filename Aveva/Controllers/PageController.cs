using Aveva.Builder.Builders;
using Aveva.Interfaces.Intaerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aveva.Controllers
{
    public class PageController : Controller
    {
        private IViewModelBuilder _viewModelBuilder;

        public PageController()
        {
            _viewModelBuilder = new ModelBuilder();
        }

        public ActionResult Index()
        {
            return View(_viewModelBuilder.PageBuilder(Sitecore.Context.Item));
        }

        public ActionResult LeftColumn()
        {
            return View(_viewModelBuilder.LeftColumnBuilder(Sitecore.Context.Item));
        }

        public ActionResult RightColumn()
        {
            return View(_viewModelBuilder.RightColumnBuilder(Sitecore.Context.Item));
        }
    }
}