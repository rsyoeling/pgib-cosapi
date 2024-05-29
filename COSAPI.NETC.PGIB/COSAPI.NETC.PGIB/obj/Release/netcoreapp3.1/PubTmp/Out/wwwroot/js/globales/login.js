$(document).ready(function () {
    //get http://localhost:3500/proyectos
    function leerProyectos () {
        
        $.ajax({
            url: 'http://localhost:3500/proyectos',
            type: "GET",
            dataType: "json",
            success: function (elements) {
                
                //añadir los proyectos al select
                var select = document.getElementById('SelectProyectos');
                for (var i = 0; i < elements.length; i++) {
                    var opt = elements[i].cr;
                    var el = document.createElement("option");
                    el.textContent = opt;
                    el.value = opt;
                    select.appendChild(el);
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
    leerProyectos();
    //click en btnIngresar
    $('#btnIngresar').click(function () {
        //get http://localhost:3500/proyectos
        //var proyecto = document.getElementById('SelectProyectos').value;
        var usuario = document.getElementById('Usuario').value;
        var pass = document.getElementById('Password').value;
        $.ajax({
            url: 'http://localhost:3500/login2',
            type: "POST",
            dataType: "json",
            data: {
                usuario: usuario,
                contraseña: pass 
            },
            success: function (elements) {
                //validar que elementos no sea null
                if (elements.length > 0) {
                    //guardar en localstorage usuario y proyecto
                    // si rol es 'A' redirigir a /admuser
                    if (elements[0].rol == 'A') {
                        localStorage.setItem('usuario', elements[0].nombre);
                        //redireccionar a la pagina de proyectos
                        window.location.href = "http://localhost:3001/admuser";
                    }
                    if (elements[0].rol == 'U') {
                        localStorage.setItem('usuario', elements[0].nombre);
                        //redireccionar a la pagina de proyectos
                        window.location.href = "http://localhost:3001/proyectos";
                    }
                    
                } else {
                    //sweealert
                    swal({
                        title: "Error",
                        text: "Usuario o contraseña incorrectos",
                        type: "error",
                        confirmButtonText: "Aceptar"
                    });
                }
               
            } 
            
        })
        // si usuario y password = admin entonces redirigir a http://srvdesaweb:3001/parametros
        /* if (usuario == 'admin' && pass == 'admin') {
            //guardar en localstorage usuario y proyecto
            localStorage.setItem('usuario', usuario);
            localStorage.setItem('proyecto', 'CRVENTAS');
            //redireccionar a la pagina de proyectos
            window.location.href = "http://srvdesaweb:3001/modelos";
        } else {
            //sweealert
            swal({
                title: "Error",
                text: "Usuario o contraseña incorrectos",
                type: "error",
                confirmButtonText: "Aceptar"
            });
        } */
    });    
});