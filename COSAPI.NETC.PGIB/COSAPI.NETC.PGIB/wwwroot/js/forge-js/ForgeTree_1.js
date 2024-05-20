$(document).ready(function () {
  prepareAppBucketTree();
  $("#refreshBuckets").click(function () {
    $("#appBuckets").jstree(true).refresh();
  });

  $("#createNewBucket").click(function () {
    createNewBucket();
  });

  $("#createBucketModal").on("shown.bs.modal", function () {
    $("#newBucketKey").focus();
  });

  $("#hiddenUploadField").change(function () {
    var node = $("#appBuckets").jstree(true).get_selected(true)[0];
    var _this = this;
    if (_this.files.length == 0) return;
    var file = _this.files[0];
    switch (node.type) {
      case "bucket":
        var formData = new FormData();
        formData.append("fileToUpload", file);
        formData.append("bucketKey", node.id);

        $.ajax({
          url: "/api/forge/oss/objects",
          data: formData,
          processData: false,
          contentType: false,
          type: "POST",
          success: function (data) {
            //console.log(data);
            $("#appBuckets").jstree(true).refresh_node(node);
            _this.value = "";
          },
        });
        break;
    }
  });
});

function createNewBucket() {
  var bucketKey = $("#newBucketKey").val();
  jQuery.post({
    url: "/api/forge/oss/buckets",
    contentType: "application/json",
    data: JSON.stringify({ bucketKey: bucketKey }),
    success: function (res) {
      $("#appBuckets").jstree(true).refresh();
      $("#createBucketModal").modal("toggle");
    },
    error: function (err) {
      if (err.status == 409) alert("Bucket already exists - 409: Duplicated");
      console.log(err);
    },
  });
}

function prepareAppBucketTree() {
  $("#appBuckets")
    .jstree({
      core: {
        themes: { icons: true },
        data: {
          url: "/api/forge/oss/buckets",
          dataType: "json",
          multiple: false,
          data: function (node) {
            return { id: node.id };
          },
        },
      },
      types: {
        default: {
          icon: "glyphicon glyphicon-question-sign",
        },
        "#": {
          icon: "glyphicon glyphicon-cloud",
        },
        bucket: {
          icon: "glyphicon glyphicon-folder-open",
        },
        object: {
          icon: "glyphicon glyphicon-file",
        },
      },
      plugins: ["types", "state", "sort", "contextmenu"],
      contextmenu: { items: autodeskCustomMenu },
    })
    .on("loaded.jstree", function () {
      $("#appBuckets").jstree("open_all");
    })
    .bind("activate_node.jstree", function (evt, data) {
      if (data != null && data.node != null && data.node.type == "object") {
        $("#forgeViewer").empty();
        var urn = data.node.id;
        console.log(urn);
        if (
          urn ==
          "dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6dGVzdF9jcl91bGltYS9DUjMxMTEwLUNJVC1CSU0tQS1DQVJELnJ2dA=="
        ) {
          /* console.log('A');
          console.log('crear HOT'); */
          crear_hot("http://localhost:3500/getMetrado3061arqui");
          //var pages = document.getElementById('paginado');
          //pages.firstChild.remove();
          $("#cntElementos").html("");
          var selects = document.querySelector("select.form-control");
          //selects.remove();
          //ejecutar la funcion getArticulos
        }
        if (
          urn ==
          "dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6dGVzdF9jcl91bGltYS9DUjMxMDMtQ0lULUJNLVMtMDAwMS5ydnQ="
        ) {
          /* console.log('S'); */
          $("#cntElementos").html("");
          crear_hot("http://localhost:3500/getMetrado3061struc");
        }
        function crear_hot(url) {
          $.ajax({
            url: url,
            type: "GET",
            async: true,
            cache: false,
            contentType: "application/json",
            success: function (response) {
              var pages = document.querySelector(".pages");
              var card = document.getElementsByClassName("card");
              var myRows = Array.from(response.keys());
              console.log(myRows);

              var example = document.getElementById("example1"),
                hot;
              var hot = new Handsontable(example, {
                language: "es-MX",
                readOnly: true,
                data: response,
                stretchH: "all",
                //colWidths: 100,
                //rowHeights: '20px',
                rowHeights: 5,
                manualRowResize: true,
                width: "100%",
                height: "1000",
                colHeaders: [
                  "VISOR",
                  "ID_ELEMENTO",
                  "ID_PARTIDA",
                  "DESC. ELEMENTO",
                  "NIVEL",
                  "FASE",
                  "CANTIDAL T.",
                  "UNIDAD",
                ],
                //rowHeights: 100,
                //rowHeaders: true,
                //colHeaders: true,
                contextMenu: false,
                multiColumnSorting: true,
                filters: true,
                dropdownMenu: [
                  "filter_by_condition",
                  "filter_operators",
                  "filter_by_value",
                  "filter_action_bar",
                ],
                search: {
                  searchResultClass: "FondoSearch",
                },
                search: true,
                licenseKey: "non-commercial-and-evaluation",
                /* hiddenRows: {
                            rows: getArray(1),
                            indicators: false
                        } */
                outsideClickDeselects: false,
                selectionMode: "multiple",
              });
              var rowsOnSinglePage = 50;

              var afterFilterSet = hot.getData();
              hot.updateSettings({
                afterFilter: function () {
                  afterFilterSet = hot.getData();
                  pages.innerHTML = "";
                  createPages(rowsOnSinglePage);
                  hot.updateSettings({
                    hiddenRows: {
                      rows: getArray(1),
                      indicators: false,
                    },
                  });
                },
              });
              //ejecutar al cambiar el valor de slcRows
              $("#slcRows").on("change", function (event) {
                event.preventDefault();
                //pages.firstChild.remove();
                rowsOnSinglePage = $(this).val();
                //afterFilterSet = GetAfterFilterSet();
                createPages(rowsOnSinglePage, afterFilterSet.length);
                hot.updateSettings({
                  hiddenRows: {
                    rows: getArray(1),
                    indicators: false,
                  },
                });
              });
              getButton.addEventListener("click", (event) => {
                const selected = hot.getSelected() || [];
                const data = [];
                var suma = 0;
                for (let i = 0; i < selected.length; i += 1) {
                  const item = selected[i];
                  data.push(hot.getData(...item));
                }

                data[0].forEach(function (element) {
                  suma += parseInt(element);
                });

                console.log(data);
                console.log(suma);
                //insertar valor en el input
                $("#sum").val(parseInt(suma).toFixed(3));
              });
              /* function createPages(rowsOnSinglePage) {
                        var bt, els = Math.ceil(afterFilterSet.length / rowsOnSinglePage);

                        // bt = document.createElement('select');
                        // bt.classList.add('form-control'); 
                        for (var i = 0; i < els; i++) {
                            bt = document.createElement('button');
                            bt.innerHTML = i + 1;
                            bt.className = 'btn btn-default';

                            bt.addEventListener('click', function (e) {

                                var el = e.target;

                                var page = el.innerHTML;


                                var start = (page - 1) * rowsOnSinglePage;
                                var end = start + rowsOnSinglePage;
                                console.log('inicio: ' + start + ' fin: ' + end);
                                hot.updateSettings({
                                    hiddenRows: {
                                        rows: getArray(start, end),
                                        indicators: false
                                    }
                                })

                            });

                            pages.appendChild(bt);
                        }


                      
                        console.log('cree el paginado');
                    }; */
              $("#btn_sum").click(function () {
                console.log("hello");
                const selected = hot.getSelected() || [];
                console.log(selected);
                const data = [];

                /* for (let i = 0; i < selected.length; i += 1) {
                            const item = selected[i];
                            console.log(selected[i]);
                        } */
              });
              function createPages(rowsOnSinglePage) {
                var bt,
                  els = Math.ceil(afterFilterSet.length / rowsOnSinglePage);
                $("#cntElementos").html(
                  "Se encontraron " + afterFilterSet.length + " elementos"
                );
                bt = document.createElement("select");
                bt.classList.add("form-control");
                pages.appendChild(bt);
                for (var i = 0; i < els; i++) {
                  var opt = document.createElement("option");
                  bt.appendChild(opt);
                  opt.innerHTML = i + 1;
                  opt.value = i + 1;

                  console.log("cree el paginado new");
                }
                bt.addEventListener("change", function (e) {
                  var el = e.target;

                  var page = el.value;
                  console.log(page);

                  var start = (page - 1) * rowsOnSinglePage;
                  var end = start + rowsOnSinglePage;
                  console.log("inicio: " + start + " fin: " + end);
                  hot.updateSettings({
                    hiddenRows: {
                      rows: getArray(page),
                      indicators: false,
                    },
                  });
                });

                if (pages.childElementCount > 1) {
                  pages.firstChild.remove();
                }
                console.log("cree el paginado");
              }
              createPages(rowsOnSinglePage); //we define how many rows should be on a single page

              /* pages.addEventListener('click', function (e) {
                        var clicked = e.target.innerHTML;
                        if (e.taget !== pages) {
                            hot.updateSettings({
                                hiddenRows: {
                                    rows: getArray(clicked),
                                    indicators: false
                                }
                            })
                        }
                    }); */

              function getArray(clicked) {
                var parts = pages.childElementCount;
                var arr = [];

                if (clicked === 1) {
                  for (
                    var i = clicked * rowsOnSinglePage;
                    i < afterFilterSet.length;
                    i++
                  ) {
                    arr.push(i);
                  }
                  return arr;
                } else {
                  for (
                    var j = 0;
                    j < clicked * rowsOnSinglePage - rowsOnSinglePage;
                    j++
                  ) {
                    arr.push(j);
                  }
                  for (
                    var i = clicked * rowsOnSinglePage;
                    i < afterFilterSet.length;
                    i++
                  ) {
                    arr.push(i);
                  }
                  return arr;
                }
              }

              hot.updateSettings({
                hiddenRows: {
                  rows: getArray(1),
                  indicators: false,
                },
              });
              function acomodar_table() {
                var mitab = document.getElementById("example1");
                mitab.style.paddingLeft = 0;
                mitab.style.paddingRight = 0;
                var mi_content_tab = document.getElementsByClassName("htCore");
                mi_content_tab[0].deleteTHead();
              }

              var vl_TextoBuscar = $(
                "#" + pControlDestino + "_txtBuscar"
              ).val();

              const search = hot.getPlugin("search");

              const queryResult = search.query(vl_TextoBuscar);

              //console.log(queryResult);
              acomodar_table();
              hot.render();
              acomodar_table();
            },
          });
        }
        getForgeToken(function (access_token) {
          jQuery.ajax({
            url:
              "https://developer.api.autodesk.com/modelderivative/v2/designdata/" +
              urn +
              "/manifest",
            headers: { Authorization: "Bearer " + access_token },
            success: function (res) {
              if (res.status === "success") launchViewer(urn);
              else
                $("#forgeViewer").html(
                  "La traducción se está ejecutanto, progreso: " +
                    res.progress +
                    ". Por favor, intente en unos minutos"
                );
            },
            error: function (err) {
              var msgButton =
                "Aún no se inicia la traducción! " +
                '<button class="btn btn-xs btn-info" onclick="translateObject()"><span class="glyphicon glyphicon-eye-open"></span> ' +
                "Comenzar traducción</button>";
              $("#forgeViewer").html(msgButton);
            },
          });
        });
      }
    });
}

function autodeskCustomMenu(autodeskNode) {
  var items;

  switch (autodeskNode.type) {
    case "bucket":
      items = {
        uploadFile: {
          label: "Subir modelo Revit",
          action: function () {
            uploadFile();
          },
          icon: "glyphicon glyphicon-cloud-upload",
        },
      };
      break;
    case "object":
      items = {
        translateFile: {
          label: "Empezar traducción",
          action: function () {
            var treeNode = $("#appBuckets").jstree(true).get_selected(true)[0];
            translateObject(treeNode);
          },
          icon: "glyphicon glyphicon-eye-open",
        },
        envioScope: {
          label: "Enviar data a SCOPe",
          action: function () {
            $.ajax({
              url: "https://developer.api.autodesk.com/modelderivative/v2/designdata/dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6cHJveS1jcnVsaW1hXzIvQ1IzMTExMC1DSVQtQk0tQS1DQVJELnJ2dA/metadata/7cb79168-733b-387a-52b7-d5a1abc33c95/properties?forceget=true",
              type: "GET",
              headers: {
                Authorization:
                  "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IlU3c0dGRldUTzlBekNhSzBqZURRM2dQZXBURVdWN2VhIn0.eyJzY29wZSI6WyJjb2RlOmFsbCIsImRhdGE6d3JpdGUiLCJkYXRhOnJlYWQiLCJidWNrZXQ6Y3JlYXRlIiwiYnVja2V0OmRlbGV0ZSIsImJ1Y2tldDpyZWFkIl0sImNsaWVudF9pZCI6ImJ1YnBjYTl0YkxDMjB5VEI3T2hsd09yZmFmWHZnaFFDIiwiYXVkIjoiaHR0cHM6Ly9hdXRvZGVzay5jb20vYXVkL2Fqd3RleHA2MCIsImp0aSI6Ims0VTRlbThmUHhDWGhYNHBjaHR0dUFqalZoNnBvNjJ4TkZWSE1UVlVsa2NoenlPZ2hERHRwYmNFNXJFSGk5TloiLCJleHAiOjE2NDg1OTEwOTB9.dNc50v8UmBC0WPCoMGFOSC1uIm1aihZGCCrxZbXOrZrlbyq_FjQm1O51mI_OzTkH0d4bfXt-2NNUt36aocAp7-AKJtNqLjWtGsyor7csWZXliLAySTfvKH29QUyG9w1y6k8kqbZkUqmKKCcoifV3MaKWUakrBlIf0cUQiq84qEBNQP_aa7pA7t9HXq9hYUWKOD4RFrc8WTI5EJxUzOH082VRf1plJ5AilMxVTzs12ASbz5BIiaNN9eqK-XNPsAbGJUkYUzscpAWciGhZwlCh7T89fI2I7udYwVPqd1vUhg9IMcpIT7acs7QquUE7shv55ByeyPO5Uev9FXfVwigV9w",
              },
              /* data: {
                    usuario: user,
                    password: pass
                }, */

              success: function (data) {
                //descargar data en json
                //let datos = JSON.parse(data);
                //console.log(data);
                let mdata = data.data.collection;
                //console.log(mdata);
                //console.log(data.collection);
                /* data.collection.forEach(element => {
                        console.log(element.properties.Data);
                       
                    }); */
                var count = 0;
                for (let i = 0; i < mdata.length; i++) {
                  //debugger;
                  if (mdata[i].properties !== undefined) {
                    // si mdata[i].properties.Data es undedined o es null
                    if (
                      mdata[i].properties.Data === undefined ||
                      mdata[i].properties.Data === null
                    ) {
                      console.log("nada");
                    } else {
                      //if (mdata[i].properties.Data !== null || mdata[i].properties.Data !== undefined  ) {
                      /* console.log(mdata[i].properties.Data["COSAPI-EXPORTAR"]);  */
                      // console.log(mdata[i].properties.Data);
                      if (
                        mdata[i].properties.Data["COSAPI-EXPORTAR"] !==
                          undefined ||
                        mdata[i].properties.Data["COSAPI-EXPORTAR"] !== null
                      ) {
                        if (
                          mdata[i].properties.Data["COSAPI-EXPORTAR"] == "Yes"
                        ) {
                          //agregar delay para que no se ejecute todo el tiempo
                          setTimeout(function () {
                            console.log("esperar");
                            count++;
                            console.log("contador: " + count);
                            console.log(mdata[i]);
                            var revit_objectid = mdata[i].objectid;
                            var id_external = mdata[i].externalId;
                            var id_elemento =
                              mdata[i].properties.Data["COSAPI-ID_ELEMENTO"];
                            var area =
                              mdata[i].properties.Construction["COSAPI-AREA"];
                            var avance =
                              mdata[i].properties.Construction["COSAPI-AVANCE"];
                            var campo_frente =
                              mdata[i].properties.Construction[
                                "COSAPI-CAMPO_FRENTE"
                              ];
                            var cantidad =
                              mdata[i].properties.Construction[
                                "COSAPI-CANTIDAD_TOTAL"
                              ];
                            var des_elemento =
                              mdata[i].properties.Construction[
                                "COSAPI-DESCRIPCION_ELEMENTO"
                              ];
                            var ejecutado =
                              mdata[i].properties.Construction[
                                "COSAPI-EJECUTADO"
                              ];
                            var estado_avance =
                              mdata[i].properties.Construction[
                                "COSAPI-ESTADO_AVANCE"
                              ];
                            var factor =
                              mdata[i].properties.Construction["COSAPI-FACTOR"];
                            var fase =
                              mdata[i].properties.Construction["COSAPI-FASE"];
                            var fech_ejecutada =
                              mdata[i].properties.Construction[
                                "COSAPI-FECHA_EJECUTADA"
                              ];
                            var fech_planificada =
                              mdata[i].properties.Construction[
                                "COSAPI-FECHA_PLANIFICADA"
                              ];
                            var h_v =
                              mdata[i].properties.Construction["COSAPI-H/V"];
                            var id_cronograma =
                              mdata[i].properties.Construction[
                                "COSAPI-ID_CRONOGRAMA"
                              ];
                            var id_partida =
                              mdata[i].properties.Construction[
                                "COSAPI-ID_PARTIDA"
                              ];
                            var lote =
                              mdata[i].properties.Construction["COSAPI-LOTE"];
                            var mod_instalacion =
                              mdata[i].properties.Construction[
                                "COSAPI-MOD_INSTALACION"
                              ];
                            var nivel =
                              mdata[i].properties.Construction["COSAPI-NIVEL"];
                            var rubro =
                              mdata[i].properties.Construction["COSAPI-RUBRO"];
                            var sistema =
                              mdata[i].properties.Construction[
                                "COSAPI-SISTEMA"
                              ];
                            var unidad =
                              mdata[i].properties.Construction["COSAPI-UNIDAD"];
                            var wbs_nivel_1 =
                              mdata[i].properties.Construction[
                                "COSAPI-WBS_NIVEL_1"
                              ];

                            console.log(cantidad);
                            console.log(unidad);
                            //hacer un insert con post
                            //create function to insert data
                            new_data(
                              revit_objectid,
                              id_external,
                              id_elemento,
                              area,
                              avance,
                              campo_frente,
                              cantidad,
                              des_elemento,
                              ejecutado,
                              estado_avance,
                              factor,
                              fase,
                              fech_ejecutada,
                              fech_planificada,
                              h_v,
                              id_cronograma,
                              id_partida,
                              lote,
                              mod_instalacion,
                              nivel,
                              rubro,
                              sistema,
                              unidad,
                              wbs_nivel_1
                            );

                            function new_data(
                              revit_objectid,
                              id_external,
                              id_elemento,
                              area,
                              avance,
                              campo_frente,
                              cantidad,
                              des_elemento,
                              ejecutado,
                              estado_avance,
                              factor,
                              fase,
                              fech_ejecutada,
                              fech_planificada,
                              h_v,
                              id_cronograma,
                              id_partida,
                              lote,
                              mod_instalacion,
                              nivel,
                              rubro,
                              sistema,
                              unidad,
                              wbs_nivel_1
                            ) {
                              $.ajax({
                                url: "http://localhost:3500/ws_bim_arquitectura",
                                type: "POST",
                                data: {
                                  revit_objectid: revit_objectid,
                                  id_external: id_external,
                                  id_elemento: id_elemento,
                                  area: area,
                                  avance: avance,
                                  campo_frente: campo_frente,
                                  cantidad: cantidad,
                                  des_elemento: des_elemento,
                                  ejecutado: ejecutado,
                                  estado_avance: estado_avance,
                                  factor: factor,
                                  fase: fase,
                                  fech_ejecutada: fech_ejecutada,
                                  fech_planificada: fech_planificada,
                                  h_v: h_v,
                                  id_cronograma: id_cronograma,
                                  id_partida: id_partida,
                                  lote: lote,
                                  mod_instalacion: mod_instalacion,
                                  nivel: nivel,
                                  rubro: rubro,
                                  sistema: sistema,
                                  unidad: unidad,
                                  wbs_nivel_1: wbs_nivel_1,
                                },
                                success: function (data) {
                                  console.log(data);
                                },
                              });
                            }
                          }, 2000);

                          /* console.log(mdata[i].properties.Data["COSAPI-ID_ELEMENTO"]);
                                        console.log(mdata[i].properties.Construction["COSAPI-COTA_INFERIOR"]);
                                        console.log(mdata[i].properties.Construction["COSAPI-FECHA_EJECUTADA"]) */
                        }
                      }
                    }
                  }
                }
              },
              error: function (error) {
                alert("Error al registrar usuario");
              },
            });
          },
          icon: "glyphicon glyphicon-send",
        },
      };
      break;
  }

  return items;
}

function uploadFile() {
  $("#hiddenUploadField").click();
}

function translateObject(node) {
  $("#forgeViewer").empty();
  if (node == null) node = $("#appBuckets").jstree(true).get_selected(true)[0];
  var bucketKey = node.parents[0];
  var objectKey = node.id;
  jQuery.post({
    url: "/api/forge/modelderivative/jobs",
    contentType: "application/json",
    data: JSON.stringify({ bucketKey: bucketKey, objectName: objectKey }),
    success: function (res) {
      $("#forgeViewer").html("La traducción empezó, espere unos minutos");
    },
  });
}
