#pragma checksum "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "381f9032fc79b7872dad8162068b9efd8d6280e0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Room_Index), @"mvc.1.0.view", @"/Views/Room/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"381f9032fc79b7872dad8162068b9efd8d6280e0", @"/Views/Room/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c2467f3dd3392a8866c559807ca9264deb77d4f4", @"/Views/_ViewImports.cshtml")]
    public class Views_Room_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Bela.Application.ViewModels.Room.RoomViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/sweetalert2/dist/sweetalert2.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/roomHub.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/sweetalert2/dist/sweetalert2.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/swal-helper.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("btnLeaveRoom"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Room", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DropRoom", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-secondary mr-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "LeaveRoom", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
  
    ViewData["Title"] = "Belot.hr";

#line default
#line hidden
#nullable disable
            DefineSection("Styles", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "381f9032fc79b7872dad8162068b9efd8d6280e06698", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "381f9032fc79b7872dad8162068b9efd8d6280e07997", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "381f9032fc79b7872dad8162068b9efd8d6280e09096", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "381f9032fc79b7872dad8162068b9efd8d6280e010195", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    <script>\r\n        var onInputTimeout = null;\r\n        var isOwner;\r\n        var swapPlayersArray = [];\r\n        $(function () {\r\n            isOwner = \'");
#nullable restore
#line 19 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                  Write(Model.IsOwner);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"' == 'True';

            renderUserList($("".filter-username-input"").val());
            renderUsersLayout();

            $("".filter-username-input"").on('input', function () {
                if (onInputTimeout !== null) {
                    clearTimeout(onInputTimeout);
                }

                var id = this.id;
                var value = this.value;

                if (id == ""filterUsernameInputLg"") {
                    $(""#filterUsernameInputSm"").val(value);
                }

                if (id == ""filterUsernameInputSm"") {
                    $(""#filterUsernameInputLg"").val(value);
                }

                onInputTimeout = setTimeout(function () {
                    renderUserList(value);
                }, 200);
            });
        });

        function renderUserList(filterUsername) {
            $("".user-list"").load(""");
#nullable restore
#line 47 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                             Write(Url.Action("GetUserListViewComponent", "Lobby"));

#line default
#line hidden
#nullable disable
                WriteLiteral("?filterUsername=\" + filterUsername + \"&isRoomAndOwner=\" + isOwner);\r\n        }\r\n\r\n        function renderUsersLayout() {\r\n            var roomId = ");
#nullable restore
#line 51 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                    Write(Model.Id);

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n            var isOwner = \'");
#nullable restore
#line 52 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                      Write(Model.IsOwner);

#line default
#line hidden
#nullable disable
                WriteLiteral("\' == \'True\';\r\n\r\n            $(\"#usersLayout\").load(\"");
#nullable restore
#line 54 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                               Write(Url.Action("GetRoomUsersLayoutViewComponent", "Room"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"?roomId="" + roomId + ""&isOwner="" + isOwner);
        }

        function inviteUser(userId, clickedElement) {
            var temp = clickedElement.innerHTML;
            clickedElement.innerHTML = ""<div class='spinner-border spinner-border-sm' role='status'><span class='sr-only'>Loading...</span></div>"";
            clickedElement.classList.add(""inactiveLink"");
            setTimeout(function () {
                clickedElement.innerHTML = temp;
                clickedElement.classList.remove(""inactiveLink"");
            }, 500);

            var ownerUsername = '");
#nullable restore
#line 66 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                            Write(Model.OwnerUsername);

#line default
#line hidden
#nullable disable
                WriteLiteral("\';\r\n            var roomId = ");
#nullable restore
#line 67 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                    Write(Model.Id);

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n\r\n            mainHubConnection.invoke(\"SendInviteToUser\", userId, ownerUsername, roomId);\r\n        }\r\n\r\n        function joinRoomGroup() {\r\n            var roomId = ");
#nullable restore
#line 73 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                    Write(Model.Id);

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n            roomHubConnection.invoke(\"JoinRoomGroup\", roomId);\r\n        }\r\n\r\n        function kickFromRoom(userId) {\r\n            var roomId = ");
#nullable restore
#line 78 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                    Write(Model.Id);

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n            $.ajax({\r\n                url: \"");
#nullable restore
#line 80 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                 Write(Url.Action("KickFromRoom", "Room"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                dataType: ""json"",
                data: { userId: userId, roomId: roomId },
                cache: false,
                success: function (data) {
                    if (!data.success) {
                        swalHelper.alertError(""Dogodila se greška"");
                    }
                },
                error: function () {
                    swalHelper.alertError(""Dogodila se greška"");
                }
            });
        }

        function toggleReady() {
            var roomId = ");
#nullable restore
#line 96 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                    Write(Model.Id);

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n            $.ajax({\r\n                url: \"");
#nullable restore
#line 98 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                 Write(Url.Action("ToggleReady", "Room"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                dataType: ""json"",
                data: { roomId: roomId },
                cache: false,
                success: function (data) {
                    if (!data.success) {
                        swalHelper.alertError(""Dogodila se greška"");
                    }
                },
                error: function () {
                    swalHelper.alertError(""Dogodila se greška"");
                }
            });
        }

        function swapPlayers(btn) {

            console.log(swapPlayersArray)
            var userId = btn.value;
            var parent = btn.parentElement.parentElement;
            if (!swapPlayersArray.includes(userId)) {
                swapPlayersArray.push(userId);
                $(parent).css(""border"", ""2px solid"");
            }
            else {
                swapPlayersArray.pop();
                $(parent).css(""border"", """");
            }

            if (swapPlayersArray.length == 2) {

                var roomId = ");
#nullable restore
#line 129 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                        Write(Model.Id);

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n                $.ajax({\r\n                    url: \"");
#nullable restore
#line 131 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                     Write(Url.Action("SwapPlayers", "Room"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                    dataType: ""json"",
                    cache: false,
                    data: { firstUserId: swapPlayersArray[0], secondUserId: swapPlayersArray[1], roomId: roomId },
                    success: function (data) {
                        if (data.success) {
                            swapPlayersArray = [];
                        }
                        else {
                            swalHelper.alertError(""Dogodila se greška"");
                        }
                    },
                    error: function () {
                        swalHelper.alertError(""Dogodila se greška"");
                    }
                });
            }
        }

        function tryStartGame() {
            var roomId = ");
#nullable restore
#line 151 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                    Write(Model.Id);

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n            $.ajax({\r\n                url: \"");
#nullable restore
#line 153 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                 Write(Url.Action("TryStartGame", "Room"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                dataType: ""json"",
                cache: false,
                data: { roomId: roomId },
                success: function (data) {
                    if (data.success) {
                        alert(""game start"");
                    }
                    else {
                        swalHelper.alertError(data.error);
                    }
                },
                error: function () {
                    swalHelper.alertError(""Dogodila se greška"");
                }
            });
        }

    </script>
");
            }
            );
            WriteLiteral("<h4>Soba: &nbsp;");
#nullable restore
#line 173 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
           Write(Model.RoomName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n\r\n<div class=\"row mt-3 mt-sm-3 mt-md-4 mt-lg-4 mt-xl-4\">\r\n    <div class=\"col-12\">\r\n");
#nullable restore
#line 177 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
         if (Model.IsOwner)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "381f9032fc79b7872dad8162068b9efd8d6280e020884", async() => {
                WriteLiteral("Raspusti sobu");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-roomId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 179 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                                                                                   WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["roomId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-roomId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["roomId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <a id=\"btnStartGame\" onclick=\"tryStartGame()\" class=\"btn btn-primary mr-3\">Pokreni igru</a>\r\n");
#nullable restore
#line 181 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
            if (Model.IsPrivate)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p class=\"mt-2\">Lozinka sobe: ");
#nullable restore
#line 183 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                                         Write(Model.RoomPassword);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 184 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
            }
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "381f9032fc79b7872dad8162068b9efd8d6280e024296", async() => {
                WriteLiteral("Izađi iz sobe");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-roomId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 188 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                                                                                    WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["roomId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-roomId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["roomId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <a id=\"btnReady\" onclick=\"toggleReady()\" class=\"btn btn-primary mr-3\">Spreman</a>\r\n");
#nullable restore
#line 190 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </div>
</div>
<hr />
<div class=""row text-center"">
    <div class=""col-12 col-sm-12 col-md-12 col-lg-8"">
    </div>
    <div class=""col-lg-4 d-none d-lg-block"">
        <h5>Online igrači</h5>
    </div>
</div>
<div class=""row"">
    <div id=""usersLayout"" class=""col-12 col-sm-12 col-md-12 col-lg-8"">
        
    </div>
    <div class=""col-lg-4 d-none d-lg-block user-list-wrapper"">
        <input id=""filterUsernameInputLg"" type=""text"" class=""form-control filter-username-input"" placeholder=""Filtriraj po imenu..."" />
        <br />
        <ul class=""list-group overflow-auto user-list"" style=""height: 500px"">
        </ul>
    </div>
</div>

<hr class=""d-lg-none"" />
<div class=""row text-center d-block d-lg-none"">
    <div class=""col-12 w-75 mr-auto ml-auto"">
        <h5>Online igrači</h5>
    </div>
</div>
<div class=""row d-block d-lg-none"">
    <div class=""col-12 w-75 mr-auto ml-auto"">
        <input id=""filterUsernameInputSm"" type=""text"" class=""form-control filter-username-input""");
            WriteLiteral(" placeholder=\"Filtriraj po imenu...\" />\r\n        <br />\r\n        <ul class=\"list-group overflow-auto user-list\" style=\"height: 250px\">\r\n        </ul>\r\n    </div>\r\n</div>\r\n\r\n<div id=\"userDetailsModal\" class=\"modal fade\" data-url=\"");
#nullable restore
#line 228 "E:\Projects\Bela\Bela.WebMVC\Views\Room\Index.cshtml"
                                                   Write(Url.Action("UserDetails", "Account"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Bela.Application.ViewModels.Room.RoomViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
