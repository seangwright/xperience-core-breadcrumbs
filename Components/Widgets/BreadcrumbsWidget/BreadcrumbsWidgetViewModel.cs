using Lucene.Net.Documents;
using System.Collections.Generic;

namespace Xperience.Components.Widgets.BreadcrumbsWidget
{
    public class BreadcrumbsWidgetViewModel
    {
        public IEnumerable<BreadcrumbItem> Hierarchy { get; set; }
        public string Separator { get; set; }
        public string ClassName { get; set; }
    }
}
