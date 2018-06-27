$("#myLink").click(function (e) {

    e.preventDefault();
    $.ajax({

        url:$(this).attr("href"),
        success: function () {
        alert("Deck Deleted");
        }

    });

});