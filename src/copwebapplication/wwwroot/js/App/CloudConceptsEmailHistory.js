$(function () {
    $.ajaxSetup({
        cache: false
    });

    $("#fromDate").datepicker({
        format: 'mm/dd/yyyy',
        startDate: '-3d'
    });

    $("#toDate").datepicker({
        format: 'mm/dd/yyyy',
        startDate: '-3d'
    });

    $("#search").click(function () {
        $("#waitCell").show();

        $.get(Url.EmailHistorySearch + '?fromDate=' + $("#fromDate").val() + '&toDate=' + $("#toDate").val())
            .success(function (result) {
                $("#resultTable").css("border", "1px solid gray");
                $('#resultTable').find("tr:gt(0)").remove();

                result.forEach(function (row) {
                    $('#resultTable').append('<tr><td class="customTd">' + new Date(parseInt(row.RequestDateTime.substr(6))) + '</td><td class="customTd">' + row.Status + '</td><td class="customTd">' + row.To + '</td><td class="customTd">' + row.Message + '</td></tr>');
                });

                $("#waitCell").hide();
            })
            .fail(function () {
                alert("Unable to connect to service. \n Please try later.");
                $("#waitCell").hide();
            });
    });
    
});