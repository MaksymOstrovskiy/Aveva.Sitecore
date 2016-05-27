using Aveva.Builder.Builders;
using Aveva.Interfaces.Intaerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aveva.Controllers
{
    public class DigitalAssetController : Controller
    {
            private IViewModelBuilder _viewModelBuilder;

            public DigitalAssetController()
            {
                _viewModelBuilder = new ModelBuilder();
            }

            public ActionResult index()
            {
                return View(_viewModelBuilder.PageBuilder(Sitecore.Context.Item));
            }
    }
}