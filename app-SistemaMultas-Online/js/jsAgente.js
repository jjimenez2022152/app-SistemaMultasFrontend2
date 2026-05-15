// =========================
// VALIDACIONES AGENTE
// =========================

function validarGuardadoAgente() {

    let respuesta = "";

    if ($("#id_agente").val().trim() == "")
        respuesta += "\nID Agente";

    if ($("#nombre").val().trim() == "")
        respuesta += "\nNombre";

    if (respuesta != "") {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false;
    }

    return true;
}

function validarActualizacionAgente() {

    let respuesta = "";

    if ($("#nombre").val().trim() == "")
        respuesta += "\nNombre";

    if (respuesta != "") {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false;
    }

    return true;
}