function ControlActions() {
    this.URL_API = "https://localhost:7117/api/";

    this.GetUrlApiService = service => `${this.URL_API}${service}`;

    this.GetTableColumsDataName = tableId => $('#' + tableId).attr("ColumnsDataName");

    this.FillTable = (service, tableId, refresh) => {
        if (!refresh) {
            const columns = this.GetTableColumsDataName(tableId).split(',');
            const arrayColumnsData = columns.map(value => ({ data: value }));

            $('#' + tableId).DataTable({
                "processing": true,
                "ajax": {
                    "url": this.GetUrlApiService(service),
                    dataSrc: 'Data'
                },
                "columns": arrayColumnsData
            });
        } else {
            $('#' + tableId).DataTable().ajax.reload();
        }
    }

    this.GetSelectedRow = () => sessionStorage.getItem(tableId + '_selected');

    this.BindFields = (formId, data) => {
        console.log(data);
        $('#' + formId + ' *').filter(':input').each(function (input) {
            const columnDataName = $(this).attr("ColumnDataName");
            this.value = data[columnDataName];
        });
    }

    this.GetDataForm = formId => {
        const data = {};
        $('#' + formId + ' *').filter(':input').each(function (input) {
            const columnDataName = $(this).attr("ColumnDataName");
            data[columnDataName] = this.value;
        });
        console.log(data);
        return data;
    }

    this.ShowMessage = (type, message) => {
        const alertType = type === 'E' ? 'danger' : 'success';
        $("#alert_container").removeClass("alert alert-success alert-danger alert-dismissable")
            .addClass(`alert alert-${alertType} alert-dismissable`);
        $("#alert_message").text(message);
        $('.alert').show();
    };

    this.ajaxAction = (type, service, data, callBackFunction) => {
        $.ajax({
            type,
            url: this.GetUrlApiService(service),
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (callBackFunction) {
                    callBackFunction(response);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(`Hubo un error al enviar la solicitud: ${textStatus}, ${errorThrown}`);
            }
        });
    };

    this.PostToAPI = (service, data, callBackFunction) => this.ajaxAction("POST", service, data, callBackFunction);

    this.PutToAPI = (service, data, callBackFunction) => this.ajaxAction("PUT", service, data, callBackFunction);

    this.DeleteToAPI = (service, data, callBackFunction) => this.ajaxAction("DELETE", service, data, callBackFunction);

    this.GetToApi = (service, onSuccess, onError, onHttpResponse) => {
        const url = this.GetUrlApiService(service);
        const xhr = new XMLHttpRequest();
        xhr.open("GET", url, true);
        xhr.onreadystatechange = function () {
            if (this.readyState === XMLHttpRequest.DONE) {
                if (this.status === 200) {
                    if (onSuccess) {
                        onSuccess(JSON.parse(this.responseText));
                    }
                } else {
                    if (onHttpResponse) {
                        onHttpResponse(this);
                    }
                    if (onError) {
                        onError();
                    }
                }
            }
        };
        xhr.send();
    };

    this.GetToApiV2 = (service, callBackFunction) => {
        $.ajax({
            type: "GET",
            url: this.GetUrlApiService(service),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (callBackFunction) {
                    callBackFunction(data);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (callBackFunction) {
                    console.log(jqXHR.responseJSON);
                    callBackFunction(jqXHR.responseJSON)
                }
            }
        });
    };
}

$.ajaxAction = function (type, url, data, callback) {
    return $.ajax({
        url,
        type,
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
}

$.put = (url, data, callback) => $.ajaxAction('PUT', url, data, callback);

$.delete = (url, data, callback) => $.ajaxAction('DELETE', url, data, callback);

$.post = (url, data, callback) => $.ajaxAction('POST', url, data, callback);