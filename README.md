# Xperience 13 .NET Core Breadcrumb Widget

This is a widget for Xperience .NET Core websites which use [Content Tree-Based routing](https://docs.xperience.io/developing-websites/implementing-routing/content-tree-based-routing).

![screenshot](/Components/Widgets/BreadcrumbsWidget/screenshot.png)

## Installation

1. Click the **Code** button to clone the repository locally or download the .ZIP package.
2. Copy the contents of the repository into your .NET Core website project, overwriting when necessary. The `/Components` folder should be placed on the root of the project files.
3. Build and [deploy](https://docs.xperience.io/developing-websites/developing-xperience-applications-using-asp-net-core/deploying-and-hosting-asp-net-core-applications) the website.

## Adding the widget to a page

The widget can be added to any page which uses the [page builder](https://docs.xperience.io/developing-websites/page-builder-development/creating-pages-with-editable-areas). It has 4 properties:

- **Show domain link first**: Displays the site name and a link to the root of the site as the first breadcrumb item
- **Show container page types**: If checked, pages that use container page types (e.g. a Folder) will appear in the breadcrumbs
- **Separator**: The text to add between each breadcrumb item
- **Container class**: The CSS class(es) to add the `div` that surrounds the breadcrumbs

## Adding breadcrumbs to views

You can also add breadcrumbs directly to any view, such as the main **_Layout.cshtml**. Add the following to the view:

```
@using Xperience.Components.Widgets.BreadcrumbsWidget
@Html.GetBreadcrumbContent()
```
The breadcrumbs will be initialized with default properties. To specify your own properties, pass an instance of `BreadcrumbsWidgetProperties`:

```
@Html.GetBreadcrumbContent(new BreadcrumbsWidgetProperties() {
    ShowContainers = true,
    ShowSiteLink = true,
    ClassName = "breadcrumb-container",
    Separator = ">"
})
```

## Compatibility

This code is only available for use on Kentico Xperience 13 websites using the [.NET Core development model](https://docs.xperience.io/developing-websites/developing-xperience-applications-using-asp-net-core). The website must be using the [content tree-based routing](https://docs.xperience.io/developing-websites/implementing-routing/content-tree-based-routing) model for the breadcrumbs to display properly.

## Feedback & Contributing

Check out the [contributing](https://github.com/kentico-ericd/xperience-core-breadcrumbs/blob/master/CONTRIBUTING.md) page to see the best places to file issues, start discussions, and begin contributing.

## License

The repository is available as open source under the terms of the [MIT License](https://opensource.org/licenses/MIT).
