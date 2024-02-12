$(document).ready(function () {
    $('.table-t tr').click(function () {
        $('.table-t tr').removeClass('active');
        $(this).addClass('active');
        let active = document.getElementsByClassName("active");
        document.getElementById("dataUpdate").setAttribute("value", active.item(0).textContent);
        document.getElementById("dataDelete").setAttribute("value", active.item(0).textContent);
    });
});
