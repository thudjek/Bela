#pragma checksum "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bbd956e84af6dbcd797d7866999f94f93e03b5ff"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_RoomUsersLayout_Default), @"mvc.1.0.view", @"/Views/Shared/Components/RoomUsersLayout/Default.cshtml")]
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
#line 1 "E:\Projects\Bela\Bela.WebMVC\Views\_ViewImports.cshtml"
using Bela.WebMVC;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bbd956e84af6dbcd797d7866999f94f93e03b5ff", @"/Views/Shared/Components/RoomUsersLayout/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c2467f3dd3392a8866c559807ca9264deb77d4f4", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_RoomUsersLayout_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Bela.WebMVC.ComponentModels.RoomUsersLayoutVCModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n    <div class=\"row\">\r\n        <div id=\"player1\" class=\"col-6 justify-content-center align-self-center text-center\">\r\n");
#nullable restore
#line 5 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
              
                var user = Model.Users.Where(u => u.RoomOrderNumber.Value == 1).FirstOrDefault();
                if (user != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div");
            BeginWriteAttribute("class", " class=\"", 376, "\"", 429, 2);
            WriteAttributeValue("", 384, "card", 384, 4, true);
#nullable restore
#line 9 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
WriteAttributeValue(" ", 388, user.IsReady ? "bg-green" : "bg-gray", 389, 40, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <div class=\"card-body text-center\">\r\n                                <h5 class=\"card-title\">");
#nullable restore
#line 11 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                  Write(user.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                                <p class=\"card-text\">Pobjede: ");
#nullable restore
#line 12 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                         Write(user.Wins);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                <p class=\"card-text\">Porazi: ");
#nullable restore
#line 13 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                        Write(user.Losses);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                <p class=\"card-text\">Odustajanja: ");
#nullable restore
#line 14 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                             Write(user.Dropouts);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 17 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                }
             

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n        <div id=\"player2\" class=\"col-6 justify-content-center align-self-center text-center\">\r\n");
#nullable restore
#line 21 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
              
                user = Model.Users.Where(u => u.RoomOrderNumber.Value == 2).FirstOrDefault();
                if (user != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div");
            BeginWriteAttribute("class", " class=\"", 1220, "\"", 1273, 2);
            WriteAttributeValue("", 1228, "card", 1228, 4, true);
#nullable restore
#line 25 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
WriteAttributeValue(" ", 1232, user.IsReady ? "bg-green" : "bg-gray", 1233, 40, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <div class=\"card-body text-center\">\r\n                            <h5 class=\"card-title\">");
#nullable restore
#line 27 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                              Write(user.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                            <p class=\"card-text\">Pobjede: ");
#nullable restore
#line 28 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                     Write(user.Wins);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"card-text\">Porazi: ");
#nullable restore
#line 29 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                    Write(user.Losses);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"card-text\">Odustajanja: ");
#nullable restore
#line 30 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                         Write(user.Dropouts);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n");
#nullable restore
#line 32 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                         if (Model.IsOwner)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"m-2 m-sm-2 m-lg-0\">\r\n");
#nullable restore
#line 35 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                 if (!user.IsReady) 
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <button onclick=\"swapPlayers(this)\"");
            BeginWriteAttribute("value", " value=\"", 1966, "\"", 1982, 1);
#nullable restore
#line 37 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
WriteAttributeValue("", 1974, user.Id, 1974, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"position-absolute btn btn-secondary btn-swap\">Zamjeni</button>\r\n");
#nullable restore
#line 38 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <button");
            BeginWriteAttribute("onclick", " onclick=\"", 2129, "\"", 2161, 3);
            WriteAttributeValue("", 2139, "kickFromRoom(", 2139, 13, true);
#nullable restore
#line 39 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
WriteAttributeValue("", 2152, user.Id, 2152, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2160, ")", 2160, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"position-absolute btn btn-secondary btn-kick\">Izbaci</button>\r\n                            </div>\r\n");
#nullable restore
#line 41 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n");
#nullable restore
#line 43 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"spinner-border mt-lg-5\" role=\"status\">\r\n                        <span class=\"sr-only\">Loading...</span>\r\n                    </div>\r\n");
#nullable restore
#line 49 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n    <hr />\r\n    <h3 class=\"text-center\">PROTIV</h3>\r\n    <hr />\r\n    <div class=\"row\">\r\n        <div id=\"player3\" class=\"col-6 justify-content-center align-self-center text-center\">\r\n");
#nullable restore
#line 58 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
              
                user = Model.Users.Where(u => u.RoomOrderNumber.Value == 3).FirstOrDefault();
                if (user != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div");
            BeginWriteAttribute("class", " class=\"", 2983, "\"", 3036, 2);
            WriteAttributeValue("", 2991, "card", 2991, 4, true);
#nullable restore
#line 62 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
WriteAttributeValue(" ", 2995, user.IsReady ? "bg-green" : "bg-gray", 2996, 40, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <div class=\"card-body text-center\">\r\n                            <h5 class=\"card-title\">");
#nullable restore
#line 64 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                              Write(user.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                            <p class=\"card-text\">Pobjede: ");
#nullable restore
#line 65 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                     Write(user.Wins);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"card-text\">Porazi: ");
#nullable restore
#line 66 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                    Write(user.Losses);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"card-text\">Odustajanja: ");
#nullable restore
#line 67 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                         Write(user.Dropouts);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n");
#nullable restore
#line 69 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                         if (Model.IsOwner)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"m-2 m-sm-2 m-lg-0\">\r\n");
#nullable restore
#line 72 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                 if (!user.IsReady) 
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <button onclick=\"swapPlayers(this)\"");
            BeginWriteAttribute("value", " value=\"", 3729, "\"", 3745, 1);
#nullable restore
#line 74 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
WriteAttributeValue("", 3737, user.Id, 3737, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"position-absolute btn btn-secondary btn-swap\">Zamjeni</button>\r\n");
#nullable restore
#line 75 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <button");
            BeginWriteAttribute("onclick", " onclick=\"", 3888, "\"", 3920, 3);
            WriteAttributeValue("", 3898, "kickFromRoom(", 3898, 13, true);
#nullable restore
#line 76 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
WriteAttributeValue("", 3911, user.Id, 3911, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3919, ")", 3919, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"position-absolute btn btn-secondary btn-kick\">Izbaci</button>\r\n                            </div>\r\n");
#nullable restore
#line 78 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n");
#nullable restore
#line 80 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"spinner-border mt-lg-5\" role=\"status\">\r\n                        <span class=\"sr-only\">Loading...</span>\r\n                    </div>\r\n");
#nullable restore
#line 86 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n        <div id=\"player4\" class=\"col-6 justify-content-center align-self-center text-center\">\r\n");
#nullable restore
#line 90 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
              
                user = Model.Users.Where(u => u.RoomOrderNumber.Value == 4).FirstOrDefault();
                if (user != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div");
            BeginWriteAttribute("class", " class=\"", 4642, "\"", 4695, 2);
            WriteAttributeValue("", 4650, "card", 4650, 4, true);
#nullable restore
#line 94 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
WriteAttributeValue(" ", 4654, user.IsReady ? "bg-green" : "bg-gray", 4655, 40, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <div class=\"card-body text-center\">\r\n                            <h5 class=\"card-title\">");
#nullable restore
#line 96 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                              Write(user.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                            <p class=\"card-text\">Pobjede: ");
#nullable restore
#line 97 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                     Write(user.Wins);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"card-text\">Porazi: ");
#nullable restore
#line 98 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                    Write(user.Losses);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"card-text\">Odustajanja: ");
#nullable restore
#line 99 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                                         Write(user.Dropouts);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n");
#nullable restore
#line 101 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                         if (Model.IsOwner)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"m-2 m-sm-2 m-lg-0\">\r\n");
#nullable restore
#line 104 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                 if (!user.IsReady) 
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <button onclick=\"swapPlayers(this)\"");
            BeginWriteAttribute("value", " value=\"", 5388, "\"", 5404, 1);
#nullable restore
#line 106 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
WriteAttributeValue("", 5396, user.Id, 5396, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"position-absolute btn btn-secondary btn-swap\">Zamjeni</button>\r\n");
#nullable restore
#line 107 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <button");
            BeginWriteAttribute("onclick", " onclick=\"", 5551, "\"", 5583, 3);
            WriteAttributeValue("", 5561, "kickFromRoom(", 5561, 13, true);
#nullable restore
#line 108 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
WriteAttributeValue("", 5574, user.Id, 5574, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 5582, ")", 5582, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"position-absolute btn btn-secondary btn-kick\">Izbaci</button>\r\n                            </div>\r\n");
#nullable restore
#line 110 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n");
#nullable restore
#line 112 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"spinner-border mt-lg-5\" role=\"status\">\r\n                        <span class=\"sr-only\">Loading...</span>\r\n                    </div>\r\n");
#nullable restore
#line 118 "E:\Projects\Bela\Bela.WebMVC\Views\Shared\Components\RoomUsersLayout\Default.cshtml"
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Bela.WebMVC.ComponentModels.RoomUsersLayoutVCModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
