const urnbase = 'urn:adsk.objects:os.object:';
var bucket = 'test2_chile_antofagasta/';

localStorage.setItem('bucket', bucket);
//obtener_data();
//obtener_metadata();
function obtener_data(){
    //get ajax
    $.ajax({
        url: 'https://developer.api.autodesk.com/modelderivative/v2/designdata/'+localStorage.getItem('urn')+'/metadata' ,
        type: "GET",
        headers: {
            Authorization : localStorage.getItem("token")},
        success: function(data){
            var urn = data.data.metadata;
            console.log(urn)
            //buscar dentro del arreglo urn el parámetro name = '{3D}'
            urn.forEach(element => {
                
                if(element.name == '{PGIB}' != element.name ){
                    //guardar el valor de guid
                    var guid = element.guid;
                    localStorage.setItem("guid", guid);
                }})
            console.log(localStorage.getItem("guid") );
        }    

    })
};
//obtener metadata
/* function obtener_metadata(){
    $.ajax({
        //url: 'https://developer.api.autodesk.com/modelderivative/v2/designdata/'+localStorage.getItem('urn')+'/metadata/'+localStorage.getItem('guid')+'/properties?forceget=true' ,
        url: 'https://developer.api.autodesk.com/modelderivative/v2/designdata/dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6dGVzdDJfY2hpbGVfYW50b2ZhZ2FzdGEvMzA5OTAtRVRNLUdFLU1PRC1TLTAwMDEucnZ0/metadata/c29cc348-e20f-2428-940e-436ac52603c8/properties?forceget=true',
        type: "GET",
        headers: {
            Authorization : localStorage.getItem("token")},
        success: function(data){
            var mdata = data.data.collection;
            console.log(mdata);
            //mostrarme todos los valores donde mdata.name contenga 'Viga Estructural - Rectangular' más algo xxx
            mdata.forEach(element => {
                if(element.name.includes('Viga Estructural - Rectangular')){
                    console.log(element.name);
                }
            }
            )
            


        }    

    })
}; */
function subirArchivo() {
    console.log('subirArchivo');
    const token = localStorage.getItem('token');
    const inputFile = document.getElementById('archivo');
    const file = inputFile.files[0];

/*     if (!file) {
        Swal.fire({
            title: 'Error',
            text: 'Por favor, selecciona un archivo Revit (.rvt) válido.',
            icon: 'error'
        });
        return;
    } */

    const bucketKey = 'test2_chile_antofagasta'; // Reemplaza con el nombre de tu bucket en Autodesk Forge
    const objectName = file.name;
console.log(objectName); 
    const apiUrl = `https://developer.api.autodesk.com/oss/v2/buckets/${bucketKey}/objects/${objectName}`;

    const formData = new FormData();
    formData.append('file', file);
    // Mostrar spinner de carga
    Swal.fire({
        title: 'Subiendo archivo',
        allowOutsideClick: false,
        onBeforeOpen: () => {
            Swal.showLoading();
        },
    });
    // Realiza la solicitud de carga
    fetch(apiUrl, {
        method: 'PUT',
        headers: {
            'Authorization': token, // Reemplaza con tu token de autorización de Autodesk Forge
        },
        body: file,
    })
    .then(response => response.json())
    .then(data => {
        console.log(data);
        // Ocultar spinner y mostrar mensaje de éxito
        Swal.fire({
            title: 'Archivo subido exitosamente',
            icon: 'success',
        });
        //almacenar el valor de urn
        const base64ObjectId = btoa(data.objectId);
        localStorage.setItem('urn', base64ObjectId);
        // Obtén el elemento del botón por su ID
        const btnCargar = document.getElementById('btnCargar');
        const btnObtenerParametros = document.getElementById('btnObtenerParámetros');
        // Deshabilita el botón
        btnObtenerParametros.disabled = false;


    })
    .catch(error => {
        console.error('Error al subir el archivo:', error);
        // Ocultar spinner y mostrar mensaje de error
        Swal.fire({
            title: 'Error al subir el archivo',
            text: 'Ha ocurrido un error al subir el archivo.',
            icon: 'error',
        });
    });
}
function obtenerParametros(){
    // traducir el archivo y mostrar el progreso
    Swal.fire({
        title: 'Traduciendo archivo',
        allowOutsideClick: false,
        onBeforeOpen: () => {
            Swal.showLoading();
        }
    });
    // usar https://developer.api.autodesk.com/modelderivative/v2/designdata/job para la traducción
    const token = localStorage.getItem('token');
    const urn = localStorage.getItem('urn');
    const apiUrl = 'https://developer.api.autodesk.com/modelderivative/v2/designdata/job';
    const job = {
        input: {
            urn: urn,
        },
        output: {
            formats: [
                {
                    type: 'svf',
                    views: ['2d', '3d'],
                },
            ],
        },
    };
    fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Authorization': token,
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(job),
    })
    .then(response => response.json())
    .then(data => {
        console.log(data);
        // Mostrar mensaje de éxito
        Swal.fire({
            title: 'Archivo traducido exitosamente',
            icon: 'success',
        });
        // Obtener el ID del trabajo
        //const workItemId = data.id;
        // Obtener el estado del trabajo
        obtenerEstadoTrabajo();
      
    })
    .catch(error => {
        console.error('Error al traducir el archivo:', error);
        // Ocultar spinner y mostrar mensaje de error
        Swal.fire({
            title: 'Error al traducir el archivo',
            text: 'Ha ocurrido un error al traducir el archivo.',
            icon: 'error',
        });
    }
    );
    
}
// obtener el estado del trabajo
function obtenerEstadoTrabajo(){
    const token = localStorage.getItem('token');
    const urn = localStorage.getItem('urn');
    const apiUrl = `https://developer.api.autodesk.com/modelderivative/v2/designdata/${urn}/manifest`;
    fetch(apiUrl, {
        method: 'GET',
        headers: {
            'Authorization': token,
        },
    })
    .then(response => response.json())
    .then(data => {
        console.log(data);
        // Obtener el estado del trabajo
        const status = data.status;
        if (status === 'inprogress') {
            setTimeout(() => {
                obtenerEstadoTrabajo();
            }, 5000);
                Swal.fire({
                    title: 'Traduciendo archivo',
                    allowOutsideClick: false,
                    onBeforeOpen: () => {
                        Swal.showLoading();
                    }
                    }); 
        
           
        } else if (status === 'success') {
            // Si el trabajo ha finalizado correctamente, obtener los parámetros
            //obtenerParametros();
            mostrarModelo();
            getParametros();
        } else {
            // Si el trabajo ha fallado, mostrar mensaje de error
            Swal.fire({
                title: 'Error al traducir el archivo',
                text: 'Ha ocurrido un error al traducir el archivo.',
                icon: 'error',
            });
        }
    })
    .catch(error => {
        console.error('Error al obtener el estado del trabajo:', error);
        // Ocultar spinner y mostrar mensaje de error
        Swal.fire({
            title: 'Error al traducir el archivo',
            text: 'Ha ocurrido un error al traducir el archivo.',
            icon: 'error',
        });
    });
}

// CHR
function saveSettingParameters(e) {
    console.log('por enviar parametros,', e);
    e.preventDefault();
    // Obtener los valores del formulario
    const cr = localStorage.getItem('selectedCR');
    const descripcion = document.getElementById('archivo').value;
    console.log('descripcion : ', descripcion);
    console.log('cr : ', cr);
    const confimacion = confirm('Estás seguro que desear guardar?');
    console.log('confimacion:', confimacion);
    const data = {cr, descripcion};

    if (confimacion) {
        $.ajax({
            url: 'http://localhost:3500/insModelos',
            type: "POST",
            dataType: "json",
            //data: JSON.stringify({data}),
            data: data,
            success: function (data) {
                console.log(data)
                alert('Se guardaron los datos correctamente');
                //token = data.access_token;
                //guardar token en variable de sesion
                //localStorage.setItem("token", 'Bearer ' + token);
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
    return false;
} 

// funcion para mostrar el modelo en viewer
function mostrarModelo(){
    //mostrar el modelo en el viewer
    var urn = localStorage.getItem('urn');
    launchViewer(urn);
}        

const formulario = $("#formulario");
const agregarPreguntaBtn = $("#agregarPregunta");
const btnpreguntas = $("#btncrearparametros");
let contadorPreguntas = 1;

//traer todos los parámetros de get http://localhost:3500/getParametros
function getParametros(){
    $.ajax({
        url: 'http://localhost:3500/getParametros',
        type: "GET",
        success: function(data){
            console.log(data);
            //almacenar los valores de familia en un json localsotrage
            localStorage.setItem('familias', JSON.stringify(data));
        
        }
    })
}  
    

agregarPreguntaBtn.on("click", function () {
    // Obtener los parámetros de familia del almacenamiento local y convertirlo a un objeto
    const dataString = localStorage.getItem('familias');
    const data = JSON.parse(dataString);

    // Crear un nuevo div para la pregunta
    const divPregunta = $("<div>").addClass("form-group");

    // Crear un select para la pregunta y asignar un nombre único
    const selectPregunta = $("<select>")
        .addClass("form-control select-search")
        .attr("name", "pregunta" + contadorPreguntas); // Nombre único para cada pregunta

    // Obtener las opciones seleccionadas en preguntas anteriores
    const opcionesSeleccionadas = [];
    formulario.find("select").each(function () {
        const seleccion = $(this).val();
        if (seleccion !== null && seleccion !== "") {
            opcionesSeleccionadas.push(seleccion);
        }
    });

    // Agregar la pregunta al div
    divPregunta.append(selectPregunta);

    // Agregar un option vacío (predeterminado) sin selección
    selectPregunta.append($("<option>").text("Seleccione un parámetro"));

    // Agregar los options basados en el objeto data, excluyendo las opciones seleccionadas
    data.forEach(element => {
        if (!opcionesSeleccionadas.includes(element.parametro)) {
            selectPregunta.append($("<option>").text(element.parametro));
        }
    });

    // Agregar el div al formulario
    formulario.append(divPregunta);

    // Incrementar el contador de preguntas
    contadorPreguntas++;
    console.log(opcionesSeleccionadas);
});

//clic en btncrearparametros
btnpreguntas.on("click", function () {
    console.log('holas');
        // Obtener las opciones seleccionadas en preguntas anteriores
        const opcionesSeleccionadas = [];
        formulario.find("select").each(function () {
            const seleccion = $(this).val();
            if (seleccion !== null && seleccion !== "") {
                opcionesSeleccionadas.push(seleccion);
            }
        });
        console.log(opcionesSeleccionadas);

       
        const proyecto = localStorage.getItem('selectedCR');
        const modelo = '30990-ETM-GE-MOD-S-0001.rvt';

        // Mapear los valores originales a objetos JSON
        const resultado = opcionesSeleccionadas.map((parametro, index) => ({
            parametro,
            proyecto,
            modelo,
            orden: (index + 1).toString() // Sumar 1 al índice para obtener el valor de orden
        }));
        obtener_metadata();  
        // Enviar los resultados al servidor put a http://localhost:3500/ins_confProyecto
       /*  try {
            $.ajax({
                url: 'http://localhost:3500/ins_confProyecto',
                type: "PUT",
                data: JSON.stringify(resultado),
                contentType: "application/json",
                success: function (data) {
                    console.log(data);
                    Swal.fire({
                        title: 'Parámetros creados exitosamente',
                        icon: 'success',
                    });

                    obtener_metadata();  
                },
                error: function (xhr, status, error) {
                    console.error('Error en la solicitud AJAX:', error);
                    Swal.fire({
                        title: 'Error en la solicitud',
                        text: 'Ha ocurrido un error al crear los parámetros.',
                        icon: 'error',
                    });
                }
            });
        } catch (error) {
            console.error('Error en la solicitud AJAX:', error);
            Swal.fire({
                title: 'Error en la solicitud',
                text: 'Ha ocurrido un error al crear los parámetros.',
                icon: 'error',
            });
        } */
        
        //redirect a avance
        //window.location.href = '/avance';


    }
);     

// extraer guid de urn
function obtener_guid(){
    $.ajax({
        url: 'https://developer.api.autodesk.com/modelderivative/v2/designdata/'+localStorage.getItem('urn')+'/metadata' ,
        type: "GET",
        headers: {
            Authorization : localStorage.getItem("token")},
        success: function(data){
            var urn = data.data.metadata;
            console.log(urn)
            //buscar dentro del arreglo urn el parámetro name = '{3D}'
            urn.forEach(element => {
                
                //if(element.name == '{3D}' != element.name ){
                if(element.name == '3D-PGIB' != element.name ){    
                    //guardar el valor de guid
                    var guid = element.guid;
                    localStorage.setItem("guid", guid);
                }})
            console.log(localStorage.getItem("guid") );
        }    

    })
}

//obtener metadata
function obtener_metadata(){
    obtener_guid();
    $.ajax({
        //url: 'https://developer.api.autodesk.com/modelderivative/v2/designdata/'+localStorage.getItem('urn')+'/metadata/'+localStorage.getItem('guid')+'/properties?forceget=true' ,
        url: 'https://developer.api.autodesk.com/modelderivative/v2/designdata/'+localStorage.getItem('urn')+'/metadata/'+localStorage.getItem('guid')+'/properties?forceget=true' ,
        type: "GET",
        headers: {
            Authorization : localStorage.getItem("token")},
        success: function(data){
            var mdata = data.data.collection;
            var extract = [];
            
            //ACTIVADO PCH
            console.log(mdata);
            //mostrarme todos los valores donde mdata.name contenga 'Viga Estructural - Rectangular' más algo xxx
             mdata.forEach(element => {
                if(element.name.includes('Viga Estructural - Rectangular')){
                    console.log(element.name);
                }
            }
            ) 


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
                                    var revit_objectid = mdata[i].objectid;
                                    //console.log(count);
                                    //solo si se necesita el Type Name
                                    /*
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
                                    */ 
                                    var id_external = mdata[i].externalId;
                                    //validar que la subfamilia Construction exista
                                    if (mdata[i].properties.Construction === undefined || mdata[i].properties.Construction === null) {
                                        // TODOS LOS VALORES SERAN ''
                                    var id_elemento = '';
                                    var area = '';
                                    var avance = '';
                                    var avance_produc = '';
                                    var campo_frente = '';
                                    var cantidad_avance = '';
                                    var cantidad_produc = '';
                                    var cantidad_total = '';
                                    var des_elemento = '';
                                    var ejecutado = '';
                                    var estado_avance = '';
                                    var factor = '';
                                    var fase = '';
                                    var fech_ejecutada = '';
                                    var fech_produc = '';
                                    var fech_planificada = '';
                                    var h_v = '';
                                    var id_cronograma = '';
                                    var id_partida = '';
                                    var lote = '';
                                    var mod_instalacion = '';
                                    var nivel = '';
                                    var rubro = '';
                                    var sistema = '';
                                    var unidad = '';
                                    var wbs_nivel_1 = '';                                    
                                    
                                    }
                                    else {   
                                        //si mdata[i].properties.Data["COSAPI-ID_ELEMENTO"] es undefined o null será igual a '' sino será igual a mdata[i].properties.Data["COSAPI-ID_ELEMENTO"]
                                    var id_elemento = mdata[i].properties.Construction["COSAPI-ID_ELEMENTO"] === undefined || mdata[i].properties.Construction["COSAPI-ID_ELEMENTO"] === null ? '' : mdata[i].properties.Construction["COSAPI-ID_ELEMENTO"];
                                    var area = mdata[i].properties.Construction["COSAPI-AREA"] === undefined || mdata[i].properties.Construction["COSAPI-AREA"] === null ? '' : mdata[i].properties.Construction["COSAPI-AREA"];
                                    var avance = mdata[i].properties.Construction["COSAPI-AVANCE"] === undefined || mdata[i].properties.Construction["COSAPI-AVANCE"] === null ? '' : mdata[i].properties.Construction["COSAPI-AVANCE"];
                                    var avance_produc = mdata[i].properties.Construction["COSAPI-AVANCE_PRODUC"] === undefined || mdata[i].properties.Construction["COSAPI-AVANCE_PRODUC"] === null ? '' : mdata[i].properties.Construction["COSAPI-AVANCE_PRODUC"];
                                    var campo_frente = mdata[i].properties.Construction["COSAPI-CAMPO_FRENTE"] === undefined || mdata[i].properties.Construction["COSAPI-CAMPO_FRENTE"] === null ? '' : mdata[i].properties.Construction["COSAPI-CAMPO_FRENTE"];
                                    var cantidad_avance = mdata[i].properties.Construction["COSAPI-CANTIDAD_AVANCE"] === undefined || mdata[i].properties.Construction["COSAPI-CANTIDAD_AVANCE"] === null ? '' : mdata[i].properties.Construction["COSAPI-CANTIDAD_AVANCE"];
                                    var cantidad_produc = mdata[i].properties.Construction["COSAPI-CANTIDAD_PRODUC"] === undefined || mdata[i].properties.Construction["COSAPI-CANTIDAD_PRODUC"] === null ? '' : mdata[i].properties.Construction["COSAPI-CANTIDAD_PRODUC"];
                                    var cantidad_total = mdata[i].properties.Construction["COSAPI-CANTIDAD_TOTAL"] === undefined || mdata[i].properties.Construction["COSAPI-CANTIDAD_TOTAL"] === null ? '' : mdata[i].properties.Construction["COSAPI-CANTIDAD_TOTAL"];
                                    var des_elemento = mdata[i].properties.Construction["COSAPI-DESCRIPCION_ELEMENTO"] === undefined || mdata[i].properties.Construction["COSAPI-DESCRIPCION_ELEMENTO"] === null ? '' : mdata[i].properties.Construction["COSAPI-DESCRIPCION_ELEMENTO"];
                                    var ejecutado = mdata[i].properties.Construction["COSAPI-EJECUTADO"] === undefined || mdata[i].properties.Construction["COSAPI-EJECUTADO"] === null ? '' : mdata[i].properties.Construction["COSAPI-EJECUTADO"];
                                    var estado_avance = mdata[i].properties.Construction["COSAPI-ESTADO_AVANCE"] === undefined || mdata[i].properties.Construction["COSAPI-ESTADO_AVANCE"] === null ? '' : mdata[i].properties.Construction["COSAPI-ESTADO_AVANCE"];
                                    var factor = mdata[i].properties.Construction["COSAPI-FACTOR"] === undefined || mdata[i].properties.Construction["COSAPI-FACTOR"] === null ? '' : mdata[i].properties.Construction["COSAPI-FACTOR"];
                                    var fase = mdata[i].properties.Construction["COSAPI-FASE"] === undefined || mdata[i].properties.Construction["COSAPI-FASE"] === null ? '' : mdata[i].properties.Construction["COSAPI-FASE"];
                                    var fech_ejecutada = mdata[i].properties.Construction["COSAPI-FECHA_EJECUTADA"] === undefined || mdata[i].properties.Construction["COSAPI-FECHA_EJECUTADA"] === null ? '' : mdata[i].properties.Construction["COSAPI-FECHA_EJECUTADA"];
                                    var fech_produc = mdata[i].properties.Construction["COSAPI-FECHA_PRODUC"] === undefined || mdata[i].properties.Construction["COSAPI-FECHA_PRODUC"] === null ? '' : mdata[i].properties.Construction["COSAPI-FECHA_PRODUC"];
                                    var fech_planificada = mdata[i].properties.Construction["COSAPI-FECHA_PLANIFICADA"] === undefined || mdata[i].properties.Construction["COSAPI-FECHA_PLANIFICADA"] === null ? '' : mdata[i].properties.Construction["COSAPI-FECHA_PLANIFICADA"];
                                    var h_v = mdata[i].properties.Construction["COSAPI-H/V"] === undefined || mdata[i].properties.Construction["COSAPI-H/V"] === null ? '' : mdata[i].properties.Construction["COSAPI-H/V"];
                                    var id_cronograma = mdata[i].properties.Construction["COSAPI-ID_CRONOGRAMA"] === undefined || mdata[i].properties.Construction["COSAPI-ID_CRONOGRAMA"] === null ? '' : mdata[i].properties.Construction["COSAPI-ID_CRONOGRAMA"];
                                    var id_partida = mdata[i].properties.Construction["COSAPI-ID_PARTIDA"] === undefined || mdata[i].properties.Construction["COSAPI-ID_PARTIDA"] === null ? '' : mdata[i].properties.Construction["COSAPI-ID_PARTIDA"];
                                    var lote = mdata[i].properties.Construction["COSAPI-LOTE"] === undefined || mdata[i].properties.Construction["COSAPI-LOTE"] === null ? '' : mdata[i].properties.Construction["COSAPI-LOTE"];
                                    var mod_instalacion = mdata[i].properties.Construction["COSAPI-MOD_INSTALACION"] === undefined || mdata[i].properties.Construction["COSAPI-MOD_INSTALACION"] === null ? '' : mdata[i].properties.Construction["COSAPI-MOD_INSTALACION"];
                                    var nivel = mdata[i].properties.Construction["COSAPI-NIVEL"] === undefined || mdata[i].properties.Construction["COSAPI-NIVEL"] === null ? '' : mdata[i].properties.Construction["COSAPI-NIVEL"];
                                    var rubro = mdata[i].properties.Construction["COSAPI-RUBRO"] === undefined || mdata[i].properties.Construction["COSAPI-RUBRO"] === null ? '' : mdata[i].properties.Construction["COSAPI-RUBRO"];
                                    var sistema = mdata[i].properties.Construction["COSAPI-SISTEMA"] === undefined || mdata[i].properties.Construction["COSAPI-SISTEMA"] === null ? '' : mdata[i].properties.Construction["COSAPI-SISTEMA"];
                                    var unidad = mdata[i].properties.Construction["COSAPI-UNIDAD"] === undefined || mdata[i].properties.Construction["COSAPI-UNIDAD"] === null ? '' : mdata[i].properties.Construction["COSAPI-UNIDAD"];
                                    var wbs_nivel_1 = mdata[i].properties.Construction["COSAPI-WBS_NIVEL_1"] === undefined || mdata[i].properties.Construction["COSAPI-WBS_NIVEL_1"] === null ? '' : mdata[i].properties.Construction["COSAPI-WBS_NIVEL_1"];


                                    /* sin validaciones
                                    var id_elemento = mdata[i].properties.Data["COSAPI-ID_ELEMENTO"];
                                    var area = mdata[i].properties.Construction["COSAPI-AREA"];
                                    var avance = mdata[i].properties.Construction["COSAPI-AVANCE"];
                                    var avance_produc = mdata[i].properties.Construction["COSAPI-AVANCE_PRODUC"];
                                    var campo_frente = mdata[i].properties.Construction["COSAPI-CAMPO_FRENTE"];
                                    var cantidad_avance = mdata[i].properties.Construction["COSAPI-CANTIDAD_AVANCE"];
                                    var cantidad_produc = mdata[i].properties.Construction["COSAPI-CANTIDAD_PRODUC"];
                                    var cantidad_total = mdata[i].properties.Construction["COSAPI-CANTIDAD_TOTAL"];
                                    var des_elemento = mdata[i].properties.Construction["COSAPI-DESCRIPCION_ELEMENTO"];
                                    var ejecutado = mdata[i].properties.Construction["COSAPI-EJECUTADO"];
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
                                    var wbs_nivel_1 = mdata[i].properties.Construction["COSAPI-WBS_NIVEL_1"]; 
                                    */
                                    
                                    }    

                                    //console.log(cantidad);
                                    //console.log(unidad);
                                    // insertar valores a extract
                                    extract.push({  revit_objectid : revit_objectid,
                                                   
                                                    id_external : id_external,
                                                    id_elemento : id_elemento,
                                                    area : area,
                                                    avance : avance, //el porcentaje
                                                    avance_produc : avance_produc,
                                                    campo_frente : campo_frente,
                                                    cantidad_avance : cantidad_avance, // calculado** = avance * metrado
                                                    cantidad_produc : cantidad_produc,
                                                    cantidad_total : cantidad_total, //esto es metrado
                                                    des_elemento : des_elemento,
                                                    ejecutado : ejecutado,
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
                                                    wbs_nivel_1 : wbs_nivel_1 
                                                });
                                    
                                    //hacer un insert con post
                                    //create function to insert data
                                    /* new_data(revit_objectid, id_external, id_elemento, area, avance, campo_frente, cantidad, des_elemento, ejecutado, estado_avance, factor, fase, fech_ejecutada, fech_planificada, h_v, id_cronograma, id_partida, lote, mod_instalacion, nivel, rubro, sistema, unidad, wbs_nivel_1); */
                                // insertar cada uno de los datos usando post a http://localhost:3500/insertMetrado    
                                $.ajax({
                                    url: 'http://localhost:3500/insertMetrado',
                                    type: 'POST',
                                    data: {
                                        revit_objectid: revit_objectid,
                                        id_external: id_external,
                                        id_elemento: id_elemento,
                                        area: area,
                                        avance: avance,
                                        avance_produc: avance_produc,
                                        campo_frente: campo_frente,
                                        cantidad_avance: cantidad_avance,
                                        cantidad_produc: cantidad_produc,
                                        cantidad_total: cantidad_total,
                                        des_elemento: des_elemento,
                                        ejecutado: ejecutado,
                                        estado_avance: estado_avance,
                                        factor: factor,
                                        fase: fase,
                                        fech_ejecutada: fech_ejecutada,
                                        fech_produc: fech_produc,
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
                                        console.log('se inserto');
                                    }
                                });

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
        }
    })
};

const btnVerModelo = $("#btnVerModelo");
// botón btnVerModelo viewer 7
btnVerModelo.on("click", function () {
   /* launchViewer('dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6dGVzdDJfY2hpbGVfYW50b2ZhZ2FzdGEvMDMtQ1I0MzIwLUJJTS1NT0QtVE9SUkUuQ0FQVEFDSU9OLjQuaWZj');
 


}) */
// Abrir o crear una base de datos llamada 'miBaseDeDatos' con versión 1
var request = indexedDB.open('miBaseDeDatos', 1);

// Manejar el evento de éxito o actualización de la base de datos
request.onupgradeneeded = function(event) {
  var db = event.target.result;

  // Crea una tienda de objetos llamada 'datos' si no existe
  if (!db.objectStoreNames.contains('datos')) {
    db.createObjectStore('datos', { keyPath: 'id' });
  }
};

// Manejar el evento de éxito cuando se abre la base de datos
request.onsuccess = function(event) {
  var db = event.target.result;

  // Iniciar una transacción en la tienda de objetos 'datos' para escritura ('readwrite')
  var transaction = db.transaction(['datos'], 'readwrite');
  var objectStore = transaction.objectStore('datos');

  // Tu objeto de datos
  var data = [
    {
      "id": 18,
      "parametro": "ACTIVIDAD_CRONOGRAMA",
      // ... (otros datos)
    },
    // ... (otros objetos)
  ];

  // Agregar cada objeto a la tienda de objetos
  data.forEach(function(item) {
    objectStore.add(item);
  });

  // Manejar eventos de transacción
  transaction.oncomplete = function() {
    console.log('Datos almacenados con éxito en IndexedDB.');
  };

  transaction.onerror = function(event) {
    console.error('Error al almacenar datos en IndexedDB: ' + event.target.error);
  };
};

// Manejar errores al abrir la base de datos
request.onerror = function(event) {
  console.error('Error al abrir la base de datos: ' + event.target.error);
};


});

// funcion para obtener metadata

