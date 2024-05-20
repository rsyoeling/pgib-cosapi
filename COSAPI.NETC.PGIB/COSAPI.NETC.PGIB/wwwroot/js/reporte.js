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
                height: 800,
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
var ctx = document.getElementById('myChart1');
var myChart = new Chart(ctx, {
    type: 'bar',
    data: {
        datasets: [{
            label: 'Elementos',
            backgroundColor: ['#6bf1ab', '#63d69f', '#438c6c', '#509c7f', '#1f794e', '#34444c', '#90CAF9', '#64B5F6', '#42A5F5', '#2196F3', '#0D47A1', '#6bf1ab', '#63d69f', '#438c6c', '#509c7f', '#1f794e', '#34444c', '#90CAF9', , '#6bf1ab', '#63d69f', '#438c6c', '#509c7f', '#1f794e', '#34444c', '#90CAF9'],
            borderColor: ['black'],
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
})

let url = 'http://localhost:3500/avanceEjecutadoTotal'
fetch(url)
    .then(response => response.json())
    .then(datos => mostrar(datos))
    .catch(error => console.log(error))


const mostrar = (items) => {
    items.forEach(element => {
        myChart.data['labels'].push(element.nivel)
        myChart.data['datasets'][0].data.push(element.total)
        myChart.update()
        var lp_total = element["VIGA POSTENSADA"];
        console.log(element);
        console.log(lp_total);
        /* document.getElementById('lp_Total').innerHTML = Number(lp_total).toFixed(2) */

    });
    //console.log(myChart.data)
}


