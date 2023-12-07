function AccountsView() {

    this.ViewName = "AccountsView";
    this.ApiService = "Account";
    this.ClientsApiService = "Client";

    var self = this;
    var administrators = [];

    this.InitView = function () {

        $("#btnCreate").show();
        $("#btnUpdate").hide();
        $("#btnDelete").hide();

        $("#btnCreate").click(function () {
            var view = new AccountsView();
            view.Create();
        });

        $("#btnUpdate").click(function () {
            var view = new AccountsView();
            view.Update();
        });

        $("#btnDelete").click(function () {
            var view = new AccountsView();
            view.Delete();
        });

        $("#btnCancel").click(function () {
            var view = new AccountsView();
            view.CleanForm();
        });

        this.LoadStudentsTable();
    }

    this.Create = function () {
        const formValidation = this.InputsValidation($("#region").val(), $("#budget").val(), $("#administrator").val());
        if (formValidation) {
            var account = {};
            account.region = $("#region").val();
            account.budgetAnual = $("#budget").val();
            account.adminId = $("#administrator").val();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Create"

            ctrlActions.PostToAPI(serviceCreate, account, function () {
                self.LoadStudentsTable();
                Swal.fire({
                    title: 'Account created',
                    text: 'Account created successfully!',
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
        const formValidation = this.InputsValidation($("#region").val(), $("#budget").val(), $("#administrator").val());
        if (formValidation) {
            var account = {};

            account.id = $("#id").val();
            account.region = $("#region").val();
            account.budgetAnual = $("#budget").val();
            account.adminId = $("#administrator").val();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Update"

            ctrlActions.PutToAPI(serviceCreate, account, function () {
                self.LoadStudentsTable();
                Swal.fire({
                    title: 'Account updated',
                    text: 'Account updated successfully!',
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

    this.LoadStudentsTable = function () {
        self.CleanForm();
        self.LoadAdminsDropdown();

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Verificar si ya existe una instancia de la tabla
        if ($.fn.DataTable.isDataTable('#tblAccounts')) {
            // Destruir la instancia existente
            $('#tblAccounts').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'region' }
        arrayColumnsData[1] = {
            'data': null,
            'render': function (data) {
                var admin = administrators.find(admin => admin.clientId === data.adminId);

                if (!admin) {
                    return "No admin found";
                }

                return admin.name;
            }
        };

        arrayColumnsData[2] = { 'data': 'budgetAnual' }

        // Crear la instancia de la tabla con los datos vacíos
        var table = $('#tblAccounts').DataTable({
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

        $('#tblAccounts tbody').on('click', 'tr', function () {
            var tr = $(this).closest('tr');
            var data = $('#tblAccounts').DataTable().row(tr).data();

            $("#id").val(data.id);
            $("#region").val(data.region);
            $("#budget").val(data.budgetAnual);
            $("#administrator").val(data.adminId);

            $("#btnCreate").hide();
            $("#btnUpdate").show();
            $("#btnDelete").show();
        });
    }

    this.InputsValidation = (region, budget, admin) => {
        return !(region === "" || budget === "" || admin === "");
    }

    this.Delete = function () {
        Swal.fire({
            title: 'Confirm action',
            text: 'Are you sure you want to delete this account?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then((result) => {
            if (result.isConfirmed) {
                var ctrlActions = new ControlActions();
                var serviceDelete = this.ApiService + "/Delete?id=" + $("#id").val();

                ctrlActions.DeleteToAPI(serviceDelete, null, function () {
                    self.LoadStudentsTable();
                    Swal.fire({
                        title: 'Account deleted',
                        text: 'Account deleted successfully!',
                        icon: 'success',
                        confirmButtonText: 'Ok'
                    });
                });
            } else {
                self.CleanForm();
            }
        });
    }

    this.LoadAdminsDropdown = function () {
        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ClientsApiService + "/RetrieveAll");

        const select = document.getElementById("administrator");

        $.ajax({
            url: urlService,
            method: "GET",
            dataType: "json",
            success: function (data) {
                administrators = data;
                select.innerHTML = "";

                administrators.forEach((dato) => {
                    const option = document.createElement("option");
                    option.text = dato.name;
                    option.value = dato.clientId;
                    select.add(option);
                });
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    this.CleanForm = function () {
        $("#id").val("");
        $("#region").val("");
        $("#budget").val("");
        $("#administrator").val("");

        $("#btnCreate").show();
        $("#btnUpdate").hide();
        $("#btnDelete").hide();
    }
}

$(document).ready(function () {
    var view = new AccountsView();
    view.InitView();
})