<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Estadisticas.aspx.cs" Inherits="tp_restobar_equipo_9.Estadisticas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.6.0/Chart.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <canvas id="ventasxMes"></canvas>
    </div>
    <div>
        <canvas id="productosxMes"></canvas>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <script>
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "Estadisticas.aspx/cargarVentasXMes",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var ventasMensuales = response.d;
                    console.log("Ventas por mes: ", ventasMensuales);

                    const ctx = document.getElementById('ventasxMes');
                    const xVentasPorMes = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre']

                    new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: xVentasPorMes,
                            datasets: [{
                                label: '# de pedidos x mes',
                                data: ventasMensuales,
                                borderWidth: 1
                            }]
                        },
                        options: {
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        }
                    });
                },
                failure: function (response) {
                    alert("Error: " + response.d);
                }
            });
        });

        
    </script>
    <script>
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "Estadisticas.aspx/cargarProductosXMes",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var productosMensuales = response.d;
                    console.log("Productos por mes: ", productosMensuales);

                    const ctx2 = document.getElementById('productosxMes');
                    const xProductosPorMes = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre']

                    new Chart(ctx2, {
                        type: 'bar',
                        data: {
                            labels: xProductosPorMes,
                            datasets: [{
                                label: '# de productos x mes',
                                data: productosMensuales,
                                borderWidth: 1
                            }]
                        },
                        options: {
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        }
                    });
                },
                failure: function (response) {
                    alert("Error: " + response.d);
                }
            });
        });
    </script>
</asp:Content>
