//ejecutar al iniciar el documento
$(document).ready(function(){

    /* const Url="https://developer.api.autodesk.com/modelderivative/v2/designdata/dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6cHJveS1jcnVsaW1hXzIvQ1IzMTExMC1DSVQtQk0tQS1DQVJELVYyLnJ2dA/metadata/7cb79168-733b-387a-52b7-d5a1abc33c95/properties?forceget=true";
    function traedatar() {
   //get json con parametros     
    $.getJSON (Url , function ( result) {
    Console. log ( result) */

    // recorrer un ciclo for pero esperar 1 segundo para cada iteracion
    /* for (var i = 0; i < 10; i++) {
        setTimeout(function () {
            console.log(i);
        }, 1000);
    } */
    
    $.ajax({
        url: "https://developer.api.autodesk.com/modelderivative/v2/designdata/dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6dGVzdDJfY2hpbGVfYW50b2ZhZ2FzdGEvQ1IzMDA1MS1URS1CTS1Nb2RlbG9kZVBpZWRyYS5ydnQ/metadata/74e70240-063a-4a9f-a480-79ae303d4c6b/properties?forceget=true",
        type: "GET",
        headers: {Authorization: "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IlU3c0dGRldUTzlBekNhSzBqZURRM2dQZXBURVdWN2VhIn0.eyJzY29wZSI6WyJjb2RlOmFsbCIsImRhdGE6d3JpdGUiLCJkYXRhOnJlYWQiLCJidWNrZXQ6Y3JlYXRlIiwiYnVja2V0OmRlbGV0ZSIsImJ1Y2tldDpyZWFkIl0sImNsaWVudF9pZCI6IndJSGpxR1ZrNUdlc28xWkdhTEZwc3FVMFVpWDdVUDRqIiwiYXVkIjoiaHR0cHM6Ly9hdXRvZGVzay5jb20vYXVkL2Fqd3RleHA2MCIsImp0aSI6IkNFVzBpS2ZZTVBjOHBkdVpHTzRiWEpwakJNZUNBbGFITnBTV1l3RDlwa3EweE91Y1RXMXplRlkzWDlOTDRRZlQiLCJleHAiOjE2Njc0ODU0OTd9.a1QJxILzWTtP4pdesDUFXPIa-NzvTg4VT8try_GxouBahjyxpXIR_HWF0PPAN9aO9975rlf2-x6fFuX7lBtj8Ncr-4WRzNqbYTtY1kjRd_Y1bdtnBNyfr93mr3Pl4_zeNc9n46xZKxF_p-XL5mEdeG7L-rfHdTUVccoJkAq4Um3_G6B3owob8ItsiZW0SECoNWbjrmFAUc4GCQ7nxIZqxt8f5Gdaljy6kohAWz6m4dfri1dwj95Qx-VKRGrzSN1vZcl8E0Clo6vU5emLMNWY-VlTS8-SaAitXOcusXjD4L-dqi33rYGFPpG6aaRQeMlQQE5x718OOadgQ3ji1X83CQ"},
        /* data: {
            usuario: user,
            password: pass
        }, */
        
        success: function(data){
            //descargar data en json
            //let datos = JSON.parse(data);
            console.log(data);
            var extract = [];
            let mdata = data.data.collection;
            console.log(mdata);
            //console.log(data.collection);
            /* data.collection.forEach(element => {
                console.log(element.properties.Data);
               
            }); */
            var count = 0;
            for (let i = 0; i < mdata.length; i++) {
                //debugger;
                //setTimeout(function(){
                if (mdata[i].properties !== undefined) {
                    
                    // si mdata[i].properties.Data es undedined o es null   
                    //if (mdata[i].properties.Data === undefined || mdata[i].properties.Data === null) {
                        //console.log('nada')
                    //}
                   // else {
                    //if (mdata[i].properties.Data !== null || mdata[i].properties.Data !== undefined  ) {
                        /* console.log(mdata[i].properties.Data["COSAPI-EXPORTAR"]);  */
                       // console.log(mdata[i].properties.Data);
                        //if( mdata[i].properties.Data["COSAPI-EXPORTAR"] !== undefined || mdata[i].properties.Data["COSAPI-EXPORTAR"] !== null ){ 
                            //if (mdata[i].properties.Data["COSAPI-EXPORTAR"] == "Yes" ) {
                                //agregar delay para que no se ejecute todo el tiempo
                                
                                    //console.log('esperar');
                                    count++;
                                    /* console.log('contador: ' +count);
                                    console.log(mdata[i]); */
                                    var revit_objectid = mdata[i].objectid;
                                    console.log(revit_objectid);
                                    //si mdata[i].properties["Identity Data"] es undefined o null 
                                    if (mdata[i].properties["Identity Data"] === undefined || mdata[i].properties["Identity Data"] === null) {
                                        var type_name = '';
                                    }
                                    else {
                                        if (mdata[i].properties["Identity Data"]["Type Name"] == undefined || mdata[i].properties["Identity Data"]["Type Name"] == null) {
                                            var type_name = '';
                                            }
                                            else {
                                            var type_name = mdata[i].properties["Identity Data"]["Type Name"];
                                            }
                                            console.log(type_name);
                                    }    
                                    //var id_external = mdata[i].externalId;
                                    //var id_elemento = mdata[i].properties.Data["COSAPI-ID_ELEMENTO"];
                                    //var area = mdata[i].properties.Construction["COSAPI-AREA"];
                                    //var avance = mdata[i].properties.Construction["COSAPI-AVANCE"];
                                    //var avance_produc = mdata[i].properties.Construction["COSAPI-AVANCE_PRODUC"];
                                    //var campo_frente = mdata[i].properties.Construction["COSAPI-CAMPO_FRENTE"];
                                    //var cantidad_avance = mdata[i].properties.Construction["COSAPI-CANTIDAD_AVANCE"];
                                    //var cantidad_produc = mdata[i].properties.Construction["COSAPI-CANTIDAD_PRODUC"];
                                    //var cantidad_total = mdata[i].properties.Construction["COSAPI-CANTIDAD_TOTAL"];
                                    //var des_elemento = mdata[i].properties.Construction["COSAPI-DESCRIPCION_ELEMENTO"];
                                    /* var ejecutado = mdata[i].properties.Construction["COSAPI-EJECUTADO"];
                                    var estado_avance = mdata[i].properties.Construction["COSAPI-ESTADO_AVANCE"];
                                    var factor = mdata[i].properties.Construction["COSAPI-FACTOR"];
                                    var fase = mdata[i].properties.Construction["COSAPI-FASE"];
                                    var fech_ejecutada = mdata[i].properties.Construction["COSAPI-FECHA_EJECUTADA"];
                                    var fech_produc = mdata[i].properties.Construction["COSAPI-FECHA_PRODUC"];
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
                                    var wbs_nivel_1 = mdata[i].properties.Construction["COSAPI-WBS_NIVEL_1"]; */

                                    //console.log(cantidad);
                                    //console.log(unidad);
                                    // insertar valores a extract
                                    extract.push({  revit_objectid : revit_objectid,
                                                    type_name : type_name,
                                                    //id_external : id_external,
                                                    //id_elemento : id_elemento,
                                                    //area : area,
                                                    //avance : avance, //el porcentaje
                                                    //avance_produc : avance_produc,
                                                    //campo_frente : campo_frente,
                                                    //cantidad_avance : cantidad_avance, // calculado** = avance * metrado
                                                    //cantidad_produc : cantidad_produc,
                                                    //cantidad_total : cantidad_total, //esto es metrado
                                                    //des_elemento : des_elemento,
                                                    /* ejecutado : ejecutado,
                                                    estado_avance : estado_avance,
                                                    factor : factor,
                                                    fase : fase,
                                                    fech_ejecutada : fech_ejecutada,
                                                    fech_produc : fech_produc,
                                                    fech_planificada : fech_planificada,
                                                    h_v : h_v,
                                                    id_cronograma : id_cronograma,
                                                    id_partida : id_partida,
                                                    lote : lote,
                                                    mod_instalacion : mod_instalacion,
                                                    nivel : nivel,
                                                    rubro : rubro,
                                                    sistema : sistema,
                                                    unidad : unidad,
                                                    wbs_nivel_1 : wbs_nivel_1 */
                                                });
                                    
                                    //hacer un insert con post
                                    //create function to insert data
                                    /* new_data(revit_objectid, id_external, id_elemento, area, avance, campo_frente, cantidad, des_elemento, ejecutado, estado_avance, factor, fase, fech_ejecutada, fech_planificada, h_v, id_cronograma, id_partida, lote, mod_instalacion, nivel, rubro, sistema, unidad, wbs_nivel_1); */
                                    
                                


                                   /*  function new_data (revit_objectid, id_external, id_elemento, area, avance, campo_frente, cantidad, des_elemento, ejecutado, estado_avance, factor, fase, fech_ejecutada, fech_planificada, h_v, id_cronograma, id_partida, lote, mod_instalacion, nivel, rubro, sistema, unidad, wbs_nivel_1) {
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
                                    }  */
                                
                                
                            //}
                                

                                


                                /* console.log(mdata[i].properties.Data["COSAPI-ID_ELEMENTO"]);
                                console.log(mdata[i].properties.Construction["COSAPI-COTA_INFERIOR"]);
                                console.log(mdata[i].properties.Construction["COSAPI-FECHA_EJECUTADA"]) */
                        //    }
                            
                            
                        //}
                    }
                        
                
                //}, 5000);
            }
            console.log(extract[0]);
            var insert_1 = '';
            var insert_2 = '';
            //var row = 0;
            //var txtArea = document.getElementById("txtArea");
            extract.forEach(function(element) {
                //row++;
                
                //insert_1 += "insert into BIM_30051_FP ( REVIT_OBJECTID, Area, Avance, Campo_Frente, Cantidad_Total, Descripcion_Elemento, Ejecutado, Estado_Avance, factor, Fase, Fecha_Ejecutada, Fecha_Planificada, h_v, Id_Cronograma, Id_Partida, Lote, Mod_Instalacion, Nivel, Rubro, sistema, Unidad, WBS_NIVEL_1, Fec_Creacion, version ) values ('" +element.revit_objectid +"',' ."+/*element.area */+" ', '."+/*element.avance */+"', '."+/*element.campo_frente */+"', '."+/*element.cantidad_total */+"', '."+/*element.des_elemento */+"', '."+/*element.ejecutado */+"', '."+/*element.estado_avance*/ +"', '."+/*element.factor*/ +"', '"+/*element.fase */+"', TO_DATE( '"+/*element.fech_ejecutada*/+"' , 'dd/mm/yy'), TO_DATE( '"+/*element.fech_planificada*/+"' , 'dd/mm/yy'), '."+/*element.h_v */+"', '."+/*element.id_cronograma*/ +"', '."+/*element.id_partida*/ +"', '."+/*element.lote */+"', '."+/*element.mod_instalacion*/ +"', '."+/*element.nivel */+"', '."+/*element.rubro */+"', '."+/*element.sistema*/ +"', '."+/*element.unidad */+"', '."+/*element.wbs_nivel_1 */+"', SYSDATE, '1') ; \n";
                insert_1 +=  "insert into BIM_30051_FP ( REVIT_OBJECTID, Descripcion_Elemento) VALUES ('"+element.revit_objectid+"', '"+element.type_name+"'); \n";
                //insert_2 +="insert into BIM__30051_FP_LOG_AVANCE ( REVIT_OBJECTID, AVANCE, fecha_ejecutada, CANTIDAD, FECHA_MODIFICACION, USER_MODIFICACION,ESTADO_AVANCE) values ('" +element.revit_objectid +"', '."+/*element.avance */+"', TO_DATE( '"+/*element.fech_ejecutada*/+"' , 'dd/mm/yy'), '."+/*element.cantidad_total*/+"',  SYSDATE , 'RIZQUIERDO','."+/*element.estado_avance*/+"' ) ;\n";
                insert_2 += "insert into BIM__30051_FP_LOG_AVANCE ( REVIT_OBJECTID) VALUES ('"+element.revit_objectid+"'); \n";
                /* insert += row + ',' +element.revit_objectid + "," + element.id_external + "," + element.id_elemento + "," + element.area + "," + element.avance + "," + element.campo_frente + "," + element.cantidad + "," + element.des_elemento + "," + element.ejecutado + "," + element.estado_avance + "," + element.factor + "," + element.fase + "," + element.fech_ejecutada + "," + element.fech_planificada + "," + element.h_v + "," + element.id_cronograma + "," + element.id_partida + "," + element.lote + "," + element.mod_instalacion + "," + element.nivel + "," + element.rubro + "," + element.sistema + "," + element.unidad + "," + element.wbs_nivel_1 + "\n"; */

                /* new_data(element.revit_objectid, element.id_external, element.id_elemento, element.area, element.avance, element.campo_frente, element.cantidad, element.des_elemento, element.ejecutado, element.estado_avance, element.factor, element.fase, element.fech_ejecutada, element.fech_planificada, element.h_v, element.id_cronograma, element.id_partida, element.lote, element.mod_instalacion, element.nivel, element.rubro, element.sistema, element.unidad, element.wbs_nivel_1);
                
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
                }   */                     
            });  
            $('#txtArea1').html(insert_1);  
            $('#txtArea2').html(insert_2); 
        },
        error: function(error){
            alert("Error al registrar usuario");
        }});
   /*  function validar(){
        //validar si existen usuarios en sesion
        if(localStorage.getItem("usuario")!=null){
            //redireccionar a la pagina principal
            window.location.href = "../../views/registroavance.html";
        }
        
    }
    validar();
    
    //click en registrar
    $("#btnRegistrar").click(function(){
        var user = $("#txtusername").val();
        var pass = $("#txtpassword").val(); 
        //post para registrar usuario
        $.ajax({
            url: "http://localhost:3500/usuariobim",
            type: "POST",
            data: {
                usuario: user,
                password: pass
            },
            success: function(data){
                //guardar usuario en sesion
                localStorage.setItem("usuario", user);
                //redireccionar a la pagina principal
                window.location.href = "../../views/registroavance.html";
            },
            error: function(error){
                alert("Error al registrar usuario");
            }});
    }); */
    $("#btnRegistrar").click(function(){
        var user = $("#txtusername").val();
        var pass = $("#txtpassword").val(); 
        //post para registrar usuario
        
    });

});    