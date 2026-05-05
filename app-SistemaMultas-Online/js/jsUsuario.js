function validarGuardado() {
    let respuesta = "";

    if ($("#id_usuario").val().trim() == "")
        respuesta += "\nID Usuario";

    if ($("#username").val().trim() == "")
        respuesta += "\nUsername";

    if ($("#password").val().trim() == "")
        respuesta += "\nPassword";

    if ($("#id_agente").val().trim() == "")
        respuesta += "\nID Agente";

    if (respuesta != "")
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
    else
        document.forms[0].submit();
}

function validarActualizacion() {
    let respuesta = "";

    if ($("#username").val().trim() == "")
        respuesta += "\nUsername";

    if ($("#password").val().trim() == "")
        respuesta += "\nPassword";

    if ($("#id_agente").val().trim() == "")
        respuesta += "\nID Agente";

    if (respuesta != "") {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false;  // <--- Esto evita que se envíe el formulario
    }

    return true;  // <--- Esto permite enviar el formulario
}