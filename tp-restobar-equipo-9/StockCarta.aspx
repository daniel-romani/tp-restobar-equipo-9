<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StockCarta.aspx.cs" Inherits="tp_restobar_equipo_9.StockCarta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater runat="server" ID="repRepetidor">
        <ItemTemplate>
            <div class="card" style="width: 18rem;">
                <img src=""<%#Eval("idProducto")%>"" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title"><%#Eval("nombre") %></h5>
                    <p class="card-text">"$ <%#Eval("cantidad") %>"</p>
                    <a href="#" class="btn btn-primary">Go somewhere</a>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
