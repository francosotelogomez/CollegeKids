$(document).ready(function () {
    console.log("ready!");
});
    function actualizarGrados() {
        var nivelSelect = document.getElementById("nivel");
    var gradoSelect = document.getElementById("grado");

    // Restablece el segundo select al placeholder
    gradoSelect.selectedIndex = 0;
    gradoSelect.disabled = (nivelSelect.value === "");

    // Restablece el segundo select al placeholder
    gradoSelect.selectedIndex = 0;
    // Oculta todas las opciones de grado
    for (var i = 0; i < gradoSelect.options.length; i++) {
        gradoSelect.options[i].style.display = "none";
        }

    // Muestra las opciones correspondientes al nivel seleccionado
    if (nivelSelect.value === "inicial") {
            for (var i = 0; i < 4; i++) {
        gradoSelect.options[i].style.display = "block";
            }
        } else if (nivelSelect.value === "primaria") {
            for (var i = 4; i < gradoSelect.options.length; i++) {
        gradoSelect.options[i].style.display = "block";
            }
        }
    }

function enviarFormulario() {
    let nombreElement = $("#nombre").val();
    let correoElement = $("#correo").val();
    let celularElement = $("#celular").val();
    let mensajeElement = $("#mensaje").val();
    let nivelElement = $("#nivel").val();
    let gradoElement = $("#grado").val();

    if (
        nombreElement.trim() == '' ||
        correoElement.trim() == '' ||
        celularElement.trim() == '' ||
        mensajeElement.trim() == '' ||
        (nivelElement == '' || nivelElement == null) ||
        (gradoElement == '' || gradoElement == null)
    ) {
        $('#FormularioNoCompletadoModal').modal('show');
    } else {
        // Crear objeto con los datos del formulario
        let formularioData = {
            Nombre: nombreElement,
            Correo: correoElement,
            Celular: celularElement,
            DetalleConsulta: mensajeElement,
            Seccion: nivelElement,
            Grado: gradoElement
        };

        // Enviar datos al servidor mediante AJAX
        $.ajax({
            url: '/Consultas/Registrar',  // URL del controlador
            type: 'POST',
            data: formularioData,
            success: function (response) {
                if (response.success) {
                    alert("Consulta registrada correctamente.");
                    // Limpiar formulario si es necesario
                    $('#formularioConsultas')[0].reset();
                } else {
                    alert("Hubo un problema al registrar la consulta.");
                }
            },
            error: function () {
                alert("Error en la comunicación con el servidor.");
            }
        });
    }
}


function cerrarModalFormulario(){
    $('#FormularioEnviadoCorrectamenteModal').modal('hide');

}

function cerrarModalFormularioNoCompletado() {
    $('#FormularioNoCompletadoModal').modal('hide');

};
