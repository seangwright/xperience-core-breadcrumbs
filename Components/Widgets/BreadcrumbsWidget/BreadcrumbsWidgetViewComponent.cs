using CMS.Core;
using CMS.DataEngine;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using Xperience.Components.Widgets.BreadcrumbsWidget;

[assembly: RegisterWidget(BreadcrumbsWidgetViewComponent.IDENTIFIER, typeof(BreadcrumbsWidgetViewComponent), "Breadcrumbs", typeof(BreadcrumbsWidgetProperties), Description = "Displays the breadcrumbs of the current page", IconClass = "icon-l-list-article")]

namespace Xperience.Components.Widgets.BreadcrumbsWidget
{
    public class BreadcrumbsWidgetViewComponent : ViewComponent
    {
        public const string IDENTIFIER = "Xperience.BreadcrumbsWidget";

        public ViewViewComponentResult Invoke(ComponentViewModel<BreadcrumbsWidgetProperties> viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            var pageDataContextRetriever = Service.Resolve<IPageDataContextRetriever>();
            var current = pageDataContextRetriever.Retrieve<TreeNode>().Page;

            var hierarchy = GetHierarchy(current, viewModel.Properties.ShowSiteLink, viewModel.Properties.ShowContainers);
            var model = new BreadcrumbsWidgetViewModel()
            {
                Hierarchy = hierarchy,
                Separator = viewModel.Properties.Separator,
                ClassName = viewModel.Properties.ClassName
            };

            return View("~/Components/Widgets/BreadcrumbsWidget/_BreadcrumbsWidget.cshtml", model);
        }

        public IEnumerable<BreadcrumbItem> GetHierarchy(TreeNode current, bool addSiteLink, bool showContainers)
        {
            var pageUrlRetriever = Service.Resolve<IPageUrlRetriever>();

            // Add current page
            var ret = new List<BreadcrumbItem>();
            ret.Add(new BreadcrumbItem()
            {
                Name = current.DocumentName,
                Url = null,
                IsCurrentPage = true
            });

            // Add current page's parents in loop
            var parent = current.Parent;
            while (parent != null)
            {
                if (parent.IsRoot()) break;

                var type = DataClassInfoProvider.GetDataClassInfo(parent.ClassName);
                if (type != null)
                {
                    if (type.ClassIsCoupledClass ||
                        !type.ClassIsCoupledClass && showContainers)
                    {
                        var url = pageUrlRetriever.Retrieve(parent).AbsoluteUrl;
                        ret.Add(new BreadcrumbItem()
                        {
                            Name = parent.DocumentName,
                            Url = url
                        });
                    }

                    parent = parent.Parent;
                }
            }

            // Add link to main domain if needed
            if (addSiteLink)
            {
                ret.Add(new BreadcrumbItem()
                {
                    Name = current.Site.DisplayName,
                    Url = current.Site.SitePresentationURL
                });
            }

            ret.Reverse();
            return ret;
        }
    }
}
