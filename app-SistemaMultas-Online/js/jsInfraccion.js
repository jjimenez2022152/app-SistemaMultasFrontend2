function validarGuardado() {
    let respuesta = "";

    if ($("#id_infraccion").val().trim() == "")
        respuesta += "\nID Infracción";

    if ($("#fecha").val().trim() == "")
        respuesta += "\nFecha";

    if ($("#lugar").val().trim() == "")
        respuesta += "\nLugar";

    if ($("#id_vehiculo").val().trim() == "")
        respuesta += "\nID Vehículo";

    if ($("#id_agente").val().trim() == "")
        respuesta += "\nID Agente";

    if ($("#id_sancion").val().trim() == "")
        respuesta += "\nID Sanción";

    if ($("#id_estado").val().trim() == "")
        respuesta += "\nID Estado";

    if (respuesta != "") {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false; // 🚫 BLOQUEA envío
    }

    return true; // ✅ PERMITE envío
}

function validarActualizacion() {
    let respuesta = "";

    if ($("#fecha").val().trim() == "")
        respuesta += "\nFecha";

    if ($("#lugar").val().trim() == "")
        respuesta += "\nLugar";

    if ($("#id_vehiculo").val().trim() == "")
        respuesta += "\nID Vehículo";

    if ($("#id_agente").val().trim() == "")
        respuesta += "\nID Agente";

    if ($("#id_sancion").val().trim() == "")
        respuesta += "\nID Sanción";

    if ($("#id_estado").val().trim() == "")
        respuesta += "\nID Estado";

    if (respuesta != "") {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false;
    }

    return true;
}