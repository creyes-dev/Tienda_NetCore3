var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("Aprobado")) {
        loadDataTable("ObtenerTodasComprasAprobadas");
    } else {
        if (url.includes("Todas")) {
            loadDataTable("ObtenerTodas");
        }
        else {
            loadDataTable("ObtenerTodasComprasPendientes");
        }
    }
});

function loadDataTable(url) {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Compra/" + url,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "nombre", "width": "20%" },
            { "data": "telefono", "width": "20%" },
            { "data": "email", "width": "15%" },
            { "data": "conteoServicios", "width": "15%" },
            { "data": "estado", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/compra/Detalles/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa-edit'></i> Detalle
                                </a>
                            </div>
                            `;
                }, "width": "15%"
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
        title: "Estás seguro de eliminar la categoría?",
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
