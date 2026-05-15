function validarGuardado()
{
    let respuesta = "";

    if ($("#id_Conductor").val().trim() == "")
        respuesta += "\n id_Conductor";

    if ($("#nombre").val().trim() == "")
        respuesta += "\n nombre";

    if ($("#dpi").val().trim() == "")
        respuesta += "\n dpi";


    if (respuesta != "")
    {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false;
    }

    return true;
}

function validarActualizacion()
{
    let respuesta = "";

    if ($("#id_Conductor").val().trim() == "")
        respuesta += "\n id_Conductor";

    if ($("#nombre").val().trim() == "")
        respuesta += "\nnombre";

    if ($("#dpi").val().trim() == "")
        respuesta += "\ndpi";


    if (respuesta != "")
    {
        alert("Los siguientes campos no pueden quedar vacíos:" + respuesta);
        return false;
    }

    return true;
}