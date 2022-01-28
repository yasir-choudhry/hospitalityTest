$('document').ready(function () {

    $('.table:not(#_tblBasket, #_tblInvoiceReport)').DataTable();
    $('.table:not(#_tblBasket, #_tblInvoiceReport)').removeAttr("role");

    $('#_aEmptyBasket').on("click", function (e) {
        if (!confirm('Are you sure you want to empty the basket?')) { e.preventDefault(); }
    });

    $('tbody').on("click", '.btnremoveitem', function (e) {
        if (!confirm('Are you sure you want to remove this item?')) { e.preventDefault(); }
    });

    $('tbody').on("click", '.btncancelorder', function (e) {
        if (!confirm('Are you sure you want to cancel this order?')) { e.preventDefault(); }
    });

    $('#_tblInvoiceReport').dataTable({
        "scrollY": "400px",
        "scrollCollapse": true,
        "paging": false
    });

});
