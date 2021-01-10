#pragma checksum "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a384f84acd9dc53ddb0032bcddaed36e3ca141b0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Stats), @"mvc.1.0.view", @"/Views/Home/Stats.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a384f84acd9dc53ddb0032bcddaed36e3ca141b0", @"/Views/Home/Stats.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3fd22fe3589ebdc381df8dd96b3beba49422a762", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Stats : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
  

    Game game = @ViewBag.Game;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    div, input, td div {
        text-align: center;
    }

    h5 {
        font-size: 2vh;
        margin-left: 2vh;
    }

    hr {
        margin: 1vh;
    }
</style>

<div class=""row"">
    <div class=""col-md-12"">
        <table>
            <thead>
                <tr>
                    <th>
                        <h4>Jogador</h4>
                    </th>
                    <th>
                        <h4>Kills</h4>
                    </th>
                    <th>
                        <h4>Deaths</h4>
                    </th>
                    <th>
                        <h4>ADR</h4>
                    </th>
                    <th>
                        <h4>Rating</h4>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td colspan=""5"">
                        <hr />
                        <h5>Team A - ");
#nullable restore
#line 50 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
                                Write(game.TeamA.TeamName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                        <hr />\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 54 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
                 foreach (StatPlayerOnGame item in ViewBag.Stats)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
                     if (item.TeamId == game.TeamAId)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr class=\"stat\">\r\n                            <td>\r\n                                <div class=\"form-group\">\r\n                                    <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 1602, "\"", 1631, 1);
#nullable restore
#line 61 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
WriteAttributeValue("", 1610, item.Player.Nickname, 1610, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" disabled />\r\n                                </div>\r\n                            </td>\r\n                            <td>\r\n                                <div class=\"form-group\">\r\n                                    <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 1876, "\"", 1895, 1);
#nullable restore
#line 66 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
WriteAttributeValue("", 1884, item.Kills, 1884, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                </div>\r\n                            </td>\r\n                            <td>\r\n                                <div class=\"form-group\">\r\n                                    <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 2131, "\"", 2151, 1);
#nullable restore
#line 71 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
WriteAttributeValue("", 2139, item.Deaths, 2139, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                </div>\r\n                            </td>\r\n                            <td>\r\n                                <div class=\"form-group\">\r\n                                    <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 2387, "\"", 2404, 1);
#nullable restore
#line 76 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
WriteAttributeValue("", 2395, item.Adr, 2395, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                </div>\r\n                            </td>\r\n                            <td>\r\n                                <div class=\"form-group\">\r\n                                    <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 2640, "\"", 2660, 1);
#nullable restore
#line 81 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
WriteAttributeValue("", 2648, item.Rating, 2648, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                </div>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 85 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 85 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td colspan=\"5\">\r\n                        <hr />\r\n                        <h5>Team B - ");
#nullable restore
#line 90 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
                                Write(game.TeamB.TeamName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                        <hr />\r\n                    </td>\r\n                </tr>\r\n\r\n");
#nullable restore
#line 95 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
                 foreach (StatPlayerOnGame item in ViewBag.Stats)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 97 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
                     if (item.TeamId == game.TeamBId)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr class=\"stat\">\r\n                <td>\r\n                    <div class=\"form-group\">\r\n                        <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 3368, "\"", 3397, 1);
#nullable restore
#line 102 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
WriteAttributeValue("", 3376, item.Player.Nickname, 3376, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" disabled />\r\n                    </div>\r\n                </td>\r\n                <td>\r\n                    <div class=\"form-group\">\r\n                        <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 3582, "\"", 3601, 1);
#nullable restore
#line 107 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
WriteAttributeValue("", 3590, item.Kills, 3590, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                    </div>\r\n                </td>\r\n                <td>\r\n                    <div class=\"form-group\">\r\n                        <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 3777, "\"", 3797, 1);
#nullable restore
#line 112 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
WriteAttributeValue("", 3785, item.Deaths, 3785, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                    </div>\r\n                </td>\r\n                <td>\r\n                    <div class=\"form-group\">\r\n                        <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 3973, "\"", 3990, 1);
#nullable restore
#line 117 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
WriteAttributeValue("", 3981, item.Adr, 3981, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                    </div>\r\n                </td>\r\n                <td>\r\n                    <div class=\"form-group\">\r\n                        <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 4166, "\"", 4186, 1);
#nullable restore
#line 122 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
WriteAttributeValue("", 4174, item.Rating, 4174, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                    </div>\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 126 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 126 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr style=\"padding:5vh;\">\r\n                    <td colspan=\"2\">\r\n                        <a style=\"float:left; margin-left:2vh !important;\"");
            BeginWriteAttribute("href", " href=\"", 4459, "\"", 4493, 2);
            WriteAttributeValue("", 4466, "/Public/Games/", 4466, 14, true);
#nullable restore
#line 130 "D:\OneDrive\OneDrive - Instituto Politécnico do Cávado e do Ave\IPCA\3_Ano\3.1 - Integração de Sistemas de Informação\Trabalhos\2_Trabalho\src\Client\Views\Home\Stats.cshtml"
WriteAttributeValue("", 4480, game.EventId, 4480, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\">Voltar para a listagem de jogos</a>\r\n                    </td>\r\n                </tr>\r\n            </tbody>\r\n        </table>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
