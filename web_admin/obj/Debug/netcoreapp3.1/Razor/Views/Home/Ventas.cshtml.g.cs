#pragma checksum "D:\Escritorio\Nueva carpetaho\HotelLuzyLuna\web_admin\Views\Home\Ventas.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fa98712d996d84bf0d86cf49ffa3e3673f81beaa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Ventas), @"mvc.1.0.view", @"/Views/Home/Ventas.cshtml")]
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
#line 1 "D:\Escritorio\Nueva carpetaho\HotelLuzyLuna\web_admin\Views\_ViewImports.cshtml"
using web_admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Escritorio\Nueva carpetaho\HotelLuzyLuna\web_admin\Views\_ViewImports.cshtml"
using web_admin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa98712d996d84bf0d86cf49ffa3e3673f81beaa", @"/Views/Home/Ventas.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8f41aeeca6c0e2e834f45995d46dc994aba869be", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Ventas : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@" <main class=""main col"">
                <div class=""row"">
                    <div class=""columna col-lg-6"">
                        <div class=""widget nueva_entrada"">
                            <h3 class=""titulo"">Centro de Ventas - Luz y Luna</h3>

                        </div>

                        <div class=""columna col-lg-7"">
                            <button class=""btn btn-primary"" data-toggle=""modal"" data-target=""#fm-modal"">Consultar Catalogo</button>
                            <div class=""modal fade"" id=""fm-modal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""fm-modal"" aria-hidden=""true"">
                                <div class=""modal-dialog"">
                                    <div class=""modal-content"">
                                        <div class=""modal-header"">
                                            <h5 class=""modal-title""");
            BeginWriteAttribute("id", " id=\"", 883, "\"", 888, 0);
            EndWriteAttribute();
            WriteLiteral(@">Catalogo de Productos</h5>
                                            <button class=""close"" data-dismiss=""modal"" aria-label=""Cerrar"">
										<span aria-hidden=""true"">&times;</span>
									</button>
                                        </div>

                                        <div class=""modal-body"">

                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fa98712d996d84bf0d86cf49ffa3e3673f81beaa4989", async() => {
                WriteLiteral(@"
                                                <table class=""table table-sm table-hover table-bordered table-responsive-lg table-active"">
                                                    <div class=""row mb-3"">
                                                        <div class=""col-7"">
                                                            <input type=""text"" class=""form-control"" placeholder=""Escriba el Producto"" name=""cantidad"" id=""cantidad"">

                                                        </div>
                                                        <div class=""col-5"">
                                                            <button class=""btn btn-primary"" data-toggle=""modal"" data-target=""#fm-modal"">Buscar Producto</button>

                                                        </div>

                                                    </div>


                                                    <thead>

                                                        <tr>
 ");
                WriteLiteral(@"                                                           <th>Tipo</th>
                                                            <th>Nombre</th>
                                                            <th>Precio Venta</th>
                                                            <th>Cantidad</th>
                                                        </tr>
                                                    </thead>

                                                    <tr");
                BeginWriteAttribute("class", " class=\"", 2795, "\"", 2803, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                                        <td>Bebida</td>
                                                        <td>Coca Cola</td>
                                                        <td>2.50</td>
                                                        <td> <input type=""text"" class=""form-control"" placeholder=""cantidad"" name=""cantidad"" id=""cantidad"">
                                                        </td>
                                                    </tr>


                                                    <tr");
                BeginWriteAttribute("class", " class=\"", 3364, "\"", 3372, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                                        <td>Pickeos</td>
                                                        <td>Pack Mediano</td>
                                                        <td>7.20</td>
                                                        <td> <input type=""text"" class=""form-control"" placeholder=""cantidad"" name=""cantidad"" id=""cantidad"">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Vino</td>
                                                        <td>Amore Ica</td>
                                                        <td>26.00</td>
                                                        <td> <input type=""text"" class=""form-control"" placeholder=""cantidad"" name=""cantidad"" id=""cantidad"">
                                                        </td>
                       ");
                WriteLiteral(@"                             </tr>


                                                </table>
                                                <button class=""btn btn-primary"" data-toggle=""modal"" data-target=""#fm-modal"">Agregar</button>
                                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
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
                                </div>
                            </div>
                        </div>
                    </div>



                    <div class=""columna col-lg-5"">
                        <div class=""card"">
                            <div class=""card-header"">
                                Datos de Habitación
                            </div>
                            <div class=""card-body"">
                                <div class=""row mb-3"">
                                    <div class=""col-6"">
                                        <span class=""input-group-text"">N° de Habitación</span>
                                    </div>
                                    <div class=""col-6"">
                                        <input type=""text""");
            BeginWriteAttribute("name", " name=\"", 5585, "\"", 5592, 0);
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=\"", 5593, "\"", 5598, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control"">
                                    </div>
                                </div>
                                <div class=""row mb-3"">
                                    <div class=""col-6"">
                                        <span class=""input-group-text"">Hora del Pedido</span>
                                    </div>
                                    <div class=""col-6"">
                                        <input type=""text""");
            BeginWriteAttribute("name", " name=\"", 6074, "\"", 6081, 0);
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=\"", 6082, "\"", 6087, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control"">
                                    </div>
                                </div>
                                <div class=""row mb-3"">
                                    <div class=""col-6"">
                                        <span class=""input-group-text"">Nombre del Cliente</span>
                                    </div>
                                    <div class=""col-6"">
                                        <input type=""text""");
            BeginWriteAttribute("name", " name=\"", 6566, "\"", 6573, 0);
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=\"", 6574, "\"", 6579, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control"">
                                    </div>
                                </div>
                                <div class=""row mb-3"">
                                    <div class=""col-6"">
                                        <span class=""input-group-text"">Importe a Pagar</span>
                                    </div>
                                    <div class=""col-6"">
                                        <input type=""text""");
            BeginWriteAttribute("name", " name=\"", 7055, "\"", 7062, 0);
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=\"", 7063, "\"", 7068, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control"">
                                    </div>
                                </div>

                                <div class=""row"">
                                    <p>Estado del Pago:</p>

                                </div>
                                <div class=""row"">
                                    <div class=""form-check"">
                                        <label class=""form-check-label"">
										<input type=""radio"" name=""EstadoPago"" id=""falta"" value=""falta""> Falta Pagar
									</label>
                                    </div>
                                    <div class=""form-check"">
                                        <label class=""form-check-label"">
										<input type=""radio"" name=""EstadoPago"" id=""cancelado"" value=""cancelado""> Cancelado
									</label>
                                    </div>
                                </div>

                            </div>
                            <button class=""btn btn-pr");
            WriteLiteral(@"imary"">Grabar</button>

                        </div>


                    </div>




                </div>


                <div class=""row mt-5"">
                    <div class=""columna col-lg-8"">
                        <table class=""table table-sm table-hover table-bordered table-responsive-lg table-active"">
                            <div class=""row mb-3"">


                            </div>


                            <thead>

                                <tr>
                                    <th>Tipo</th>
                                    <th>Nombre</th>
                                    <th>Cantidad</th>
                                    <th>Precio Venta</th>
                                    <th>Sub Total</th>
                                    <th>Editar</th>
                                    <th>Eliminar</th>
                                </tr>
                            </thead>

                            <tr");
            BeginWriteAttribute("class", " class=\"", 9092, "\"", 9100, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                <td>Bebida</td>
                                <td>Coca Cola</td>
                                <td>4</td>
                                <td>2.50</td>
                                <td>10.00</td>
                                <td> <a href=""#""><i class=""icon-edit""></i></a></td>
                                <td> <a href=""#""><i class=""icon-logout""></i></a></td>
                            </tr>
                            </tr>


                            <tr");
            BeginWriteAttribute("class", " class=\"", 9621, "\"", 9629, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                <td>Bebida</td>
                                <td>Coca Cola</td>
                                <td>4</td>
                                <td>2.50</td>
                                <td>10.00</td>
                                <td> <a href=""#""><i class=""icon-edit""></i></a></td>
                                <td> <a href=""#""><i class=""icon-logout""></i></a></td>
                            </tr>
                            <tr>
                                <td>Bebida</td>
                                <td>Coca Cola</td>
                                <td>4</td>
                                <td>2.50</td>
                                <td>10.00</td>
                                <td> <a href=""#""><i class=""icon-edit""></i></a></td>
                                <td> <a href=""#""><i class=""icon-logout""></i></a></td>
                            </tr>
                            </tr>


                        </table>
                    ");
            WriteLiteral("</div>\r\n                </div>\r\n            </main>\r\n");
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