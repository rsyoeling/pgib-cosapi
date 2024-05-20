$(document).ready(function () {

    $.ajaxSetup({ cache: false });

    $(document).click(function () {
        $(".icon-sli").hide("slow");
        $(".relative-icon").find(".icon-sli").slideUp();
        //clickUser
        $(".caret-comp").addClass("fa-caret-right").removeClass("fa-caret-down");
        $(".despligue-datos").slideUp();
        $(".userDiv").removeClass("Active");
    })
    $("body").on("click", ".close-float", function () {
        $(".float-menu").animate({ top: "-=205" }, 500)
    })
    $("body").on("click", ".dpl-menu", function () {
        $(".float-menu").animate({ top: "+=205" }, 500)
    })
    $("body").on("click", ".mn-sb", function (e) {
        if ($(this).hasClass("Active")) {
            e.preventDefault();
        }
    })
    $("body").on("click", ".section-sociedad", function () {
        if ($(this).siblings().is(":visible")) {
            $(this).siblings().hide("slow");
            $(this).find("i").addClass("fa-arrow-circle-right").removeClass("fa-arrow-circle-down");
        } else {
            $(this).siblings().show("slow");
            $(this).find("i").addClass("fa-arrow-circle-down").removeClass("fa-arrow-circle-right");
        }
    })
    $("body").on("click", ".icon-action-sociedad", function (e) {
        $(".relative-icon .icon-sli").hide();
        if ($(this).parents(".relative-icon").find(".icon-sli").is(":visible")) {
            $(this).parents(".relative-icon").find(".icon-sli").slideUp();
        } else {
            $(this).parents(".relative-icon").find(".icon-sli").slideDown();
        }
        e.stopPropagation();
    })
    $("body").on("click", ".state-mod .icon-state", function (e) {
        if ($(this).hasClass("fa-toggle-off")) {
            $(this).addClass("fa-toggle-on");
            $(this).removeClass("fa-toggle-off");
        } else {
            $(this).addClass("fa-toggle-off");
            $(this).removeClass("fa-toggle-on");
        }
        e.stopPropagation();
    })
    $("body").on("click", ".userDiv", function (e) {
        if ($(".despligue-datos").is(":visible")) {
            $(".caret-comp").addClass("fa-caret-right").removeClass("fa-caret-down");
            $(".despligue-datos").slideUp();
            $(this).removeClass("Active");
        } else {
            $(".caret-comp").addClass("fa-caret-down").removeClass("fa-caret-right");
            $(".despligue-datos").slideDown();
            $(this).addClass("Active");
        }
        e.stopPropagation();
    })
    $("body").off("click", ".NuevaSociedad");
    $("body").on("click", ".NuevaSociedad", function () {
        var _html = "<div class='jss540'><div class='textDiv'>Código</div><input type='text' placeholder='Código de la Sociedad' /></div>" +
            "<div class='jss540'><div class='textDiv'>Descripción</div><input type='text' placeholder='Descripción de la Sociedad' /></div>" +
            "<div class='jss540'><div class='textDiv'>Estado</div><div class='state-mod'><i class='fa fa-toggle-off icon-state' aria-hidden='true' ></i></div></div>";
        PopUpExt(($(this).attr("mod") == "true" ? "Modificar" : "Nueva") + " Sociedad", _html, 0, 400, 0, "<div class='btns btnSuc'><i class='fa fa-floppy-o'></i>GUARDAR</div>");
        $(".content-up >input:eq(0)").focus();
    })
    /*
    $("body").off("click", ".NuevaPlantilla");
    $("body").on("click", ".NuevaPlantilla", function () {
        var _html = "<div class='jss540'><div class='textDiv'>Código</div><input type='text' placeholder='Código de la Plantilla' /></div>" +
            "<div class='jss540'><div class='textDiv'>Descripción</div><input type='text' placeholder='Descripción de la Plantilla' /></div>" +
            "<div class='jss540'><div class='textDiv'>Estado</div><div class='state-mod'><i class='fa fa-toggle-off icon-state' aria-hidden='true' ></i></div></div>";
        PopUpExt(($(this).attr("mod") == "true" ? "Modificar" : "Nueva") + " Plantilla", _html, 0, 400, 0, "<div class='btns btnSuc'><i class='fa fa-floppy-o'></i>GUARDAR</div>");
        $(".content-up >input:eq(0)").focus();
    })*/
    $("body").off("click", ".NuevoTipoDato");
    $("body").on("click", ".NuevoTipoDato", function () {
        var _html = "<div class='jss540'><div class='textDiv'>Descripción</div><input type='text' placeholder='Descripción del tipo de dato' /></div>" +
            "<div class='jss540'><div class='textDiv'>Derivado</div><select class='cmb'><option value='0'>Ninguno</option></select></div>" +
            "<div class='jss540'><div class='textDiv'>Estado</div><div class='state-mod'><i class='fa fa-toggle-off icon-state' aria-hidden='true' ></i></div></div>";
        PopUpExt(($(this).attr("mod") == "true" ? "Modificar" : "Nuevo") + " Tipo de Dato", _html, 0, 400, 0, "<div class='btns btnSuc'><i class='fa fa-floppy-o'></i>GUARDAR</div>");
        $(".content-up >input:eq(0)").focus();
    })
    $("body").on("click", ".btnCanc,.closePopUp", function () {
        CerrarModal();
    })
    /*$("body").on("click", ".remove-link", function (e) {
        var table = $(this).parents("table").find("tbody");
        $(this).parents("tr").fadeOut(function () {
            $(this).remove();
            if (table.find("tr").length == 0) {
                if (table.parents(".row").find(".empty-table").length > 0) { table.parents(".row").find(".empty-table").show("slow") } else {
                    table.parents(".row").append("<div class='empty-table'><img src='/Content/images/empty.png' /><h2>!Empieza a agregar tus primeros registros!</h2><span>Presione el botón de <b>Nuevo</b> para ingresar un nuevo registro.</span><div>");
                }
                table.parents(".linksociedad").fadeOut(function () {
                    $(this).remove()
                    if ($(".linksociedad").length == 0) { $(".empty-table").show("slow") }
                });
            }
        })
        e.stopPropagation();
    })*/
    $("body").on("click", "li .remove-link", function (e) {
        $(this).parents("li").fadeOut(function () {
            if ($(this).hasClass("Active")) {
                $(".form-data-content").hide();
                $(".empty-table:eq(0)").show("slow");
            }
            $(this).remove();
        })
        e.stopPropagation();
    })
    $("body").on("click", ".ulContent ul ul li", function (e) {
        $(this).find("input").click();
        e.stopPropagation();
    })
    $("body").on("click", ".ulContent ul ul li input", function (e) {
        e.stopPropagation();
    })

    /*
    $("body").on("click", ".wsCamp li,.masterCamp li", function () {
        $(this).parents("ul").find("li").removeClass("Active");
        $(this).addClass("Active");
        //Funcionalidad
        $(".section-create-table").hide();
        if ($(this).parents(".treeCamp").hasClass("wsCamp")) {
            $(".empty-table:eq(0)").hide();
            $(".masterCamp li").removeClass("Active");
            $(".section-create-camp").hide();
            $(".content-mod").show(function () {
                $(this).scrollTop(0);
            });
            $(".content-mod").css("display", "grid");
            $(".empty-table:eq(1)").show("slow");
        } else {
            $(".empty-table:eq(1)").hide();
            $(".section-create-camp").show(function () {
                $(this).scrollTop(0);
            });
            $(".section-create-camp").css("display", "grid");
        }
        $(".delFormData").show();
        $(".ctp-23:visible input:not(disabled)").eq(0).focus();
    })

    $("body").on("click", ".wsCamp .plus-create,.masterCamp .plus-create,li .icon-sli .check-link", function () {
        //Funcionalidad
        $(".empty-table").hide();
        if ($(this).parents(".treeCamp").hasClass("wsCamp")) {
            $(".content-mod,.section-create-camp").hide();
            $(".wsCamp li").removeClass("Active");
            $(".section-create-table").show(function () {
                $(this).scrollTop(0);
            })
            $(".section-create-table").css("display", "inline");
        } else {
            $(".masterCamp li").removeClass("Active");
            $(".section-create-camp").show(function () {
                $(this).scrollTop(0);
            });
            $(".section-create-camp").css("display", "grid");
        }
        $(".delFormData").hide();
        $(".ctp-23:visible input:not(disabled)").eq(0).focus();
        //        
        $(".lisk-dk .icon-sli").hide();
        $(".lisk-dk .li").removeClass("Active")
        $(this).parents("li").addClass("Active");
    })
    $("body").on("click", ".form-data-content .btnCanc", function () {
        $(this).parents(".form-data-content").hide();
        if ($(this).parents(".form-data-content").hasClass("section-create-table")) {
            $(".wsCamp li").remove("Active");
            $(".empty-table:eq(0)").show("slow");
        } else {
            $(".masterCamp li").remove("Active");
            $(".empty-table:eq(1)").show("slow");
        }
    })
    $("body").on("click", "#btnAgregarTable", function () {
        $(".wsCamp .lisk-dk").append("<li class='Active'>101008 - (" + $(this).parents(".form-data-content").find("input[name='DescripcionTabla']").val() + ")</li>");
        $(".section-create-table").hide();
        $(".content-mod,.empty-table:eq(1)").show("slow");
        $(".content-mod").css("display", "grid");
    })
    $("body").on("click", "#btnAgregarCampo", function () {
        $(".ulContent .sup li").each(function (k, e) {
            if ($(e).find("input").is(":checked")) {
                $("#TablaRegistroCampos tbody").append("<tr class='ntr' draggable='false'>" +
                    "<td>" + $(e).parents(".sup").find("div:eq(0) span").html() + "</td>" +
                    "<td>" + $(e).find("span").html() + "</td>" +
                    "<td class='center-cl'>" +
                    "<div class='state-mod state-obl'>" +
                    "<i class='fa fa-toggle-off icon-state' aria-hidden='true'></i>" +
                    "</div>" +
                    "</td>" +
                    "<td class='center-cl'>" +
                    "<div class='state-mod state-habil'>" +
                    "<i class='fa fa-toggle-off icon-state' aria-hidden='true'></i>" +
                    "</div>" +
                    "</td>" +
                    "<td class='center-cl'><div class='check-link chk-switch'><a><i class='fa fa-trash remove-link' tooltips='Eliminar Campo' aria-hidden='true'></i></a></div></td>" +
                    "</tr>");
                $("#TablaRegistroCampos tbody .ntr").show("slow");
                $(e).fadeOut(function () {
                    if ($(this).parents(".sup").find("li").length == 1) {
                        $(this).parents(".sup").remove();
                    }
                    $(this).remove();
                });
                $(".row").find(".empty-table").hide();
            }
        })        
    })*/
   
    $("body").off("mouseenter", "i");
    $("body").on("mouseenter", "i", function () {
        if ($(this).attr("tooltips") != undefined && $(this).attr("tooltips") != "") {
            var coordenadas = $(this).offset();
            $("body").append("<div class='tooltips' style='display:none'>" + $(this).attr("tooltips") + "</div>");
            $(".tooltips").offset({ top: coordenadas.top - 36, left: coordenadas.left - 20 });
            $(".tooltips").show("fast");
        }
    })
    $("body").off("mouseleave", "i");
    $("body").on("mouseleave", "i", function () {
        $(".tooltips").remove();
    })
    var TablaRegistroDetalle = document.getElementById('TablaRegistroDetalle');
    if (TablaRegistroDetalle != null) {
        new Sortable(TablaRegistroDetalle, {
            animation: 150,
            ghostClass: 'blue-background-class'
        });
    }
})
