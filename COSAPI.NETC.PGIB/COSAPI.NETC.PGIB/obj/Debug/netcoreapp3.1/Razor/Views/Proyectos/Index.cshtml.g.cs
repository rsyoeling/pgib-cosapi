#pragma checksum "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Proyectos\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5ae153f312a80394e604fe959e309fa2af5bf6c50193f902a95ce8d45cb05550"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Proyectos_Index), @"mvc.1.0.view", @"/Views/Proyectos/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\_ViewImports.cshtml"
using COSAPI.NETC.PGIB;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\_ViewImports.cshtml"
using COSAPI.NETC.PGIB.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"5ae153f312a80394e604fe959e309fa2af5bf6c50193f902a95ce8d45cb05550", @"/Views/Proyectos/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d3ff00ad2a56c4aaf88538e94b5dbc75bae147f384bc9b0396a119806ee172d3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Proyectos_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("uploadForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 1 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Proyectos\Index.cshtml"
  
    ViewData["Title"] = "Proyectos";
    List<Proyectos> vl_Proyectos = ViewBag.ListarProyectos;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container mt-5"">
    <h2>Proyectos BIM</h2>
    <!-- Botón para abrir el modal //Agregar Proyecto -->
    <button type=""button"" class=""btn btn-primary"" data-toggle=""modal"" data-target=""#proyectoModal"">

        Cargar Imagen de Proyecto
    </button>

    <div id=""proyectosContainer"" class=""row mt-3"">
");
#nullable restore
#line 15 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Proyectos\Index.cshtml"
         foreach (var item in vl_Proyectos)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"card-container card m-2\">\r\n                <div class=\"tooltip-trigger\"");
            BeginWriteAttribute("title", " title=\"", 583, "\"", 683, 9);
            WriteAttributeValue("", 591, "Creado", 591, 6, true);
            WriteAttributeValue(" ", 597, "por:", 598, 5, true);
            WriteAttributeValue(" ", 602, "<br/>F.", 603, 8, true);
            WriteAttributeValue(" ", 610, "Creado:", 611, 8, true);
#nullable restore
#line 18 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Proyectos\Index.cshtml"
WriteAttributeValue(" ", 618, item.fechaCreacion, 619, 19, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 638, "<br/>", 639, 6, true);
            WriteAttributeValue(" ", 644, "F.", 645, 3, true);
            WriteAttributeValue(" ", 647, "Modificado:", 648, 12, true);
#nullable restore
#line 18 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Proyectos\Index.cshtml"
WriteAttributeValue(" ", 659, item.fechaModificacion, 660, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" style=""height: 200px; width: 250px;"">
                    <div class=""card-body"" style=""height: 200px; width: 250px; overflow: hidden; position: relative; text-align: center;"">
                        <button style=""position: absolute; top: -4px; right: -4px;"" class=""btn""");
            BeginWriteAttribute("onclick", " onclick=\"", 959, "\"", 997, 3);
            WriteAttributeValue("", 969, "deletebtn(", 969, 10, true);
#nullable restore
#line 20 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Proyectos\Index.cshtml"
WriteAttributeValue("", 979, item.idProyectos, 979, 17, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 996, ")", 996, 1, true);
            EndWriteAttribute();
            WriteLiteral(">X</button>\r\n\r\n                        <img");
            BeginWriteAttribute("src", " src=\"", 1041, "\"", 1059, 1);
#nullable restore
#line 22 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Proyectos\Index.cshtml"
WriteAttributeValue("", 1047, item.imagen, 1047, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Imagen\" style=\"height: 100%; width: 100%; object-fit: cover;\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1127, "\"", 1198, 4);
            WriteAttributeValue("", 1137, "window.location.href", 1137, 20, true);
            WriteAttributeValue(" ", 1157, "=", 1158, 2, true);
            WriteAttributeValue(" ", 1159, "\'../modelos/Index/\'+", 1160, 21, true);
#nullable restore
#line 22 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Proyectos\Index.cshtml"
WriteAttributeValue(" ", 1180, item.idProyectos, 1181, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n                        <h5 class=\"card-title\" style=\"position: absolute; bottom: 0; left: 0; right: 0; background-color: rgba(255, 255, 255, 0.8); padding: 0px; margin: 0;\">");
#nullable restore
#line 24 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Proyectos\Index.cshtml"
                                                                                                                                                                         Write(item.cr);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 24 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Proyectos\Index.cshtml"
                                                                                                                                                                                    Write(item.nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 28 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Proyectos\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </div>
</div>
<!-- Modal para agregar proyecto -->
<div class=""modal"" id=""proyectoModal"" tabindex=""-1"" role=""dialog"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Cargar imagen de Proyecto</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <!-- Formulario para ingresar datos del proyecto -->
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ae153f312a80394e604fe959e309fa2af5bf6c50193f902a95ce8d45cb0555010531", async() => {
                WriteLiteral(@"
                    <div class=""form-group"">
                        <label for=""crInput"">CR:</label>
                        <input type=""text"" class=""form-control"" id=""crInput"" required maxlength=""9"">
                    </div>
                    <div class=""form-group"">
                        <label for=""nombreInput"">Nombre:</label>
                        <input type=""text"" class=""form-control"" id=""nombreInput"" required maxlength=""10"">
                    </div>
                    <div class=""form-group"">
                        <label for=""descripcionInput"">Descripción:</label>
                        <textarea class=""form-control"" id=""descripcionInput"" required maxlength=""200""></textarea>
                    </div>
                    <div class=""form-group"">
                        <label for=""imageFile"">Imagen</label>
                        <input type=""file"" name=""imageFile"" id=""imageFile"" class=""form-control"" required>
                    </div>
                    <button type");
                WriteLiteral("=\"submit\" class=\"btn btn-primary\">Agregar</button>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
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
    $(document).ready(function () {
        $('.tooltip-trigger').tooltipster({
            theme: 'tooltipster-shadow',
            contentAsHTML: true,
            interactive: true
        });
    });

    $('#uploadForm').submit(function (e) {
        e.preventDefault();

        var formData = new FormData();
        formData.append('crInput', $('#crInput').val());
        formData.append('nombreInput', $('#nombreInput').val());
        formData.append('descripcionInput', $('#descripcionInput').val());
        formData.append('imageFile', $('#imageFile')[0].files[0]);
        
        $.ajax({
            url: '");
#nullable restore
#line 86 "C:\Users\Computer\Desktop\pgib-cosapi\COSAPI.NETC.PGIB\COSAPI.NETC.PGIB\Views\Proyectos\Index.cshtml"
             Write(Url.Action("Upload", "Proyectos"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"', //'/Proyectos/Upload', // Reemplaza 'Controller' con el nombre de tu controlador
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                // console.log(data);
                if (data === null) {
                    swal('Error', 'Ingrese una imagen en formato correcto.', 'error');
                } else {
                    var response = JSON.parse(data);
                    //console.log('if 1: ' + response.content);
                    if (response.content == 1) {
                        limpiarCampos();
                        $('#proyectoModal').modal('hide');
                        swal('Éxito', ""Proyecto registrado."", ""success"").then(function () {
                            location.reload(); //agregarfila();
                        });

                    }
                }
            },
            error: function (error) {
                // Manejar er");
            WriteLiteral(@"rores
                console.log(error);
            }
        });
    });

    function limpiarCampos() {
        var formulario = document.getElementById('uploadForm');
        var elementos = formulario.elements;

        for (var i = 0; i < elementos.length; i++) {
            var tipo = elementos[i].type.toLowerCase();
            if (tipo === ""text"" || tipo === ""file"" || tipo === ""textarea"") {
                elementos[i].value = """";
            }
        }
    }

    function deletebtn(IdProy) {
        
        Swal.fire({
            title: ""¿Seguro que quieres eliminar proyecto?"",
            showCancelButton: true,
            confirmButtonText: ""Aprobar"",
        }).then((result) => {
            if (result[""value""]) {
                //    fetch('/Usuario/EliminarUsuario?idUsuario=' + IdUsuario + '&estatus=' + (Estado == ""A"" ? 0 : 1), {
                //        method: 'PUT'
                //    })
                //        .then(response => response.json())
     ");
            WriteLiteral(@"           //        .then(data => {
                //            if (data.content == 1) {
                //                agregarfila();
                //                Swal.fire(""Éxito"", ""Usuario "" + (Estado == ""A"" ? ""Inactivado"" : ""Activado""), ""success"");
                //            }
                //        })
                //        .catch(error => {
                //            console.error('Error:', error);
                //        });

            }
        });

        //Swal.fire(""Éxito"", ""Usuario "" + (Estado == ""A"" ? ""Inactivado"" : ""Activado""), ""success"");
    }
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
