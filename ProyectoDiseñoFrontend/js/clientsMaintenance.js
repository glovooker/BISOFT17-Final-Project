function ClientsView() {

    this.ViewName = "ClientsView";
    this.ApiService = "Client";

    var self = this;

    this.InitView = function () {

        $("#btnCreate").show();
        $("#btnUpdate").hide();
        $("#btnDelete").hide();

        $("#btnCreate").click(function () {
            var view = new ClientsView();
            view.Create();
        });

        $("#btnUpdate").click(function () {
            var view = new ClientsView();
            view.Update();
        });

        $("#btnDelete").click(function () {
            var view = new ClientsView();
            view.Delete();
        });

        $("#btnCancel").click(function () {
            var view = new ClientsView();
            view.CleanForm();
        });

        this.LoadClientsTable();
    }

    this.Create = function () {
        const formValidation = this.InputsValidation($("#name").val(), $("#mainContact").val(), $("#email").val());
        if (formValidation) {
            var client = {};
            client.name = $("#name").val();
            client.mainContact = $("#mainContact").val();
            client.email = $("#email").val();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Create"

            ctrlActions.PostToAPI(serviceCreate, client, function () {
                self.LoadClientsTable();
                Swal.fire({
                    title: 'Client created',
                    text: 'Client created successfully!',
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
        const formValidation = this.InputsValidation($("#name").val(), $("#mainContact").val(), $("#email").val());
        if (formValidation) {
            var client = {};

            client.id = $("#id").val();
            client.name = $("#name").val();
            client.mainContact = $("#mainContact").val();
            client.email = $("#email").val();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Update"

            ctrlActions.PutToAPI(serviceCreate, client, function () {
                self.LoadClientsTable();
                Swal.fire({
                    title: 'Client updated',
                    text: 'Client updated successfully!',
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

    this.LoadClientsTable = function () {
        self.CleanForm();

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Verificar si ya existe una instancia de la tabla
        if ($.fn.DataTable.isDataTable('#tblClients')) {
            // Destruir la instancia existente
            $('#tblClients').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'mainContact' };
        arrayColumnsData[1] = { 'data': 'name' };
        arrayColumnsData[2] = { 'data': 'email' };

        // Crear la instancia de la tabla con los datos vacíos
        var table = $('#tblClients').DataTable({
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

        $('#tblClients tbody').on('click', 'tr', function () {
            var tr = $(this).closest('tr');
            var data = $('#tblClients').DataTable().row(tr).data();

            $("#id").val(data.id);
            $("#mainContact").val(data.mainContact);
            $("#name").val(data.name);
            $("#email").val(data.email);

            $("#btnCreate").hide();
            $("#btnUpdate").show();
            $("#btnDelete").show();
        });
    }

    this.InputsValidation = (mainContact, name, email) => {
        return !(mainContact === "" || name === "" || email === "");
    }

    this.Delete = function () {
        Swal.fire({
            title: 'Confirm action',
            text: 'Are you sure you want to delete this client?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then((result) => {
            if (result.isConfirmed) {
                var ctrlActions = new ControlActions();
                var serviceDelete = this.ApiService + "/Delete?id=" + $("#id").val();

                ctrlActions.DeleteToAPI(serviceDelete, null, function () {
                    self.LoadClientsTable();
                    Swal.fire({
                        title: 'Client deleted',
                        text: 'Client deleted successfully!',
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
        $("#mainContact").val("");
        $("#name").val("");
        $("#email").val("");

        $("#btnCreate").show();
        $("#btnUpdate").hide();
        $("#btnDelete").hide();
    }
}

$(document).ready(function () {
    var view = new ClientsView();
    view.InitView();
})