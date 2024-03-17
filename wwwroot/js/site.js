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

function enviarFormulario(){
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
        nivelElement.trim() == '' ||
        gradoElement.trim() == ''
    ) {
        $('#FormularioNoCompletadoModal').modal('show');

    //    resetEfectoBtnCrearPersona();
    } else {

        let datos = {
            nombre: nombreElement,
            grado: gradoElement,
            nivel: nivelElement,
            correo: correoElement,
            celular: celularElement,
            mensaje: mensajeElement,
        }
        console.log(response);
        $('#FormularioEnviadoCorrectamenteModal').modal('show');
        $("#nombre").val("");
        $("#correo").val("");
        $("#celular").val("");
        $("#mensaje").val("");
        $("#nivel").val("");
        $("#grado").val("");
        //$.ajax({
        //    type: 'POST',
        //    url: 'https://localhost:7217/Twilio',
        //    contentType: 'application/json',
        //    data: JSON.stringify(datos),
        //    success: function (response) {
        //        console.log(response);
        //        $('#FormularioEnviadoCorrectamenteModal').modal('show');
        //        $("#nombre").val("");
        //        $("#correo").val("");
        //        $("#celular").val("");
        //        $("#mensaje").val("");
        //        $("#nivel").val("");
        //        $("#grado").val("");

        //    },
        //    error: function (error) {
        //        console.error(error);
        //    }
        //});

    }
};

function cerrarModalFormulario(){
    $('#FormularioEnviadoCorrectamenteModal').modal('hide');

}

function cerrarModalFormularioNoCompletado() {
    $('#FormularioNoCompletadoModal').modal('hide');

};
