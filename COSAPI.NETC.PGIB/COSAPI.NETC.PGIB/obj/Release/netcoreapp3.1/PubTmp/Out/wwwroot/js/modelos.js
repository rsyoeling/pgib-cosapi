const urnbase = 'urn:adsk.objects:os.object:';
var bucket = 'test2_chile_antofagasta/';
var grid; 
//leer el valor de selectedCR
var selectedCR = localStorage.getItem('selectedCR');
localStorage.setItem('bucket', bucket);
function crear_tabla(uri) {
    $.ajax({
        url: 'http://localhost:3500/getModelos',
        //datos a enviar
        data: { cr: selectedCR },
        type: "POST",    
        dataType: "json",
        beforeSend: function () {
            $(document.getElementById('mi_search')).append("<img id='imgLoad' src='/img/load.gif' style='width:40px;margin:auto;' /><p id='idCargando'>Cargando...</p>");
        },
        success: function (elements) {
            
            $(document.getElementById('imgLoad')).remove();
            $(document.getElementById('idCargando')).remove();
            console.log(elements);
            function removeDuplicates(items, field) {
                var getter = function (item) { return item[field] },
                    result = [],
                    index = 0,
                    seen = {};

                while (index < items.length) {
                    var item = items[index++],
                        text = getter(item);

                    if (text !== undefined && text !== null && !seen.hasOwnProperty(text)) {
                        result.push(item);
                        seen[text] = true;
                    }
                }

                return result;
            }

            var filterSource = new kendo.data.DataSource({
                data: elements
            });
            var record = 0;
            grid = $("#grid").kendoGrid({
                
            //$("#grid").kendoGrid({    
                dataSource: {
                    data: elements,
                    schema: {
                        model: {
                            fields: {
                                DESCRIPCION: { type: "string" },
                                VERSION: { type: "number" },
                                DISCIPLINA: { type: "string" },
                                FECHA_CREACION: { type: "string" },
                                USUARIO: { type: "string" },
                                ESTADO: { type: "number" }
                            }
                        }
                    },
                    pageSize: 10,
                    change: function (e) {
                        filterSource.data(e.items);
                    },
                },
                reorderable: true,
                resizable: true,
                //height: 400,
                height: "100vh",
                scrollable: {
                    virtual: false
                },
                //groupable: true,
                //sortable: true,
                columnMenu: true,
                lockable: true,
                //dataBound: onDataBound,
                toolbar: ["search"],
                pageable: {
                    input: true,
                    numeric: false
                },
                filterable: true,
                /* filterable: {
                    multi: true,
                    search: true
                }, */
                
                columns: [
                    { title: "NOMBRE",field: "DESCRIPCION",width:"150px", lockable: true, filterable: { multi: true, search: true }, width: "auto"} ,
                    { field: "VERSION",width:"50px", lockable: true, width: "auto",  filterable: { multi: true, search: true } , attributes: { style: "text-align:center;" }},
                    { field: "DISCIPLINA",width:"150px", lockable: true, width: "auto",  filterable: { multi: true, search: true } },
                    { field: "FECHA_CREACION",width:"150px", lockable: true, width: "auto",  filterable: { multi: true, search: true } , attributes: { style: "text-align:center;" }},
                    { field: "USUARIO", lockable: true, width: "auto",
                       /*  filterable: {
                            multi: true,
                            search: true,
                            dataSource: filterSource,
                            itemTemplate: function (e) {
                                return e.DESCRIPCION;
                            }
                        } */

                     /*ancho automatico */  filterable: { multi: true, search: true }}, 
                    { field: "ACCIONES", width: "auto", template:"<div><button id='btn-model' type='button' class='btn btn-primary m-1'>	"+	                    	
                    "<i class='icon-arrow-right15'></i><i class='icon-office'></i>  Navegar</button> "+
                    "<button id='btn-parameter' type='button' class='btn btn-primary m-1'><i class='icon-arrow-right15'></i><i class='icon-office'></i>  Actualizar 🔼 </button> "+
                    " <button id='btn-del-model' type='button' class='btn btn-danger rounded-round'>"+
                    "<i class='icon-cancel-circle2'></i>  Eliminar</button></div> " ,lockable: true, filterable: { multi: true, search: true } }  
                ],
                dataBinding: function() {
                    record = (this.dataSource.page() -1) * this.dataSource.pageSize();
                }
            })
            
        }
    });

}
//click en btn-model
$(document).on('click', '#btn-model', function () {
    //valor de la fila seleccionada segunda columna
    var valor = $(this).closest('tr').find('td:eq(0)').text();
    var urntotal = urnbase + bucket + valor;
    //convert urntotal to base64
    var urn64 = btoa(urntotal);
    console.log(urntotal);
    console.log(urn64);
    //seteear urn64 en localstorage
    localStorage.setItem('urn', urn64);
    //redireccionar a avance
    window.location.href = '/avance';

});
//click en btn-parameter
$(document).on('click','#btn-parameter',function(){
    //valor de la fila seleccionada segunda columna
    var valor = $(this).closest('tr').find('td:eq(1)').text();
    var urntotal = urnbase + bucket + valor;
    //convert urntotal to base64
    var urn64 = btoa(urntotal);
    //seteear urn64 en localstorage
    localStorage.setItem('urn', urn64);
    //redireccionar a avance
    window.location.href = '/parametros';
});
// click en btn-del-model
$(document).on('click','#btn-del-model',function(){
    //valor de la fila seleccionada segunda columna
    var valor = $(this).closest('tr').find('td:eq(1)').text();
    var dataToDelete = [
        { item: valor }
      ];
      
    //alert(valor);
    //invocar delete object de forge autodesk https://developer.api.autodesk.com/oss/v2/buckets/test2_chile_antofagasta/objects/30990-ETM-GE-MOD-S-0001.rvt
    //añadir animación de cargando mientras elimina el modelo
    $.ajax({
        url: 'https://developer.api.autodesk.com/oss/v2/buckets/'+bucket+'objects/'+valor,
        type: "DELETE",
        headers: {
            Authorization : localStorage.getItem("token")},
        })
        //mostrar mensaje de eliminando con swal
        .done(function() {
            swal("Se eliminó el modelo", "El modelo se ha eliminado", "success");
            
        })
        .fail(function() {
            swal("Error", "No se pudo eliminar el modelo", "error");
        });
    //eliminar también de la Base de Datos delete http://localhost:3500/deleteModelos
    $.ajax({
        url: 'http://localhost:3500/deleteModelos',
        type: "DELETE",
        data: JSON.stringify(dataToDelete),
        dataType: "json",
        })
        .done(function() {
            //recargar la tabla
            grid.dataSource.read();
            grid.refresh();
            swal("Se eliminó el modelo de la base de datos", "El modelo se ha eliminado", "success");
        })
        .fail(function() {
            //alert("no se pudo eliminar de la base de datos");
            swal("Error", "No se pudo eliminar el modelo de la base de datos", "error");
        });            
});

    