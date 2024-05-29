//document ready
$(document).ready(function(){
// Escucha el evento de cambio en el input
document.getElementById('imageInput').addEventListener('change', function () {
    // Verifica si el usuario ha seleccionado al menos un archivo
    if (this.files && this.files[0]) {
        // Crea una instancia de FileReader para leer el archivo
        var reader = new FileReader();

        // Define lo que sucede cuando se completan las operaciones de lectura del archivo
        reader.onload = function (e) {
            // e.target.result contiene el contenido del archivo en base64
            var base64Result = e.target.result;

            // Opcional: Muestra el resultado en base64
            document.getElementById('base64Output').textContent = base64Result;

            // Opcional: Muestra la imagen usando el resultado en base64
            var imagePreview = document.getElementById('imagePreview');
            imagePreview.src = base64Result;
            imagePreview.style.display = 'block';
        };

        // Lee el archivo como un URL de datos base64
        reader.readAsDataURL(this.files[0]);
    }
});

});