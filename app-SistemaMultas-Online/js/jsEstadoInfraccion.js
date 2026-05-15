function validarGuardado() {
    let respuesta = "";

    if ($("#id_estado").val().trim() == "")
        respuesta += "\nID Estado";

    if ($("#descripcion").val().trim() == "")
        respuesta += "\nDescripción";

    if (respuesta != "") {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false;
    }

    return true;
}

function validarActualizacion() {
    let respuesta = "";

    if ($("#descripcion").val().trim() == "")
        respuesta += "\nDescripción";

    if (respuesta != "") {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false;
    }

    return true;
}