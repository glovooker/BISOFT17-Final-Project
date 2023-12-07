function BandsView() {

    this.ViewName = "BandsView";
    this.ApiService = "Band";

    var self = this;

    this.InitView = function () {

        $("#btnCreate").show();
        $("#btnUpdate").hide();
        $("#btnDelete").hide();

        $("#btnCreate").click(function () {
            var view = new BandsView();
            view.Create();
        });

        $("#btnUpdate").click(function () {
            var view = new BandsView();
            view.Update();
        });

        $("#btnDelete").click(function () {
            var view = new BandsView();
            view.Delete();
        });

        $("#btnCancel").click(function () {
            var view = new BandsView();
            view.CleanForm();
        });

        this.LoadBandsTable();
    }

    this.Create = function () {
        const formValidation = this.InputsValidation($("#price").val(), $("#description").val());
        if (formValidation) {
            var band = {};
            band.price = $("#price").val();
            band.description = $("#description").val();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Create"

            ctrlActions.PostToAPI(serviceCreate, band, function () {
                self.LoadBandsTable();
                Swal.fire({
                    title: 'Band created',
                    text: 'Band created successfully!',
                    icon: 'success',
                    confirmButtonText: 'Ok'
                });
            });
        } else {
            Swal.fire({
                title: 'Error with the form',
                text: 'Please complete all the fields of the form.',
                icon: 'error',
                confirmButtonText: 'Ok'
            });
        }
    }

    this.Update = function () {
        const formValidation = this.InputsValidation($("#price").val(), $("#description").val());
        if (formValidation) {
            var band = {};

            band.id = $("#id").val();
            band.price = $("#price").val();
            band.description = $("#description").val();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Update"

            ctrlActions.PutToAPI(serviceCreate, band, function () {
                self.LoadBandsTable();
                Swal.fire({
                    title: 'Band updated',
                    text: 'Band updated successfully!',
                    icon: 'success',
                    confirmButtonText: 'Ok'
                });
            });
        } else {
            Swal.fire({
                title: 'Error with the form',
                text: 'Please complete all the fields of the form.',
                icon: 'error',
                confirmButtonText: 'Ok'
            });
        }
    }

    this.LoadBandsTable = function () {
        self.CleanForm();

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Verificar si ya existe una instancia de la tabla
        if ($.fn.DataTable.isDataTable('#tblBands')) {
            // Destruir la instancia existente
            $('#tblBands').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'description' };
        arrayColumnsData[1] = { 'data': 'price' };

        // Crear la instancia de la tabla con los datos vacíos
        var table = $('#tblBands').DataTable({
            "data": [],
            "columns": arrayColumnsData
        });

        // Actualizar la tabla con los nuevos datos
        $.ajax({
            url: urlService,
            method: "GET",
            dataType: "json",
            success: function (data) {
                table.rows.add(data).draw();
            },
            error: function (error) {
                console.log(error);
            }
        });

        $('#tblBands tbody').on('click', 'tr', function () {
            var tr = $(this).closest('tr');
            var data = $('#tblBands').DataTable().row(tr).data();

            $("#id").val(data.id);
            $("#description").val(data.description);
            $("#price").val(data.price);

            $("#btnCreate").hide();
            $("#btnUpdate").show();
            $("#btnDelete").show();
        });
    }

    this.InputsValidation = (description, price) => {
        return !(description === "" || price === "");
    }

    this.Delete = function () {
        Swal.fire({
            title: 'Confirm action',
            text: 'Are you sure you want to delete this band?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then((result) => {
            if (result.isConfirmed) {
                var ctrlActions = new ControlActions();
                var serviceDelete = this.ApiService + "/Delete?id=" + $("#id").val();

                ctrlActions.DeleteToAPI(serviceDelete, null, function () {
                    self.LoadBandsTable();
                    Swal.fire({
                        title: 'Band deleted',
                        text: 'Band deleted successfully!',
                        icon: 'success',
                        confirmButtonText: 'Ok'
                    });
                });
            } else {
                self.CleanForm();
            }
        });
    }

    this.CleanForm = function () {
        $("#id").val("");
        $("#description").val("");
        $("#price").val("");

        $("#btnCreate").show();
        $("#btnUpdate").hide();
        $("#btnDelete").hide();
    }
}

$(document).ready(function () {
    var view = new BandsView();
    view.InitView();
})