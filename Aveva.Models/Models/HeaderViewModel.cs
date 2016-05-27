using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aveva.Models.Models
{
    public class HeaderViewModel
    {
        public HeaderViewModel()
        {
            mainNavigationItems = new List<MainNavigationModel>();
        }

        public string headerText { get; set; }

        public string logoImage { get; set; }

        public string avevaWorldImage { get; set; }

        public string searchText { get; set; }

        public string searchImage { get; set; }

        public List<MainNavigationModel> mainNavigationItems { get; set; }
    }
}