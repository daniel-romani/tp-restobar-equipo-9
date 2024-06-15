<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SeleccionOrden.aspx.cs" Inherits="tp_restobar_equipo_9.SeleccionOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <h2>Comanda</h2>
    <div class="row">
        <div class="col-md-12">
            <div class="order-info">
                <p><strong>N° de Mesa:</strong> <asp:Label ID="lblMesaId" runat="server"></asp:Label></p>
                <p><strong>N° de Comensales:</strong> <asp:Label ID="lblNroComensales" runat="server"></asp:Label></p>
            </div>
            <div class="order-details">
                <table class="table">
                    <thead>
                        <tr>
                            <th>Cantidad</th>
                            <th>Concepto</th>
                            <th>Precio Unitario</th>
                            <th>Total</th>
                        </tr>
                    </thead>
                    <tbody id="orderItems">
                        <!-- Items de la comanda se cargarán aquí -->
                    </tbody>
                </table>
            </div>
            <div class="menu-selection">
                <h3>Seleccionar de la Carta</h3>
                <asp:Repeater ID="rptCarta" runat="server">
                    <ItemTemplate>
                        <div class="menu-item">
                            <p><%# Eval("Nombre") %> - $<%# Eval("Precio") %> (<%# Eval("Tipo") %>)</p>
                            <button type="button" class="btn btn-primary" onclick="agregarItem('<%# Eval("Nombre") %>', <%# Eval("Precio") %>, '<%# Eval("Tipo") %>')">Agregar</button>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    function agregarItem(nombre, precio, tipo) {
        var table = document.getElementById("orderItems");
        var rows = table.getElementsByTagName("tr");

        for (var i = 0; i < rows.length; i++) {
            var cells = rows[i].getElementsByTagName("td");
            if (cells[1].innerText === nombre) {
                // Item already exists, update quantity and total
                var quantityCell = cells[0].getElementsByTagName("input")[0];
                var quantity = parseInt(quantityCell.value) + 1;
                quantityCell.value = quantity;
                cells[3].innerText = (quantity * precio).toFixed(2);
                return;
            }
        }

        // Item does not exist, create new row
        var newRow = document.createElement('tr');
        newRow.innerHTML = `
            <td><input type="number" value="1" class="form-control" onchange="actualizarTotal(this, ${precio})" /></td>
            <td>${nombre}</td>
            <td>${precio.toFixed(2)}</td>
            <td>${precio.toFixed(2)}</td>
        `;
        newRow.setAttribute('data-tipo', tipo);

        table.appendChild(newRow);
    }

    function actualizarTotal(input, precio) {
        var row = input.closest('tr');
        var totalCell = row.getElementsByTagName('td')[3];
        var quantity = parseInt(input.value);
        totalCell.innerText = (quantity * precio).toFixed(2);
    }
</script>
</asp:Content>
