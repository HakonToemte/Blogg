#pragma checksum "C:\Users\hakon\OneDrive\Documents\GitHub\Blogg\Pages\View.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "67ffef3d22b886c18a1f9deea2c37217967aab56"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Blogg.Pages.Pages_View), @"mvc.1.0.razor-page", @"/Pages/View.cshtml")]
namespace Blogg.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\hakon\OneDrive\Documents\GitHub\Blogg\Pages\_ViewImports.cshtml"
using Blogg;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67ffef3d22b886c18a1f9deea2c37217967aab56", @"/Pages/View.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cfb241ed2b7e74ba2675f41281f4c81c19b2dd37", @"/Pages/_ViewImports.cshtml")]
    public class Pages_View : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\hakon\OneDrive\Documents\GitHub\Blogg\Pages\View.cshtml"
  
    ViewData["Title"] = "View";
    if (Model.LoggedUser != null)
    {
        Layout = "_LoggedLayout";
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\hakon\OneDrive\Documents\GitHub\Blogg\Pages\View.cshtml"
  string author = (Model.Author.UserName + "s");

#line default
#line hidden
#nullable disable
            WriteLiteral(" <center>\r\n        <h1 class=\"display-4\" style=center>");
#nullable restore
#line 12 "C:\Users\hakon\OneDrive\Documents\GitHub\Blogg\Pages\View.cshtml"
                                      Write(author);

#line default
#line hidden
#nullable disable
            WriteLiteral(" blog</h1>\r\n</center>\r\n");
#nullable restore
#line 15 "C:\Users\hakon\OneDrive\Documents\GitHub\Blogg\Pages\View.cshtml"
 foreach (Post post in Model.Posts.Reverse()){

#line default
#line hidden
#nullable disable
            WriteLiteral("    <center>\r\n        <h1 class=\"display-4\" style=center>");
#nullable restore
#line 17 "C:\Users\hakon\OneDrive\Documents\GitHub\Blogg\Pages\View.cshtml"
                                      Write(post.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n    </center>\r\n    <br>\r\n    <p class=\"bread\"> ");
#nullable restore
#line 20 "C:\Users\hakon\OneDrive\Documents\GitHub\Blogg\Pages\View.cshtml"
                 Write(post.Text);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    <br>\r\n    <br>\r\n");
#nullable restore
#line 23 "C:\Users\hakon\OneDrive\Documents\GitHub\Blogg\Pages\View.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ViewModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ViewModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ViewModel>)PageContext?.ViewData;
        public ViewModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
