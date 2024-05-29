$(document).ready(function () {
    
    var urn = localStorage.getItem("urn");
    //viewer
    launchViewer(urn);
    crear_tabla('http://localhost:3500/getAvancexModelo');
    /*
    var viewer;
    
    var options = {
        env: 'AutodeskProduction',
        getAccessToken: function (onGetAccessToken) {
            var token = localStorage.getItem("token");
            onGetAccessToken(token, 3600);
        }
    };
    Autodesk.Viewing.Initializer(options, function () {
        viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById('viewer'), {
            extensions: [ 'Autodesk.DocumentBrowser','HandleSelectionExtension1','MyAwesomeExtension','SummaryIcon'] 
        });
        viewer.start();
        
        Autodesk.Viewing.Document.load('urn:' + urn, onDocumentLoadSuccess, onDocumentLoadFailure);
    });
    function onDocumentLoadSuccess(doc) {
        var viewables = doc.getRoot().getDefaultGeometry();
        viewer.loadDocumentNode(doc, viewables).then(i => {
            // documented loaded, any action?
        });
    }
    function onDocumentLoadFailure(viewerErrorCode) {
        console.error('onDocumentLoadFailure() - errorCode:' + viewerErrorCode);
    }
    */

    function crear_tabla(uri) {
        $.ajax({
            url: uri,
            type: "POST",
            data: { bim_modelo: "6" },
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

                var grid = $("#grid").kendoGrid({
                    dataSource: {
                        data: elements,
                        schema: {
                            model: {
                                fields: {
                                    VISOR: { type: "string" },
                                    ID_PARTIDA: { type: "string" },
                                    DESCRIPCION_ELEMENTO: { type: "string" },
                                    NIVEL: { type: "string" },
                                    FASE: { type: "string" },
                                    UNIDAD: { type: "string" },
                                    AVANCE: { type: "string" },
                                    ESTADO_AVANCE: { type: "string" },
                                    CANTIDAD_AVANCE: { type: "number" },
                                    TOTAL: { type: "number" },
                                    FECHA_EJECUTADA: { type: "string" },
                                    FECHA_PLANIFICADA: { type: "date" }
                                }
                            }
                        },
                        pageSize: 150,
                        change: function (e) {
                            filterSource.data(e.items);
                        },
                    },
                    reorderable: true,
                    resizable: true,
                    height: "100vh",
                    scrollable: {
                        virtual: false
                    },
                    groupable: true,
                    //sortable: true,
                    columnMenu: true,
                    lockable: true,
                    dataBound: onDataBound,
                    toolbar: ["excel", "search"],
                    pageable: {
                        input: true,
                        numeric: false
                    },
                    filterable: true,
                    /* filterable: {
                        multi: true,
                        search: true
                    }, */
                    filterMenuInit: function (e) {
                        var grid = e.sender;
                        e.container.data("kendoPopup").bind("open", function () {
                            filterSource.sort({ field: e.field, dir: "asc" });
                            //var uniqueDsResult = removeDuplicates(grid.dataSource.view(), e.field);
                            //uniqueDsResult VIEW ALL DATA
                            var uniqueDsResult = removeDuplicates(grid.dataSource.data(), e.field);
                            filterSource.data(uniqueDsResult);
                        })
                    },
                    columns: [
                        {
                            field: "VISOR", width: 100, filterable: {
                                multi: true,
                                dataSource: filterSource,
                                search: true
                            },
                            //bloquear columna
                            //locked: true
                        },
                        { field: "ID_PARTIDA", lockable: true, width: 150, filterable: { multi: true, search: true } },
                        { field: "DESCRIPCION_ELEMENTO", lockable: true, width: 300, filterable: { multi: true, search: true } },
                        { field: "NIVEL", lockable: true, width: 150, filterable: { multi: true, search: true } },
                        { field: "FASE", lockable: true, width: 150, filterable: { multi: true, search: true } },
                        { field: "TOTAL", title: "METRADO", lockable: true, width: 150, filterable: { multi: true, search: true }, attributes: { style: "text-align:right;" }, 
                        //aggregates: [ "sum" ],
                        //groupFooterTemplate: "Mestrado total: #: sum #" 
                            },
                        { field: "UNIDAD", lockable: true, width: 150, filterable: { multi: true, search: true }, attributes: { style: "text-align:right;" } },
                        { field: "AVANCE", lockable: true, width: 150, filterable: { multi: true, search: true }, attributes: { style: "text-align:right;" } },
                        { field: "ESTADO_AVANCE", lockable: true, width: 150, filterable: { multi: true, search: true } },
                        { field: "CANTIDAD_AVANCE", title: "METRADO-AVANCE", lockable: true, width: 150, filterable: { multi: true, search: true }, attributes: { style: "text-align:right;" } },
                        //{ field: "ESTADO_AVANCE", lockable: true, width: 150, filterable: { multi: true, }, attributes: { style: "text-align:right;" } },

                        { field: "FECHA_EJECUTADA", lockable: true, width: 150, filterable: { multi: true, search: true }, attributes: { style: "text-align:right;" } },
                        { field: "FECHA_PLANIFICADA", lockable: true, width: 150, filterable: { multi: true, search: true }, attributes: { style: "text-align:right;" } }

                    ]
                })
                function onDataBound(e) {
                    var grid = this;
                    grid.table.find("tr").each(function () {
                        var dataItem = grid.dataItem(this);
                        var themeColor = dataItem.Discontinued ? 'success' : 'error';
                        var text = dataItem.Discontinued ? 'available' : 'not available';

                        $(this).find(".badgeTemplate").kendoBadge({
                            themeColor: themeColor,
                            text: text,
                        });

                        $(this).find(".rating").kendoRating({
                            min: 1,
                            max: 5,
                            label: false,
                            selection: "continuous"
                        });

                        $(this).find(".sparkline-chart").kendoSparkline({
                            legend: {
                                visible: false
                            },
                            data: [dataItem.TargetSales],
                            type: "bar",
                            chartArea: {
                                margin: 0,
                                width: 180,
                                background: "transparent"
                            },
                            seriesDefaults: {
                                labels: {
                                    visible: true,
                                    format: '{0}%',
                                    background: 'none'
                                }
                            },
                            categoryAxis: {
                                majorGridLines: {
                                    visible: false
                                },
                                majorTicks: {
                                    visible: false
                                }
                            },
                            valueAxis: {
                                type: "numeric",
                                min: 0,
                                max: 130,
                                visible: false,
                                labels: {
                                    visible: false
                                },
                                minorTicks: { visible: false },
                                majorGridLines: { visible: false }
                            },
                            tooltip: {
                                visible: false
                            }
                        });

                        kendo.bind($(this), dataItem);
                    });
                }
            }
        });

    }
    //crear el grido con los datos de http://localhost:3500/getMetradoAvance
    //llenar las columnas con los valores devueltos
    function crear_tabla_metrado(){
        $.ajax({
            url: 'http://localhost:3500/getMetradoAvance',
            type: "GET",
            data: { bim_modelo: "6" },
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

                var grid1 = $("#gridMetrado").kendoGrid({
                    dataSource: {
                        data: elements,
                        schema: {
                            model: {
                                fields: {
                                    FECHA_EJECUTADA : { type: "string" },
                                    UNIDAD : { type: "string" },
                                    METRADO : { type: "number" },
                                    METRADO_AVANCE : { type: "number" },
                                }
                            }
                        },
                        pageSize: 150,
                        change: function (e) {
                            filterSource.data(e.items);
                        },
                    },
                    reorderable: true,
                    resizable: true,
                    height: "100vh",
                    scrollable: {
                        virtual: false
                    },
                    groupable: true,
                    //sortable: true,
                    columnMenu: true,
                    lockable: true,
                    dataBound: onDataBound,
                    toolbar: ["excel", "search"],
                    pageable: {
                        input: true,
                        numeric: false
                    },
                    filterable: true,
                    /* filterable: {
                        multi: true,
                        search: true
                    }, */
                    filterMenuInit: function (e) {
                        var grid1 = e.sender;
                        e.container.data("kendoPopup").bind("open", function () {
                            filterSource.sort({ field: e.field, dir: "asc" });
                            //var uniqueDsResult = removeDuplicates(grid.dataSource.view(), e.field);
                            //uniqueDsResult VIEW ALL DATA
                            var uniqueDsResult = removeDuplicates(grid1.dataSource.data(), e.field);
                            filterSource.data(uniqueDsResult);
                        })
                    },
                    columns: [
                        { field: "FECHA_EJECUTADA", lockable: true, width: 150, filterable: { multi: true, search: true } },
                        { field: "UNIDAD", lockable: true, width: 150, filterable: { multi: true, search: true } },
                        { field: "METRADO", lockable: true, width: 150, filterable: { multi: true, search: true }, attributes: { style: "text-align:right;" },},
                        /* aggregates: [ "sum" ],
                        groupFooterTemplate: "Mestrado total: #: sum #" 
                            }, */
                        { field: "METRADO_AVANCE", lockable: true, width: 150, filterable: { multi: true, search: true }, attributes: { style: "text-align:right;" } },
                        



                    ]
                })
                function onDataBound(e) {
                    var grid1 = this;
                    grid1.table.find("tr").each(function () {
                        var dataItem = grid1.dataItem(this);
                        var themeColor = dataItem.Discontinued ? 'success' : 'error';
                        var text = dataItem.Discontinued ? 'available' : 'not available';

                        $(this).find(".badgeTemplate").kendoBadge({
                            themeColor: themeColor,
                            text: text,
                        });

                        $(this).find(".rating").kendoRating({
                            min: 1,
                            max: 5,
                            label: false,
                            selection: "continuous"
                        });

                        $(this).find(".sparkline-chart").kendoSparkline({
                            legend: {
                                visible: false
                            },
                            data: [dataItem.TargetSales],
                            type: "bar",
                            chartArea: {
                                margin: 0,
                                width: 180,
                                background: "transparent"
                            },
                            seriesDefaults: {
                                labels: {
                                    visible: true,
                                    format: '{0}%',
                                    background: 'none'
                                }
                            },
                            categoryAxis: {
                                majorGridLines: {
                                    visible: false
                                },
                                majorTicks: {
                                    visible: false
                                }
                            },
                            valueAxis: {
                                type: "numeric",
                                min: 0,
                                max: 130,
                                visible: false,
                                labels: {
                                    visible: false
                                },
                                minorTicks: { visible: false },
                                majorGridLines: { visible: false }
                            },
                            tooltip: {
                                visible: false
                            }
                        });

                        kendo.bind($(this), dataItem);
                    });
                }
            }
        });

    }

    // mostrar en un modal el avance ejecutado dentro de un grid
    var btnVerMetrado = $("#btnVerMetradoEjecutado");
    btnVerMetrado.click(function () {
        $("#modalVerMetradoEjecutado").modal("show");
        crear_tabla_metrado();
        console.log('holiss')
    }
    );

    //ejecutar el post http://localhost:3500/getParteDiario con clic en btnCalcularRup
    var btnCalcularRup = $("#btnCalcularRup");
    var lblfecha_inicio = $("#fecha_inicio");
    var lblfecha_fin = $("#fecha_fin");
    btnCalcularRup.click(function () {
        $.ajax({
            url: 'http://localhost:3500/getParteDiario',
            type: "POST",
            data: { fecha_inicio: lblfecha_inicio.val(), fecha_fin: lblfecha_fin.val(), cr: "crventas" },
            dataType: "json",
            //alerta swal de éxito
            success: function (elements) {
                swal("¡Bien hecho!", "Se ha calculado el RUP", "success");
                // ahora consumir el get getbimhoraspd
                /* $.ajax({
                    url: 'http://localhost:3500/getbimhoraspd',
                    type: "GET",
                    dataType: "json",
                    data: { fecha_inicio: lblfecha_inicio.val(), fecha_fin: lblfecha_fin.val(), cr: "CRVENTAS" },
                    //alerta swal de éxito
                    success: function (elements) {
                        swal("¡Bien hecho!", "Se ha calculado el RUP", "success");
                        //mostrar los datos en gridRup
                        //var gridRup = $("#gridRup").kendoGrid({

                    },
                    //alerta swal de error
                    error: function (elements) {
                        swal("¡Error!", "No se ha calculado el RUP", "error");
                    }

            },
            ) */
        },
         //alerta swal de error
         error: function (elements) {
            swal("¡Error!", "No se ha calculado el RUP", "error");
        }
    });
    });


});
