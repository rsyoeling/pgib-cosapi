$(document).ready(function () {
    prepareAppBucketTree();
    $('#refreshBuckets').click(function () {
      $('#appBuckets').jstree(true).refresh();
    });

    $('#createNewBucket').click(function () {
      createNewBucket();
    });

    $('#createBucketModal').on('shown.bs.modal', function () {
      $("#newBucketKey").focus();
    })

    $('#hiddenUploadField').change(function () {
      var node = $('#appBuckets').jstree(true).get_selected(true)[0];
      var _this = this;
      if (_this.files.length == 0) return;
      var file = _this.files[0];
      switch (node.type) {
        case 'bucket':
          var formData = new FormData();
          formData.append('fileToUpload', file);
          formData.append('bucketKey', node.id);

          $.ajax({
            url: '/api/forge/oss/objects',
            data: formData,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (data) {
              console.log(data);
              $('#appBuckets').jstree(true).refresh_node(node);
              _this.value = '';
            }
          });
          break;
      }
    });
  });

  function createNewBucket() {
    var bucketKey = $('#newBucketKey').val();
    jQuery.post({
      url: '/api/forge/oss/buckets',
      contentType: 'application/json',
      data: JSON.stringify({ 'bucketKey': bucketKey }),
      success: function (res) {
        console.log(res);
        $('#appBuckets').jstree(true).refresh();
        $('#createBucketModal').modal('toggle');
      },
      error: function (err) {
        if (err.status == 409)
          alert('Bucket already exists - 409: Duplicated')
        console.log(err);
      }
    });
  }

  function prepareAppBucketTree() {
    $('#appBuckets').jstree({
      'core': {
        'themes': { "icons": true },
        'data': {
          "url": '/api/forge/oss/buckets',
          "dataType": "json",
          'multiple': false,
          "data": function (node) {
            return { "id": node.id };
          }
        }
      },
      'types': {
        'default': {
          'icon': 'glyphicon glyphicon-question-sign'
        },
        '#': {
          'icon': 'glyphicon glyphicon-cloud'
        },
        'bucket': {
          'icon': 'glyphicon glyphicon-folder-open'
        },
        'object': {
          'icon': 'glyphicon glyphicon-file'
        }
      },
      "plugins": ["types", "state", "sort", "contextmenu"],
      contextmenu: { items: autodeskCustomMenu }
    }).on('loaded.jstree', function () {
      $('#appBuckets').jstree('open_all');
    }).bind("activate_node.jstree", function (evt, data) {
      if (data != null && data.node != null && data.node.type == 'object') {
        $("#forgeViewer").empty();
        //sessionStorage.setItem('urn', '');
        var urn = data.node.id;
        //console.log(urn);
        sessionStorage.setItem('urn', urn);
        console.log(sessionStorage.getItem('urn'));
        getForgeToken(function (access_token) {
          jQuery.ajax({
            url: 'https://developer.api.autodesk.com/modelderivative/v2/designdata/' + urn + '/manifest',
            headers: { 'Authorization': 'Bearer ' + access_token },
            success: function (res) {
              console.log(res);
              if (res.status === 'success') launchViewer(urn);
              else $("#forgeViewer").html('La traducción se está ejecutanto, progreso: ' + res.progress + '. Por favor, intente en unos minutos');
            },
            error: function (err) {
              var msgButton = 'Aún no se inicia la traducción! ' +
                '<button class="btn btn-xs btn-info" onclick="translateObject()"><span class="glyphicon glyphicon-eye-open"></span> ' +
                'Comenzar traducción</button>'
              $("#forgeViewer").html(msgButton);
            }
          });
        })
      }
    });
  }
  

  function autodeskCustomMenu(autodeskNode) {
    var items;
    console.log('autodesk parámetros');
    switch (autodeskNode.type) {
      case "bucket":
        items = {
          uploadFile: {
            label: "Subir modelo Revit",
            action: function () {
              uploadFile();
            },
            icon: 'glyphicon glyphicon-cloud-upload'
          }
        };
        break;
      case "object":
        items = {
          translateFile: {
            label: "Empezar traducción",
            action: function () {
              var treeNode = $('#appBuckets').jstree(true).get_selected(true)[0];
              translateObject(treeNode);
              
            },
            icon: 'glyphicon glyphicon-eye-open'
          },
          envioScope : {
            label: "Enviar data a SCOPe",
            action: function () {
              $.ajax({
                url: "https://developer.api.autodesk.com/modelderivative/v2/designdata/dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6dGVzdDJfY2hpbGVfYW50b2ZhZ2FzdGEvMDEuMDJfVEM0LnJ2dA/metadata/9746081a-1a2b-5145-faf9-9efe4fa582fc/properties?forceget=true",
                type: "GET",
                headers: {Authorization: "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IlU3c0dGRldUTzlBekNhSzBqZURRM2dQZXBURVdWN2VhIiwicGkuYXRtIjoiN3ozaCJ9.eyJzY29wZSI6WyJjb2RlOmFsbCIsImRhdGE6d3JpdGUiLCJkYXRhOnJlYWQiLCJidWNrZXQ6Y3JlYXRlIiwiYnVja2V0OmRlbGV0ZSIsImJ1Y2tldDpyZWFkIl0sImNsaWVudF9pZCI6IndJSGpxR1ZrNUdlc28xWkdhTEZwc3FVMFVpWDdVUDRqIiwiYXVkIjoiaHR0cHM6Ly9hdXRvZGVzay5jb20vYXVkL2Fqd3RleHA2MCIsImp0aSI6IkZ6b0R3QW5vQkZuRUxUbXdxYnpHa01UUDU2N1VyNlNORFhZV0M5MEpOTWU5N1hBdDFPOXBURDVQQmFRT2hqVjkiLCJleHAiOjE2ODg0ODMyMDJ9.Dg1uw_xFweRarg06QwYaiiP_8Mjp1DF5YZbulGUpPk3RGrCVguGHGl6rqy5UP-nAYIQwIrWytVNxa-JCqnnvanTlpAnB5z4M-Rz_CGwonWqoVJ6BAfgAw4BxWjuJAWZGwwbyxXR-dFGRe6xC2hHjrLis1hCQhjNN-A5Bm9UANirw3WetxVt7t8gW71iY2QFlw3yYRoxI_T_BxnsWQ0SicC39Kh7-7hSR2y5rBEROF_T33W_tVJI1vrmSpuuLGTvPylp0O7JXd7e_iwAdJNVOGQ6-En5DjcH0GmnSKeUT1vXgKn95U7z5IkQx_G5uPm-F-aokbnv0Khb2G_vGHe4sOw"},
                /* data: {
                    usuario: user,
                    password: pass
                }, */
                
                success: function(data){
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
                            if (mdata[i].properties.Data === undefined || mdata[i].properties.Data === null) {
                                console.log('nada')
                            }
                            else {
                            //if (mdata[i].properties.Data !== null || mdata[i].properties.Data !== undefined  ) {
                                /* console.log(mdata[i].properties.Data["COSAPI-EXPORTAR"]);  */
                               // console.log(mdata[i].properties.Data);
                                if( mdata[i].properties.Data["COSAPI-EXPORTAR"] !== undefined || mdata[i].properties.Data["COSAPI-EXPORTAR"] !== null ){ 
                                    if (mdata[i].properties.Data["COSAPI-EXPORTAR"] == "Yes" ) {
                                        //agregar delay para que no se ejecute todo el tiempo
                                        setTimeout(function(){
                                            console.log('esperar');
                                            count++;
                                            console.log('contador: ' +count);
                                            console.log(mdata[i]);
                                            var revit_objectid = mdata[i].objectid;
                                            var id_external = mdata[i].externalId;
                                            var id_elemento = mdata[i].properties.Data["COSAPI-ID_ELEMENTO"];
                                            var area = mdata[i].properties.Construction["COSAPI-AREA"];
                                            var avance = mdata[i].properties.Construction["COSAPI-AVANCE"];
                                            var campo_frente = mdata[i].properties.Construction["COSAPI-CAMPO_FRENTE"];
                                            var cantidad = mdata[i].properties.Construction["COSAPI-CANTIDAD_TOTAL"];
                                            var des_elemento = mdata[i].properties.Construction["COSAPI-DESCRIPCION_ELEMENTO"];
                                            var ejecutado = mdata[i].properties.Construction["COSAPI-EJECUTADO"];
                                            var estado_avance = mdata[i].properties.Construction["COSAPI-ESTADO_AVANCE"];
                                            var factor = mdata[i].properties.Construction["COSAPI-FACTOR"];
                                            var fase = mdata[i].properties.Construction["COSAPI-FASE"];
                                            var fech_ejecutada = mdata[i].properties.Construction["COSAPI-FECHA_EJECUTADA"];
                                            var fech_planificada = mdata[i].properties.Construction["COSAPI-FECHA_PLANIFICADA"];
                                            var h_v = mdata[i].properties.Construction["COSAPI-H/V"];
                                            var id_cronograma = mdata[i].properties.Construction["COSAPI-ID_CRONOGRAMA"];
                                            var id_partida = mdata[i].properties.Construction["COSAPI-ID_PARTIDA"];
                                            var lote = mdata[i].properties.Construction["COSAPI-LOTE"];
                                            var mod_instalacion = mdata[i].properties.Construction["COSAPI-MOD_INSTALACION"];
                                            var nivel = mdata[i].properties.Construction["COSAPI-NIVEL"];
                                            var rubro = mdata[i].properties.Construction["COSAPI-RUBRO"];
                                            var sistema = mdata[i].properties.Construction["COSAPI-SISTEMA"];
                                            var unidad = mdata[i].properties.Construction["COSAPI-UNIDAD"];
                                            var wbs_nivel_1 = mdata[i].properties.Construction["COSAPI-WBS_NIVEL_1"];
        
                                            console.log(cantidad);
                                            console.log(unidad);
                                            //hacer un insert con post
                                            //create function to insert data
                                            new_data(revit_objectid, id_external, id_elemento, area, avance, campo_frente, cantidad, des_elemento, ejecutado, estado_avance, factor, fase, fech_ejecutada, fech_planificada, h_v, id_cronograma, id_partida, lote, mod_instalacion, nivel, rubro, sistema, unidad, wbs_nivel_1);
                                            
                                        
        
        
                                            function new_data (revit_objectid, id_external, id_elemento, area, avance, campo_frente, cantidad, des_elemento, ejecutado, estado_avance, factor, fase, fech_ejecutada, fech_planificada, h_v, id_cronograma, id_partida, lote, mod_instalacion, nivel, rubro, sistema, unidad, wbs_nivel_1) {
                                                $.ajax({
                                                    url: 'http://localhost:3500/ws_bim_arquitectura',
                                                    type: 'POST',
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
                                                        wbs_nivel_1: wbs_nivel_1
                                                    },
                                                    success: function(data){                   
                                                        console.log(data);
                                                        
                                                    }
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
                error: function(error){
                    alert("Error al registrar usuario");
                }});
            },
            icon: 'glyphicon glyphicon-send'
          }
        };
        break;
    }

    return items;
  }

  function uploadFile() {
    $('#hiddenUploadField').click();
  }

  function translateObject(node) {
    $("#forgeViewer").empty();
    if (node == null) node = $('#appBuckets').jstree(true).get_selected(true)[0];
    var bucketKey = node.parents[0];
    var objectKey = node.id;
    jQuery.post({
      url: '/api/forge/modelderivative/jobs',
      contentType: 'application/json',
      data: JSON.stringify({ 'bucketKey': bucketKey, 'objectName': objectKey }),
      success: function (res) {
        $("#forgeViewer").html('La traducción empezó, espere unos minutos');
      },
    });
  }
