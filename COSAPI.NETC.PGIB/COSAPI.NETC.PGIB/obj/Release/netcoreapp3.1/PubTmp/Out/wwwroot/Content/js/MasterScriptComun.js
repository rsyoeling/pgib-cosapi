function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};

function CompletaExiste(pRespuesta) {
    if (pRespuesta != "") {
        pRespuesta += "<br/>";
    }

    return pRespuesta;
}
$(document).ready(function () {
    $.ajaxSetup({
        cache: false,
        beforeSend: function (xhr) {           
            ShowLoading();
        },
        complete: function () {
            HideLoading();
        }
    });
}); 

$(window).load(function () {
    $(".Tabla-u-content").height($(window).height() - $(".header-cab").height() + 7);
    $(".section-create-camp").height($(window).height() - $(".header-cab").height() + 7);
    $(".bg_body").css("top", $(".header-cab").height());
    $(".LoadBlock").fadeOut("fast");
});
function BuscarDatos() {
    $(".PanelNone").hide();
    $(".PanelSearch").show("slow");
}
function PopUpExt(Cabecera, Contenido, TipoBoton, width, height, btn) {
    $(".MasterBlock,.modPopUp").remove();

    $("body").append("<div class='MasterBlock'></div><div class='modPopUp'><div class='supBar'><span>" + Cabecera + "</span><i class='fa fa-times closePopUp'></i></div><div class='content-up' style='" + (height == 0 || height == undefined ? "" : "height:" + height + "px") + "'>" + Contenido + "</div>" +
        "<div class='inBar'>" + (TipoBoton == 0 ? btn : TipoBoton == 1 ? "<div class='btns' id='btnSubir'>SUBIR ARCHIVO</div>" : "") + "<div class='btns btnCanc'>CERRAR</div></div></div>");
    if (width == undefined || width == 0) { } else { $(".modPopUp").css({ "width": width + "px" }) };
    $(".modPopUp").css({ "margin-left": "-" + ($(".modPopUp").width() / 2) + "px" });
}

function PopUpExt2(Cabecera, Contenido, TipoBoton, width, height, btn) {
    //$(".MasterBlock").remove();
    $(".MasterBlock,.modPopUp").remove();

    $("body").append("<div class='MasterBlock'></div><div class='modPopUp'><div class='supBar'><span>" + Cabecera + "</span><i class='fa fa-times closePopUp'></i></div><div class='content-up' style='" + (height == 0 || height == undefined ? "" : "height:" + height + "px") + "'>" + Contenido + "</div>" +
        "<div class='inBar'>" + (TipoBoton == 0 ? btn : TipoBoton == 1 ? "<div class='btns' id='btnSubir'>SUBIR ARCHIVO</div>" : "") + "</div></div>");
    if (width == undefined || width == 0) { } else { $(".modPopUp").css({ "width": width + "px" }) };
    $(".modPopUp").css({ "margin-left": "-" + ($(".modPopUp").width() / 2) + "px" });
}

function CerrarModal() {
    $(".MasterBlock,.modPopUp").remove();
    $(".MasterBlock").remove();
}
function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}
function allowDrop(ev) {
    ev.preventDefault();
}
function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    ev.target.appendChild(document.getElementById(data));
    e.dataTransfer.setData('text/html', this.innerHTML);
}
//function AceptarDatos() {
//    $(".PanelSearch").hide();
//    $(".PanelNone").show("slow");
//}
//function CancelDatos() {
//    $(".PanelSearch").hide();
//    $(".PanelNone").show("slow");
//}


function allowAlphaNumericSpace(e) {
    var code = ('charCode' in e) ? e.charCode : e.keyCode;
    if (!(code == 32) && // space
        !(code == 64) && // arroba
        !(code == 46) && // punto
        !(code > 47 && code < 58) && // numeric (0-9)
        !(code > 64 && code < 91) && // upper alpha (A-Z)
        !(code > 96 && code < 123)) { // lower alpha (a-z)
        e.preventDefault();
    }
}

function allowAlphaNumericSpaceEmail(e) {
    var code = ('charCode' in e) ? e.charCode : e.keyCode;
    if (!(code == 32) && // space
        !(code == 64) && // arroba
        !(code == 46) && // punto
        !(code > 47 && code < 58) && // numeric (0-9)
        !(code > 64 && code < 91) && // upper alpha (A-Z)
        !(code > 96 && code < 123)) { // lower alpha (a-z)
        e.preventDefault();
    }
}

function allowAlphaNumeric(e) {
    var code = ('charCode' in e) ? e.charCode : e.keyCode;
    if (!(code > 47 && code < 58) && // numeric (0-9)
        !(code > 64 && code < 91) && // upper alpha (A-Z)
        !(code > 96 && code < 123)) { // lower alpha (a-z)
        e.preventDefault();
    }
}

function allowAlpha(e) {
    var code = ('charCode' in e) ? e.charCode : e.keyCode;
    if (!(code > 64 && code < 91) && // upper alpha (A-Z)
        !(code > 96 && code < 123)) { // lower alpha (a-z)
        e.preventDefault();
    }
}

function allowAlphaSpace(e) {
    var code = ('charCode' in e) ? e.charCode : e.keyCode;
    if (!(code == 32) && // space
        !(code > 64 && code < 91) && // upper alpha (A-Z)
        !(code > 96 && code < 123)) { // lower alpha (a-z)
        e.preventDefault();
    }
}

function allowNumeric(e) {
    var code = ('charCode' in e) ? e.charCode : e.keyCode;
    if (!(code > 47 && code < 58)) // numeric (0-9)
    {
        e.preventDefault();
    }
}

function allowNumericDecimal(e) {
    var code = ('charCode' in e) ? e.charCode : e.keyCode;
    if (!(code == 46) && // point decimal
        !(code > 47 && code < 58)) // numeric (0-9)
    {
        e.preventDefault();
    }
}


function replaceAll(str, find, replace) {
    return str.replace(new RegExp(find, 'g'), replace);
}


function ConvertoToDate(pFecha) {
    //Dividimos la fecha primero utilizando el espacio para obtener solo la fecha y el tiempo por separado
    var splitDate = pFecha.split(" ");
    var date = splitDate[0].split("/");
    var time = splitDate[1].split(":");

    // Obtenemos los campos individuales para todas las partes de la fecha
    var dd = date[0];
    var mm = date[1] - 1;
    var yyyy = date[2];
    var hh = time[0];
    var min = time[1];
    var ss = time[2];

    // Creamos la fecha con Javascript
    var fecha = new Date(yyyy, mm, dd, hh, min, ss);


    return fecha;
}