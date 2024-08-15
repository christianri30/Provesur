var idxContacto = 0;
var idxBanco = 0;
var BaseUrl;
var type = "";
var dbimg64 = "";
var nameimg = "";
var fileBase64 = "";
var nameFileRecurso = "";
var inputFile = document.getElementById("fileImage");

function loadInit(r) {
    BaseUrl = r.data.paramSection;
    loadSelectMG(BaseUrl + "GlobalMaestro", "cboTipoPersona", "SociosNegocios.Maestro", "TipoPersona", null);
    loadSelectMG(BaseUrl + "GlobalMaestro", "cboTipoDocumento", "SociosNegocios.Maestro", "TipoDocumento", null);
    loadSelectMG(BaseUrl + "GlobalMaestro", "cboBanco", "SociosNegocios.Banco", "Banco", null);
    loadSelectMG(BaseUrl + "GlobalMaestro", "cboMoneda", "Global.Moneda", "Moneda", null);
    loadSelect("cboCondicionPago", "globalCondicionPago", "condicionPagoId", "descripcion", null);
}
$(function () {
    axiosCon("GET", "/Dashboard/getBaseUrl", loadInit)
    $("#btnSunat").on("click", function () {
        switch ($("#cboTipoDocumento").val()) {
            case "0002":
                type = "reniec";
                break;
            case "0004":
                type = "sunat";
                break;
            default:
                type = "";
                break;
        }
        if (type != "") {
            axiosCon("GET", BaseUrl + type + "/" + $("#txtDocumento").val().trim(), showConsulta);
        }
    });
    function showConsulta(m) {
        let responseJson = m.data;
        switch (type) {
            case "sunat":
                console.log(responseJson);
                $("#txtRazonSocial").val(responseJson.razonSocial);
                $("#txtDireccion").val(responseJson.direccion.trim() + " " + responseJson.distrito.trim() + " " + responseJson.provincia.trim());
                break;
            case "reniec":
                $("#txtRazonSocial").val(responseJson.nombres + " " + responseJson.apellidoPaterno + " " + responseJson.apellidoMaterno);
                break;
            default:
                break;
        }
    }

    $("#btnAgregarContacto").on("click", function () {
        if (validate("vContact")) {
            addRowContact();
        }
    });

    $("#btnAgregarCtaBanco").on("click", function () {
        if (validate("vBank")) {
            addRowBank();
        }
    });

    $("#btnAgregarContrato").on("click", function () {
        if (validate("vContract")) {
            addRowContract();
        }
    });
    $("#btnGrabar").on("click", async function () {
        if (validate("validate")) {
            axios.post(BaseUrl + 'socionegocio', createPostObject()).then(res => {
                if (res.data.resultado) {
                    Swal.fire('Registro exitoso!', 'Los datos han sido ingresado correctamente al sistema.', 'success');
                    setTimeout(function () {
                        window.location.href = "/proveedor"
                    }, 1800);
                }
                else {
                    Swal.fire('Error', res.data.data, 'error');
                }
            }).catch(err => {
                Swal.fire('Error', err.toString(), 'error');
            });
        }
    });
});

function examinar() {
    inputFile.click();
}
function addRowContract() {
    let cad = "";
    cad += "<tr class='rowDetContratos'>";
    cad += '<td class="">' + $("#txtDescripcionContrato").val() + '</td>';
    cad += "<td class=''><div class='col-1'><button class='btn btn-info btn-xs 'onclick='generaDescargablePdf(this);'><i class='ri-file-download-fill fs-12' data-name='" + $("#lblArchivo")[0].dataset.name + "' data-id='" + $("#lblArchivo")[0].dataset.id + "' ></i></button></td>";
    cad += '<td class="">' + cadButton("Contrato") + '</td>';
    cad += "</tr>";
    document.getElementById("tbContracts").innerHTML += cad;
}
function addRowBank() {
    let cad = "";
    if (idxBanco == 0) {
        cad += "<tr class='rowDetBancos'>";
        cad += '<td class="">' + $("#txtDescripcionBanco").val() + '</td>';
        cad += '<td class="" data-id="' + $("#cboBanco").val() + '">' + $("#cboBanco option:selected").text() + '</td>';
        cad += '<td class="" data-id="' + $("#cboMoneda").val() + '">' + $("#cboMoneda option:selected").text() + '</td>';
        cad += '<td class="">' + $("#txtCtaBancaria").val() + '</td>';
        cad += '<td class="">' + $("#txtCtaInterBancaria").val() + '</td>';
        cad += '<td class="">' + $("#txtCtaDetraccion").val() + '</td>';
        cad += '<td class="">' + cadButton("Banco") + '</td>';
        cad += "</tr>";
        document.getElementById("tbBanks").innerHTML += cad;
    }
    else {

        cad += '<td class="">' + $("#txtDescripcionBanco").val() + '</td>';
        cad += '<td class="" data-id="' + $("#cboBanco").val() + '">' + $("#cboBanco option:selected").text() + '</td>';
        cad += '<td class="" data-id="' + $("#cboMoneda").val() + '">' + $("#cboMoneda option:selected").text() + '</td>';
        cad += '<td class="">' + $("#txtCtaBancaria").val() + '</td>';
        cad += '<td class="">' + $("#txtCtaInterBancaria").val() + '</td>';
        cad += '<td class="">' + $("#txtCtaDetraccion").val() + '</td>';
        cad += '<td class="">' + cadButton("Banco") + '</td>';
        $("#tbBanks")[0].rows[idxBanco - 1].innerHTML = cad;
    }
    cleanControl("bank");
    idxBanco = 0;
}

function addRowContact() {
    let cad = "";
    if (idxContacto == 0) {
        cad += "<tr class='rowDetContacto'>";
        cad += '<td class="">' + $("#txtNomContacto").val() + '</td>';
        cad += '<td class="">' + $("#txtCargo").val() + '</td>';
        cad += '<td class="">' + $("#txtTelefonoContacto").val() + '</td>';
        cad += '<td class="">' + $("#txtAnexoContacto").val() + '</td>';
        cad += '<td class="">' + $("#txtCelularContacto").val() + '</td>';
        cad += '<td class="">' + $("#txtMailContacto").val() + '</td>';
        cad += '<td class="">' + $("#txtFechaNacimiento").val() + '</td>';
        cad += '<td class="">' + cadButton("Contacto") + '</td>';
        cad += "</tr>";
        document.getElementById("tbContactos").innerHTML += cad;
    }
    else {
        cad += '<td class="">' + $("#txtNomContacto").val() + '</td>';
        cad += '<td class="">' + $("#txtCargo").val() + '</td>';
        cad += '<td class="">' + $("#txtTelefonoContacto").val() + '</td>';
        cad += '<td class="">' + $("#txtAnexoContacto").val() + '</td>';
        cad += '<td class="">' + $("#txtCelularContacto").val() + '</td>';
        cad += '<td class="">' + $("#txtMailContacto").val() + '</td>';
        cad += '<td class="">' + $("#txtFechaNacimiento").val() + '</td>';
        cad += '<td class="">' + cadButton("Contacto") + '</td>';
        $("#tbContactos")[0].rows[idxContacto - 1].innerHTML = cad;
    }
    cleanControl("contact");
    idxContacto = 0;
}
function cadButton(t) {
    let cad = "";
    cad += '<ul class="list-inline" style="margin-bottom: 0px;">';
    cad += '<li class="list-inline-item">';
    cad += '<div class="dropdown">';
    cad += '<button class="btn btn-soft-secondary btn-sm dropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false">';
    cad += '<i class="ri-more-fill align-middle"></i>';
    cad += '</button>';
    cad += '<ul class="dropdown-menu dropdown-menu-end" style="">';
    if (t != "Contrato") {
        cad += '<li>';
        cad += '<a class="dropdown-item edit-item-btn" href="javascript:void(0)" onclick="edit' + t + '(this)"><i class="ri-pencil-fill align-bottom me-2 text-muted"></i>Editar</a>';
        cad += '</li>';
    }
    cad += '<li>';
    cad += '<a class="dropdown-item remove-item-btn" href="javascript:void(0)" onclick="deleteRowDet(this);"><i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Eliminar</a>';
    cad += '</li>';
    cad += '</ul>';
    cad += '</div>';
    cad += '</li>';
    cad += ' </ul>';
    return cad;
}
function editContacto(e) {
    idxContacto = (e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).rowIndex;
    $("#txtNomContacto").val($("#tbContactos")[0].rows[idxContacto - 1].childNodes[0].innerHTML);
    $("#txtCargo").val($("#tbContactos")[0].rows[idxContacto - 1].childNodes[1].innerHTML);
    $("#txtTelefonoContacto").val($("#tbContactos")[0].rows[idxContacto - 1].childNodes[2].innerHTML);
    $("#txtAnexoContacto").val($("#tbContactos")[0].rows[idxContacto - 1].childNodes[3].innerHTML);
    $("#txtCelularContacto").val($("#tbContactos")[0].rows[idxContacto - 1].childNodes[4].innerHTML);
    $("#txtMailContacto").val($("#tbContactos")[0].rows[idxContacto - 1].childNodes[5].innerHTML);
    $("#txtFechaNacimiento").val($("#tbContactos")[0].rows[idxContacto - 1].childNodes[6].innerHTML);
}
function editBanco(e) {
    idxBanco = (e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).rowIndex;
    $("#txtDescripcionBanco").val($("#tbBanks")[0].rows[idxBanco - 1].childNodes[0].innerHTML);
    $("#cboBanco").val($("#tbBanks")[0].rows[idxBanco - 1].childNodes[1].dataset.id);
    $("#cboMoneda").val($("#tbBanks")[0].rows[idxBanco - 1].childNodes[2].dataset.id);
    $("#txtCtaBancaria").val($("#tbBanks")[0].rows[idxBanco - 1].childNodes[3].innerHTML);
    $("#txtCtaInterBancaria").val($("#tbBanks")[0].rows[idxBanco - 1].childNodes[4].innerHTML);
    $("#txtCtaDetraccion").val($("#tbBanks")[0].rows[idxBanco - 1].childNodes[5].innerHTML);
}
function deleteRowDet(e) {
    e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.remove();
}
function createPostObject() {
    //Crear objeto
    var obj = {};
    $("#formEntidad .form-control").each(function (index) {
        switch ($(this)[0].localName) {
            case "select":
                obj[$(this)[0].name] = $(this).val();
                break;
            case "input": case "textarea":
                if ($(this)[0].type == "checkbox") {
                    obj[$(this)[0].name] = $(this)[0].checked;
                }
                else {
                    obj[$(this)[0].name] = $(this).val();
                }
                break;
            default:
        }
    });
    
    //Cargar Contactos
    var listaContactos = [];
    $(".rowDetContacto").each(function (obj) {
        listaContactos.push({
            UniqueID: $(".rowDetContacto")[obj].dataset.id,
            Contacto: $(".rowDetContacto")[obj].children[0].innerHTML,
            Cargo: $(".rowDetContacto")[obj].children[1].innerHTML,
            Telefono: $(".rowDetContacto")[obj].children[2].innerHTML,
            Anexo: $(".rowDetContacto")[obj].children[3].innerHTML,
            Celular: $(".rowDetContacto")[obj].children[4].innerHTML,
            Mail: $(".rowDetContacto")[obj].children[5].innerHTML,
            FechaNacimiento: $(".rowDetContacto")[obj].children[6].innerHTML
        });
    });
    obj.Contactos = listaContactos;
    //Cargar Bancos
    var listaBancos = [];
    $(".rowDetBancos").each(function (obj) {
        listaBancos.push({
            UniqueID: $(".rowDetBancos")[obj].dataset.id,
            Descripcion: $(".rowDetBancos")[obj].children[0].innerHTML,
            BancoId: $(".rowDetBancos")[obj].children[1].dataset.id,
            TipoMonedaId: $(".rowDetBancos")[obj].children[2].dataset.id,
            NumeroCuenta: $(".rowDetBancos")[obj].children[3].innerHTML,
            NumeroCuentaInterbancaria: $(".rowDetBancos")[obj].children[4].innerHTML,
            NumeroCuentaDetraccion: $(".rowDetBancos")[obj].children[5].innerHTML
        });
    });
    obj.CtasBancarias = listaBancos;

    //Cargar Contratos
    var listaContratos = [];
    $(".rowDetContratos").each(function (obj) {
        listaContratos.push({
            ContratoID: $(".rowDetContratos")[obj].dataset.id,
            Descripcion: $(".rowDetContratos")[obj].children[0].innerHTML,
            NombreArchivo: $(".rowDetContratos")[obj].children[1].children[0].children[0].children[0].dataset.name,
            Archivo: $(".rowDetContratos")[obj].children[1].children[0].children[0].children[0].dataset.id,
        });
    });
    obj.Contratos = listaContratos;

    //Datos agregados al final
    obj.FechaAniversario = $("#txtFechaAniversario").val();
    obj["Tipo"] = "P";
    obj["TipoPersonaID"] = "01";
    obj.Updated_By = localStorage.getItem("user");
    return obj;
}