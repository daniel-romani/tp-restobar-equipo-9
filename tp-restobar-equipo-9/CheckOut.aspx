<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="tp_restobar_equipo_9.CheckOut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function confirmarCierreCuenta() {
            return confirm("¿Está seguro de que desea cerrar la cuenta?");
        }
    </script>

    <div class="container">
        <h2>Comanda</h2>
        <div class="row">
            <div class="col-md-12">
                <div class="order-info">
                    <p>
                        <strong>N° de Mesa:</strong>
                        <asp:Label ID="lblMesaId" runat="server"></asp:Label>
                        <asp:HiddenField ID="hiddenMesaId" runat="server" />
                    </p>
                    <p>
                        <strong>N° de Comensales:</strong>
                        <asp:Label ID="lblNroComensales" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="order-details">
                    <asp:Table ID="orderItems" runat="server" CssClass="table">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell>Cantidad</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Concepto</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Precio Unitario</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Total</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </div>
            </div>
        </div>
        <asp:Button ID="Btn_cerrarCuenta" runat="server" CssClass="btn btn-success" OnClientClick="return confirmarCierreCuenta();" OnClick="Btn_CerrarCuenta_Click" Text="Cerrar cuenta" Visible="true" />
        <asp:Literal ID="literalScript" runat="server"></asp:Literal>
    </div>
</asp:Content>
