#pragma checksum "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "43b5ab31d1af6fc474e80db0fc98bc2a975f63ed"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_RequestList), @"mvc.1.0.view", @"/Views/Home/RequestList.cshtml")]
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
#nullable restore
#line 5 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
using System.Security.Principal;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"43b5ab31d1af6fc474e80db0fc98bc2a975f63ed", @"/Views/Home/RequestList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0d203e20c38baf8280b7ede91fd1a18b2d8eafff", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_RequestList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Request>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
  
    ViewData["Title"] = "Перечень заявок";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
  
    WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
    string userName = currentIdentity?.Name ?? "Неизвестно";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n\r\n<div class=\"container-fluid\">\r\n    <div class=\"row mt-3\">\r\n        <div class=\"col\">\r\n            <div>\r\n                <b>Новая заявка</b>\r\n            </div>\r\n            <div>\r\n                <span class=\"user\">Пользователь: ");
#nullable restore
#line 22 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                                            Write(userName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n            </div>\r\n        </div>\r\n        <div class=\"col-auto text-center\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43b5ab31d1af6fc474e80db0fc98bc2a975f63ed5787", async() => {
                WriteLiteral("Составить новую заявку");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </div>
    </div>

    

    <div class=""row mb-4 mt-2"">
        <div class=""col text-center"">
            <table class=""table"">
                <thead>
                    <tr>
                        <th>Full Name</th>
                        <th>Department</th>
                        <th>Position</th>
                        <th>Course Name</th>
                        <th>Course Type</th>
                        <th>Notation</th>
                        <th>Status</th>
                        <th>Course Beginning</th>
                        <th>Course End</th>
                        <th>Year</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 50 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                     foreach (var request in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 53 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                           Write(request.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 54 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                           Write(request.Department);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 55 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                           Write(request.Position);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 56 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                           Write(request.CourseName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 57 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                           Write(request.CourseType);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 58 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                           Write(request.Notation);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 59 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                           Write(request.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 60 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                           Write(request.CourseBeginning.ToString("dd.MM.yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 61 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                           Write(request.CourseEnd.ToString("dd.MM.yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 62 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                           Write(request.Year);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n");
#nullable restore
#line 64 "C:\Users\lokot\Desktop\CourseRequest\CourseRequest\Views\Home\RequestList.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Request>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
