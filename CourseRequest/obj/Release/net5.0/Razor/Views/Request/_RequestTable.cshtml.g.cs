#pragma checksum "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Request\_RequestTable.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "66a796a19fc24a3d8e6b943212994461f06dd3c2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Request__RequestTable), @"mvc.1.0.view", @"/Views/Request/_RequestTable.cshtml")]
namespace AspNetCore
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
#line 1 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\_ViewImports.cshtml"
using CourseRequest;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\_ViewImports.cshtml"
using CourseRequest.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"66a796a19fc24a3d8e6b943212994461f06dd3c2", @"/Views/Request/_RequestTable.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0d203e20c38baf8280b7ede91fd1a18b2d8eafff", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Request__RequestTable : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<RequestOut>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Request\_RequestTable.cshtml"
 foreach (var request in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\r\n        <td class=\"text-center\">");
#nullable restore
#line 6 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Request\_RequestTable.cshtml"
                           Write(request.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td class=\"text-center\">");
#nullable restore
#line 7 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Request\_RequestTable.cshtml"
                           Write(request.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td class=\"text-center\">");
#nullable restore
#line 8 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Request\_RequestTable.cshtml"
                           Write(request.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td class=\"text-center\">");
#nullable restore
#line 9 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Request\_RequestTable.cshtml"
                           Write(request.CourseName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td class=\"text-center\">");
#nullable restore
#line 10 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Request\_RequestTable.cshtml"
                           Write(request.CourseType);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td class=\"text-center\">");
#nullable restore
#line 11 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Request\_RequestTable.cshtml"
                           Write(request.CourseStart.ToString("dd.MM.yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 11 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Request\_RequestTable.cshtml"
                                                                         Write(request.CourseEnd.ToString("dd.MM.yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td class=\"text-center\"><a");
            BeginWriteAttribute("href", " href=\"", 509, "\"", 543, 2);
            WriteAttributeValue("", 516, "/Detail/Details/", 516, 16, true);
#nullable restore
#line 12 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Request\_RequestTable.cshtml"
WriteAttributeValue("", 532, request.Id, 532, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Содержание</a></td>\r\n    </tr>\r\n");
#nullable restore
#line 14 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Request\_RequestTable.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<RequestOut>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
