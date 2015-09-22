$(function () {
    $.ajaxSetup({
        cache: false
    });

    $("#search").click(function () {
        $("#waitCell").show();

        $.get(Url.AuthorSearch + '?firstName=' + $("#firstName").val() + '&lastName=' + $("#lastName").val())
            .success(function (result) {
                $("#resultTable").css("border", "1px solid gray");
                $('#resultTable').find("tr:gt(0)").remove();
                
                result.forEach(function (row) {
                    $('#resultTable').append('<tr><td class="customTd">' + row.Firstname + '</td><td class="customTd">' + row.Lastname + '</td><td class="customTd"><a href="https://twitter.com/' + row.Twitter + '">@' + row.Twitter + '</a></td><td class="customTd">' + row.Phone + '</td><td class="customTd"><a href="#" class="emailClick" data-email="' + row.Email + '" data-twitter="' + row.Twitter + '">' + row.Email + '</a></td><td><img src="' + Url.Image + row.Twitter + '" alt="Photo" height="50" width="50"></td></tr>');
                });

                $(".emailClick").click(function (e) {
                    $('#to').val($(this).attr('data-email'));
                    $('#thandle').val($(this).attr('data-twitter'));

                    $("#email-dialog").dialog({
                        modal: true,
                        draggable: false,
                        resizable: false,
                        position: ['center', 'top'],
                        show: 'blind',
                        hide: 'blind',
                        width: 400,
                        dialogClass: 'ui-dialog-osx',
                        buttons: {
                            "Send": function () {
                                $emailBox = $(this);

                                $.get(Url.Mail + '?to=' + $("#to").val() + '&message=' + $("#message").val() + '&thandle=' + $("#thandle").val())
                                    .success(function (result) {
                                        $emailBox.dialog("close");
                                    })
                                    .fail(function () {
                                        $emailBox.dialog("close");
                                    });
                            },
                            "Cancel": function () {
                                $(this).dialog("close");
                            }
                        }
                    });

                    e.preventDefault();
                });

                $("#waitCell").hide();
            })
            .fail(function () {
                alert("Unable to connect to service. \n Please try later.");
                $("#waitCell").hide();
            });
    });
    
});