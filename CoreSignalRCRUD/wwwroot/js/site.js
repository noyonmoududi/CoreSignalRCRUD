
$(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/SignalRServer").build();

    connection.start();

    connection.on("refreshProducts", function () {
        loadData();
    });

    loadData();

    function loadData() {
        var tr = '';

        $.ajax({
            url: '/Product/GetProducts',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr = tr + `<tr>
                        <td>${v.ProductName}</td>
                        <td>${v.UnitPrice}</td>
                       <td>${v.Qty}</td>
                        <td><a href='../Product/Delete?id=${v.ProductId}'>Delete</a></td>
                    </tr>`;
                });

                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
});