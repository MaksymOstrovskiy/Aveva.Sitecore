using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aveva.Models.Models
{
    public class ColumnModel
    {
        public List<GadgetModel> gadgets { get; set; }

        public List<SubnavigationModel> navigation { get; set; }

        public string currentNavigationItem { get; set; }

        public ColumnModel()
        {
            gadgets = new List<GadgetModel>();
            navigation = new List<SubnavigationModel>();

        }

      
    }
}