#pragma checksum "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e396150620b25e66428ea0f3f5d0e07d9d05a10c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Players_Details), @"mvc.1.0.view", @"/Views/Players/Details.cshtml")]
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
#line 1 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\_ViewImports.cshtml"
using Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\_ViewImports.cshtml"
using Client.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e396150620b25e66428ea0f3f5d0e07d9d05a10c", @"/Views/Players/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3fd22fe3589ebdc381df8dd96b3beba49422a762", @"/Views/_ViewImports.cshtml")]
    public class Views_Players_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Client.Models.Player>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Detalhes do Jogador</h1>\r\n\r\n<div>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <div class=\"col-md-4\">\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 15 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 18 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 21 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Nickname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 24 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayFor(model => model.Nickname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 27 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Age));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 30 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayFor(model => model.Age));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 33 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Nationality));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 36 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayFor(model => model.Nationality));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n        </div>\r\n        <div class=\"col-md-2\"></div>\r\n        <div class=\"col-md-6\">\r\n            <h3>Redes Sociais</h3>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 43 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Facebook));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 46 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayFor(model => model.Facebook));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 49 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Twitter));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 52 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayFor(model => model.Twitter));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 55 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Instagram));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 58 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
           Write(Html.DisplayFor(model => model.Instagram));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n        </div>\r\n        \r\n        \r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e396150620b25e66428ea0f3f5d0e07d9d05a10c11470", async() => {
                WriteLiteral("Editar");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 66 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Players\Details.cshtml"
                           WriteLiteral(Model.PlayerId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e396150620b25e66428ea0f3f5d0e07d9d05a10c13801", async() => {
                WriteLiteral("Voltar para a listagem");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Client.Models.Player> Html { get; private set; }
    }
}
#pragma warning restore 1591
