﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Servicio/ObtenerTodas",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "nombre", "width": "20%" },
            { "data": "categoria.nombre", "width": "20%" },
            { "data": "precio", "width": "15%" },
            { "data": "frecuencia.conteo", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Servicio/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa-edit'></i> Editar
                                </a>
                                &nbsp;
                                <a onclick=Eliminar("/Admin/Servicio/Eliminar/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa-trash-alt'></i> Eliminar
                                </a>
                            </div>
                            `;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "Sin registros."
        },
        "width": "100%"
    });
}

function Eliminar(url) {
    swal({
        title: "Estás seguro de eliminar el servicio?",
        text: "No podrás volver a obtener el contenido borrado!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, eliminalo!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}
