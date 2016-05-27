using Aveva.Builder.Builders;
using Aveva.Interfaces.Intaerfaces;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aveva.Controllers
{
    public class MainController : Controller
    {
        private IViewModelBuilder _viewModelBuilder;

        private Item _homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath.ToString());

        public MainController()
        {
            _viewModelBuilder = new ModelBuilder();
        }

        public ActionResult Header()
        {

            return View(_viewModelBuilder.HeaderBuilder(_homeItem));
        }

        public ActionResult Body()
        {
            return View(_viewModelBuilder.PageBuilder(_homeItem));
        }

        public ActionResult Footer()
        {
            return View(_viewModelBuilder.FooterBuilder(_homeItem));
        }
    }
}