// =========================
// VALIDACIONES SANCION
// =========================

function validarGuardadoSancion() {

    let respuesta = "";

    if ($("#id_sancion").val().trim() == "")
        respuesta += "\nID Sanción";

    if ($("#descripcion").val().trim() == "")
        respuesta += "\nDescripción";

    if ($("#monto").val().trim() == "")
        respuesta += "\nMonto";

    if (respuesta != "") {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false;
    }

    return true;
}

function validarActualizacionSancion() {

    let respuesta = "";

    if ($("#descripcion").val().trim() == "")
        respuesta += "\nDescripción";

    if ($("#monto").val().trim() == "")
        respuesta += "\nMonto";

    if (respuesta != "") {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false;
    }

    return true;
}