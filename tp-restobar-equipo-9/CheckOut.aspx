<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="tp_restobar_equipo_9.CheckOut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>Comanda</h2>
        <div class="row">
            <div class="col-md-12">
                <div class="order-info">
                    <p><strong>N° de Mesa:</strong>
                        <asp:Label ID="lblMesaId" runat="server"></asp:Label></p>
                    <p><strong>N° de Comensales:</strong>
                        <asp:Label ID="lblNroComensales" runat="server"></asp:Label></p>
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
            </div>
        </div>
    </div>
</asp:Content>
