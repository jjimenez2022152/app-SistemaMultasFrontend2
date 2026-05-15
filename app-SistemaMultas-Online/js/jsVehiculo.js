function validarGuardado() {

    let respuesta = "";

    if ($("#id_vehiculo").val().trim() == "")
        respuesta += "\nID Vehiculo";

    if ($("#placa").val().trim() == "")
        respuesta += "\nPlaca";

    if ($("#marca").val().trim() == "")
        respuesta += "\nMarca";

    if ($("#id_conductor").val().trim() == "")
        respuesta += "\nID Conductor";

    if (respuesta != "") {

        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);

        return false;
    }

    return true;
}

function validarActualizacion() {

    let respuesta = "";

    if ($("#placa").val().trim() == "")
        respuesta += "\nPlaca";

    if ($("#marca").val().trim() == "")
        respuesta += "\nMarca";

    if ($("#id_conductor").val().trim() == "")
        respuesta += "\nID Conductor";

    if (respuesta != "") {

        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);

        return false;
    }

    return true;
}