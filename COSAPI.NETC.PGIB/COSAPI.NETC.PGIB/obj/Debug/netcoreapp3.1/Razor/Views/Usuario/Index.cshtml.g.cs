#pragma checksum "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7cbd3ed724a902710dfccf761bd1fb8673eb0948"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Usuario_Index), @"mvc.1.0.view", @"/Views/Usuario/Index.cshtml")]
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
#line 1 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\_ViewImports.cshtml"
using COSAPI.NETC.PGIB;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\_ViewImports.cshtml"
using COSAPI.NETC.PGIB.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7cbd3ed724a902710dfccf761bd1fb8673eb0948", @"/Views/Usuario/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aa2bc4fdf0f82918345b302dae75a6758dd2cbac", @"/Views/_ViewImports.cshtml")]
    public class Views_Usuario_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/global_assets/images/img_avatar.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Avatar"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:200px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("addUserForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
  
    ResponseUsuario vl_Usuario = ViewBag.ListarUsuario;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""card-body"" style=""padding: 0px; height: 100vh"">
    <!-- viewer html col-4-->
    <div class=""container mt-5"">
        <h1 style=""margin-bottom:-0.5em;"">Mantenimiento de Usuarios</h1>
        <hr>
        <br>
        <div style=""text-align: center;"">
            <!--<img src=""https://www.w3schools.com/howto/img_avatar.png"" alt=""Avatar"" style=""width:200px"">-->
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "7cbd3ed724a902710dfccf761bd1fb8673eb09486461", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            <!-- <button class=""btn btn-success btn-add"" onclick=""addUser()"">Añadir Usuario</button> -->
        </div>
        <div class=""card-body "" style=""padding: 0px; height: 100vh"">
            <!-- viewer html -->
            <div class=""container mt-5"">
                <button id=""addUserBtn"" class=""btn btn-primary mb-3"" data-toggle=""modal"" data-target=""#addUserModal"" onclick=""activarNuevo()"">Añadir Usuario</button>
                <table class=""table table-bordered table-hover"" id=""tbMantUsuario"">
                    <thead>
                        <tr style=""background-color: #f2f2f2;"">
                            <th style=""display: none;"">idUsuario</th>
                            <th>Usuario</th>
                            <th>Nombre</th>
                            <th>Rol</th>
                            <th>Correo</th>
                            <th>Estado</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
      ");
            WriteLiteral("              <tbody id=\"userTable\">\r\n");
#nullable restore
#line 32 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
                         foreach (var item in vl_Usuario.content)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td style=\"display: none;\">");
#nullable restore
#line 35 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
                                                      Write(item.idUsuario);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 36 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
                               Write(item.usuario);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 37 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
                               Write(item.nombresCompleto);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 38 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
                               Write(item.rolNombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 39 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
                               Write(item.correoElectronico);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 40 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
                                Write(item.status == 1 ? "A" : "I");

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>\r\n                                    <button class=\"btn btn-primary edit-btn\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2244, "\"", 2278, 3);
            WriteAttributeValue("", 2254, "editbtn(", 2254, 8, true);
#nullable restore
#line 42 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
WriteAttributeValue("", 2262, item.idUsuario, 2262, 15, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2277, ")", 2277, 1, true);
            EndWriteAttribute();
            WriteLiteral(">Editar</button>\r\n                                    <button class=\"btn btn-danger delete-btn\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2374, "\"", 2441, 10);
            WriteAttributeValue("", 2384, "deletebtn(", 2384, 10, true);
#nullable restore
#line 43 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
WriteAttributeValue("", 2394, item.idUsuario, 2394, 15, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2409, ",", 2409, 1, true);
#nullable restore
#line 43 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
WriteAttributeValue(" ", 2410, item.status, 2411, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2423, "==", 2424, 3, true);
            WriteAttributeValue(" ", 2426, "1", 2427, 2, true);
            WriteAttributeValue(" ", 2428, "?", 2429, 2, true);
            WriteAttributeValue(" ", 2430, "\'A\'", 2431, 4, true);
            WriteAttributeValue(" ", 2434, ":", 2435, 2, true);
            WriteAttributeValue(" ", 2436, "\'I\')", 2437, 5, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 43 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
                                                                                                                                              Write(item.status == 1 ? "Inactivar" : "Activar");

#line default
#line hidden
#nullable disable
            WriteLiteral("</button>\r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 46 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
<div class=""modal"" id=""addUserModal"">
    <div class=""modal-dialog"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Añadir Usuario</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
            </div>
            <div class=""modal-body"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7cbd3ed724a902710dfccf761bd1fb8673eb094814040", async() => {
                WriteLiteral(@"
                    <input type=""hidden"" class=""form-control"" name=""IdUsuario"" id=""IdUsuario"">
                    <div class=""form-group"">
                        <label for=""Nombres"">Nombres</label>
                        <input type=""text"" class=""form-control"" name=""Nombres"" id=""Nombres"">
                    </div>
                    <div class=""form-group"">
                        <label for=""ApellidoPaterno"">Apellido Paterno</label>
                        <input type=""text"" class=""form-control"" name=""ApellidoPaterno"" id=""ApellidoPaterno"">
                    </div>
                    <div class=""form-group"">
                        <label for=""ApellidoMaterno"">Apellido Materno</label>
                        <input type=""text"" class=""form-control"" name=""ApellidoMaterno"" id=""ApellidoMaterno"">
                    </div>
                    <div class=""form-group"">
                        <label for=""Rol"">Rol</label>
                        <select class=""form-control"" name=""Rol"" id=""Ro");
                WriteLiteral("l\">\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7cbd3ed724a902710dfccf761bd1fb8673eb094815444", async() => {
                    WriteLiteral("--Seleccione--");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7cbd3ed724a902710dfccf761bd1fb8673eb094816704", async() => {
                    WriteLiteral("Administrador");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7cbd3ed724a902710dfccf761bd1fb8673eb094817963", async() => {
                    WriteLiteral("Operador");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                        </select>
                    </div>
                    <div class=""form-group"">
                        <label for=""Usuario"">Usuario</label>
                        <input type=""text"" class=""form-control"" name=""Usuario"" id=""Usuario"">
                    </div>
                    <div class=""form-group"">
                        <label for=""Password"">Contraseña</label>
                        <input type=""password"" class=""form-control"" name=""Password"" id=""Password"">
                    </div>
                    <div class=""form-group"">
                        <label for=""Correo"">Correo</label>
                        <input type=""text"" class=""form-control"" name=""Correo"" id=""Correo"">
                    </div>
                    <button type=""submit"" class=""btn btn-primary"" id=""BtnInsUsu"">Registrar</button>
                    <button type=""submit"" class=""btn btn-primary"" id=""BtnActUsu"" style=""display:none;"">Actualizar</button>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
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
<script>
    $('#BtnInsUsu').click(function (event) {
        event.preventDefault();
        var selectElement = document.getElementById('Rol');
        var valorSeleccionado = selectElement.value;

        var requestUsuario =");
#nullable restore
#line 108 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
                        Write(Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(new RequestUsuario())));

#line default
#line hidden
#nullable disable
            WriteLiteral(@";

        requestUsuario.nombres = document.getElementById('Nombres').value;
        requestUsuario.apellidoPaterno = document.getElementById('ApellidoPaterno').value;
        requestUsuario.apellidoMaterno = document.getElementById('ApellidoMaterno').value;
        requestUsuario.idRol = parseInt(valorSeleccionado);
        requestUsuario.usuario = document.getElementById('Usuario').value;
        requestUsuario.clave = document.getElementById('Password').value;
        requestUsuario.correoElectronico = document.getElementById('Correo').value;

        //$('#addUserModal').modal('hide');
        //swal('Éxito', ""Usuario registrado."", ""success"");
        //agregarfila();
        //=================================================
        fetch('/Usuario/InsertarUsuario', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestUsuario)
        })
            .then(response => response.j");
            WriteLiteral(@"son())
            .then(data => {
                //console.log(data);
                if (data.content == 1) {
                    limpiarCampos();
                    $('#addUserModal').modal('hide');
                    agregarfila();
                    swal('Éxito', ""Usuario registrado."", ""success"");
                }
            })
            .catch(error => {
                console.error('Error:', error);
            });

    });

    function limpiarCampos() {
        var formulario = document.getElementById('addUserForm');
        var elementos = formulario.elements;

        for (var i = 0; i < elementos.length; i++) {
            var tipo = elementos[i].type.toLowerCase();
            if (tipo === ""text"" || tipo === ""password"" || tipo === ""hidden"") {
                elementos[i].value = """";
            } else if (tipo === ""select-one"") {
                elementos[i].selectedIndex = 0;
            }
        }
    }

    function activarNuevo() {
        limpiarCampo");
            WriteLiteral(@"s();
        document.getElementById('BtnInsUsu').style.display = 'block';
        document.getElementById('BtnActUsu').style.display = 'none';
        document.getElementById('Usuario').disabled = false;
        document.getElementById('Password').disabled = false;
    }

    function agregarfila() {
        fetch('/Usuario/ActualizarListado')
            .then(response => response.text())
            .then(data => {
                document.getElementById('tbMantUsuario').getElementsByTagName('tbody')[0].innerHTML = data;
            });
    }

    function editbtn(IdUsuario) {
        document.getElementById('BtnInsUsu').style.display = 'none';
        document.getElementById('BtnActUsu').style.display = 'block';
        //var userId = this.getAttribute('data-id');
        document.getElementById('IdUsuario').value = IdUsuario; //userId;
        fetch('/Usuario/listarusuarioid?idUsuario=' + IdUsuario) //userId
            .then(response => response.json()) //text()
            .then(d");
            WriteLiteral(@"ata => {
                //console.log(data);
                $('#addUserModal').modal('show');

                data.content.forEach(item => {
                    document.getElementById('Nombres').value = item.nombres;
                    document.getElementById('ApellidoPaterno').value = item.apellidoPaterno;
                    document.getElementById('ApellidoMaterno').value = item.apellidoMaterno;
                    document.getElementById('Rol').value = item.idRol;
                    document.getElementById('Usuario').disabled = true;
                    document.getElementById('Usuario').value = item.usuario;
                    document.getElementById('Password').disabled = true;
                    document.getElementById('Password').value = item.clave;
                    document.getElementById('Correo').value = item.correoElectronico;

                });
            });
  }

    $('#BtnActUsu').click(function (event) {
        event.preventDefault();
        var selectElem");
            WriteLiteral("ent = document.getElementById(\'Rol\');\r\n        var valorSeleccionado = selectElement.value;\r\n\r\n        var requestUsuario =");
#nullable restore
#line 206 "F:\2024\cosapi\general\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Usuario\Index.cshtml"
                        Write(Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(new RequestActUsu())));

#line default
#line hidden
#nullable disable
            WriteLiteral(@";

        requestUsuario.idUsuario = parseInt(document.getElementById('IdUsuario').value);
        requestUsuario.nombres = document.getElementById('Nombres').value;
        requestUsuario.apellidoPaterno = document.getElementById('ApellidoPaterno').value;
        requestUsuario.apellidoMaterno = document.getElementById('ApellidoMaterno').value;
        requestUsuario.idRol = parseInt(valorSeleccionado);
        requestUsuario.correoElectronico = document.getElementById('Correo').value;

        fetch('/Usuario/ActualizarUsuario', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestUsuario)
        })
            .then(response => response.json())
            .then(data => {
                if (data.content == 1) {
                    limpiarCampos();
                    $('#addUserModal').modal('hide');
                    agregarfila();
                    swal('Éxito', ""Usuario a");
            WriteLiteral(@"ctualizado."", ""success"");
                }
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });

    function deletebtn(IdUsuario, Estado) {
        Swal.fire({
            title: ""¿Seguro que quieres "" + (Estado == ""A"" ? ""Inactivar"" : ""Activar"") + "" Usuario ?"",
            showCancelButton: true,
            confirmButtonText: ""Aprobar"",
        }).then((result) => {
            //console.log(result[""value""]);
            //result.isConfirmed
            if (result[""value""]) {

                fetch('/Usuario/EliminarUsuario?idUsuario=' + IdUsuario + '&estatus=' + (Estado == ""A"" ? 0 : 1), {
                    method: 'PUT'
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.content == 1) {
                            agregarfila();
                            Swal.fire(""Éxito"", ""Usuario "" + (Estado == ""A"" ? ""Inactivado"" : ""Activa");
            WriteLiteral(@"do""), ""success"");
                        }
                    })
                    .catch(error => {
                        console.error('Error:', error);
                    });

            }
        });

        //Swal.fire(""Éxito"", ""Usuario "" + (Estado == ""A"" ? ""Inactivado"" : ""Activado""), ""success"");
    }

</script>");
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
