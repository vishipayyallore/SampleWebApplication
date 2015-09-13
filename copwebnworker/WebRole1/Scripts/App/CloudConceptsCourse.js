$(function () {
    $.ajaxSetup({
        cache: false
    });

    $("#search").click(function () {
        $("#waitCell").show();

        $.get(Url.CourseSearch + '?courseName=' + $("#courseName").val())
            .success(function (result) {
                $("#resultTable").css("border", "1px solid gray");
                $('#resultTable').find("tr:gt(0)").remove();

                result.forEach(function (course) {
                    $('#resultTable').append('<tr><td class="customTd">&nbsp;&bull;&nbsp;</td><td class="customTd">' + course + '</td></tr>');
                });

                $("#waitCell").hide();
            })
            .fail(function () {
                alert("Unable to connect to service. \n Please try later.");
                $("#waitCell").hide();
            });
    });
    
});