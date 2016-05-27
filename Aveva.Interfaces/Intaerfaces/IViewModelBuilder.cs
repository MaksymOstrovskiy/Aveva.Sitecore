using Aveva.Models.Models;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aveva.Interfaces.Intaerfaces
{
    public interface IViewModelBuilder
    {
        HeaderViewModel HeaderBuilder(Item mainItem);
        FooterViewModel FooterBuilder(Item mainItem);
        PageModel PageBuilder(Item mainItem);
        ColumnModel LeftColumnBuilder(Item mainItem);
        ColumnModel RightColumnBuilder(Item mainItem);
    }
}
