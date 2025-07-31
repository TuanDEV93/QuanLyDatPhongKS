$(document).ready(function(){
    $(".modal").parsley()
}), $(function () {
    $("#modal-schedule").parsley().on("field:validated", function () {
        var n = 0 === $(".parsley-error").length;
        $(".alert-info").toggleClass("d-none", !n),
            $(".alert-warning").toggleClass("d-none", n)
    }).on("input[type=submit]", function () {
        return !1
    })
});