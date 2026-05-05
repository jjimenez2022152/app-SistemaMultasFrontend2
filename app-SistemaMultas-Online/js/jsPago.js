function validarGuardado() {
    let respuesta = "";

    if ($("#id_pago").val().trim() == "")
        respuesta += "\nID Pago";

    if ($("#fecha_pago").val().trim() == "")
        respuesta += "\nFecha Pago";

    if ($("#monto").val().trim() == "")
        respuesta += "\nMonto";

    if ($("#id_infraccion").val().trim() == "")
        respuesta += "\nID Infracción";

    if (respuesta != "") {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false;
    }

    return true;
}

function validarActualizacion() {
    let respuesta = "";

    if ($("#fecha_pago").val().trim() == "")
        respuesta += "\nFecha Pago";

    if ($("#monto").val().trim() == "")
        respuesta += "\nMonto";

    if ($("#id_infraccion").val().trim() == "")
        respuesta += "\nID Infracción";

    if (respuesta != "") {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false;
    }

    return true;
}