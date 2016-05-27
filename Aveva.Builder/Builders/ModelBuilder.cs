using Aveva.Builder.Builders;
using Aveva.Interfaces.Intaerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using Aveva.Models.Models;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.Links;

namespace Aveva.Builder.Builders
{
    public class ModelBuilder : IViewModelBuilder
    {
        public HeaderViewModel HeaderBuilder(Item mainItem)
        {
            HeaderViewModel headerViewModel = new HeaderViewModel();
            var childList = mainItem.GetChildren();
            var headerItem = childList.FirstOrDefault(x => x.TemplateName == "Header");

            string logoUrl = String.Empty;
            ImageField imageField = headerItem.Fields["Logo"];
            if (imageField != null && imageField.MediaItem != null)
            {
                MediaItem media = new MediaItem(imageField.MediaItem);
                logoUrl = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(media));
            }

            string avevaImageUrl = String.Empty;
            ImageField avevaImageField = headerItem.Fields["Aveva World Image"];
            if (avevaImageField != null && avevaImageField.MediaItem != null)
            {
                MediaItem media = new MediaItem(avevaImageField.MediaItem);
                avevaImageUrl = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(media));
            }

            string searchImageUrl = String.Empty;
            ImageField searchImageField = headerItem.Fields["Search Image"];
            if (searchImageField != null && searchImageField.MediaItem != null)
            {
                MediaItem media = new MediaItem(searchImageField.MediaItem);
                searchImageUrl = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(media));
            }

            var navigationItems = headerItem.GetChildren().Where(x => x.TemplateName == "Main Navigation");
            foreach(var item in navigationItems)
            {
                var navigationItem = new MainNavigationModel();
                navigationItem.navigationText = item.Fields["Navigation Item Text"].Value;
                LinkField navigationLink = item.Fields["Link"];
                navigationItem.navigationUrl = navigationLink.GetFriendlyUrl();

                var subnavigationItems = item.GetChildren().Where(x => x.TemplateName == "Subnavigation Item");
                foreach(var subItem in subnavigationItems)
                {
                    var subnavigationItem = new SubnavigationModel();
                    subnavigationItem.subnavigationText = subItem.Fields["Item Text"].Value;
                    LinkField subNavigationLink = item.Fields["Link"];
                    subnavigationItem.subnavigationUrl = subNavigationLink.GetFriendlyUrl();

                    navigationItem.subnavigationItems.Add(subnavigationItem);
                }

                headerViewModel.mainNavigationItems.Add(navigationItem);
            }


            headerViewModel.headerText = headerItem.Fields["Header Text"].Value;
            headerViewModel.logoImage = logoUrl;
            headerViewModel.searchImage = searchImageUrl;
            headerViewModel.searchText = headerItem.Fields["Search Text"].Value;

            return headerViewModel;
        }

        public FooterViewModel FooterBuilder(Item mainItem)
        {
            FooterViewModel footerViewModel = new FooterViewModel();

            var childList = mainItem.GetChildren();
            var footerItem = childList.FirstOrDefault(x => x.TemplateName == "Footer");

            string footerImageUrl = String.Empty;
            ImageField footerImageField = footerItem.Fields["Search Image"];
            if (footerImageField != null && footerImageField.MediaItem != null)
            {
                MediaItem media = new MediaItem(footerImageField.MediaItem);
                footerImageUrl = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(media));
            }

            footerViewModel.worldwideImage = footerImageUrl;
            footerViewModel.copyright = footerItem.Fields["Copyright"].Value;
            footerViewModel.adress = footerItem.Fields["Adress"].Value;
            footerViewModel.phone = footerItem.Fields["Phone"].Value;
            footerViewModel.registration = footerItem.Fields["Registration"].Value;

            return footerViewModel;
        }

        public PageModel PageBuilder(Item mainItem)
        {
            PageModel pageModel = new PageModel();

            string headerImageUrl = String.Empty;
            ImageField headerImageField = mainItem.Fields["Header Image"];
            if (headerImageField != null && headerImageField.MediaItem != null)
            {
                MediaItem media = new MediaItem(headerImageField.MediaItem);
                headerImageUrl = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(media));
            }

            //pageModel.title = mainItem.Fields["Title"].Value;
            pageModel.mainColumn = mainItem.Fields["Main Column"].Value;
            pageModel.headerImage = headerImageUrl;

            return pageModel;
        }

        public ColumnModel LeftColumnBuilder(Item mainItem)
        {
            ColumnModel columnModel = new ColumnModel();

            MultilistField gadgetField = mainItem.Fields["Left Column"];

            if(gadgetField != null)
            {
                foreach(var ID in gadgetField.TargetIDs)
                {
                    Item currentItem = Sitecore.Context.Database.Items[ID];

                    GadgetModel gadget = new GadgetModel();

                    List<SubnavigationModel> navigationItems = new List<SubnavigationModel>();

                   

                    gadget.header = currentItem.Fields["Header"].Value;
                    gadget.content = currentItem.Fields["Content"].Value;
                    gadget.link = currentItem.Fields["Link"].Value;
                    gadget.linkText = currentItem.Fields["Link Text"].Value;

                    columnModel.gadgets.Add(gadget);
                }

                if (mainItem.TemplateName == "Page Base")
                {
                    var children = mainItem.GetChildren().Where(x => x.TemplateName == "Digital Asset Sub Page");

                    foreach (var item in children)
                    {
                        SubnavigationModel navItem = new SubnavigationModel();

                        navItem.subnavigationText = item.DisplayName;
                        navItem.subnavigationUrl = LinkManager.GetItemUrl(item);

                        columnModel.navigation.Add(navItem);
                    }
                }
                else if (mainItem.TemplateName == "Digital Asset Sub Page")
                {
                    var children = mainItem.Parent.GetChildren().Where(x => x.TemplateName == "Digital Asset Sub Page");

                    foreach (var item in children)
                    {
                        SubnavigationModel navItem = new SubnavigationModel();

                        navItem.subnavigationText = item.DisplayName;
                        navItem.subnavigationUrl = LinkManager.GetItemUrl(item);

                        columnModel.navigation.Add(navItem);
                    }

                    columnModel.currentNavigationItem = mainItem.DisplayName;
                }

            }

            return columnModel;
        }

        public ColumnModel RightColumnBuilder(Item mainItem)
        {
            ColumnModel columnModel = new ColumnModel();

            MultilistField gadgetField = mainItem.Fields["Right Column"];

            if (gadgetField != null)
            {
                foreach (var ID in gadgetField.TargetIDs)
                {
                    Item currentItem = Sitecore.Context.Database.Items[ID];

                    GadgetModel gadget = new GadgetModel();

                    gadget.header = currentItem.Fields["Header"].Value;
                    gadget.content = currentItem.Fields["Content"].Value;
                    gadget.link = currentItem.Fields["Link"].Value;
                    gadget.linkText = currentItem.Fields["Link Text"].Value;

                    columnModel.gadgets.Add(gadget);
                }
            }

            return columnModel;
        }
    }


}