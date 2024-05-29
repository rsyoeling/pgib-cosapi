/* cuando el documento termine de cargar */
$(document).ready(function () {
//ejecutar al dar clic en btnGenerarInfo
document.getElementById("btnGenerarInfo").onclick = function () {
  
    // post ajax a http://10.100.94.14/valorizacioneq/api/Maestro/EnviarDatos , pasando un json
    $.ajax({
        type: "POST",
        url: "http://10.100.94.14/valorizacioneq/api/Maestro/EnviarDatos",
        data: JSON.stringify([
            {
                "DOC_DATE": "20240131",
                "POST_DATE": "20240131",
                "CANT": "9",
                "UNITH": "H",
                "KOSTL": "C2135E0022",
                "CLACT": "HM/NOR",
                "PSPNR": "C.31250/0.BB"
            }    
           
        ]),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            //mostrar todos el resultado en la alerta con swal
            swal("Respuesta", JSON.stringify(data), "success");
        },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });
};



});