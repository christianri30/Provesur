var BaseUrl;


$(function () {
    //axiosCon("GET", "/Dashboard/getBaseUrl", loadInit);
    loadTable();
});
//function loadInit(r) {
//    BaseUrl = r.data.paramSection;
//    axiosCon("GET", BaseUrl + "socionegocio?t=p", cargarTabla)
//}
function loadTable() {
    axiosCon("GET", "/socionegocio/GetAll?t=p", cargarTabla)
}
function cargarTabla(r) {
    console.log(r);
    let datos = r.data.data;
    let cols = ["numeroDocumento", "razonSocial", "nombreComercial", "usuarioModificacion", "fechaModificacion"];
    loadDataTable(cols, datos, "socioNegocioId", "tbDatos", cadButtonOptions(), [[1, 'asc']]);
}
function eliminar(id) {
    Swal.fire({
        title: 'Desea eliminar este registro?',
        text: 'No podra recuperar los datos eliminados.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!'
    }).then((result) => {
        if (result.value) {
            axiosCon("DELETE", BaseUrl + "socionegocio/" + id, loadTable);
        } else {
            Swal.fire('Cancelado', 'No se eliminó el registro', 'error');
        }
    });
}

function btnActions(el, opt) {
    let id = el.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
    console.log(id);
    action = opt;
    switch (opt) {
        case "new":
            //cleanBP();
            //showForm();
            //$("#titleForm").html("Nueva Pregunta");
            break;
        case "view":
            window.location.href = "/proveedor/detalle/" + id;
            break;
        case "edit":
            window.location.href = "/proveedor/editar/" + id;
            break;
        case "delete":
            eliminar(id);
            //tbDatos.columns.adjust().draw();
            break;
        default:
            break;
    }
}