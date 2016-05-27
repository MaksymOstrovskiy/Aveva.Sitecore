using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aveva.Models.Models
{
    public class MainNavigationModel
    {
        public MainNavigationModel()
        {
            subnavigationItems = new List<SubnavigationModel>();
        }

        public string navigationText { get; set; }

        public string navigationUrl { get; set; }

        public List<SubnavigationModel> subnavigationItems { get; set; }
    }
}