<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Jornadas.aspx.cs" Inherits="tp_restobar_equipo_9.Jornadas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="mb-4">Jornada</h2>
    <div class="row">
        <div class="col-md-6">
           

        </div>
        <% 
            if (!negocio.BuscarJornadaActiva())
            { %>
        <div class="col-md-6">
             <asp:TextBox ID="Fecha_Jornada" runat="server" type="date" ReadOnly="true" CssClass="form-control" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHora_ini" runat="server" type="time" step="1" ReadOnly="true" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnHora_ini" runat="server" Text="Seleccionar" CssClass="btn btn-primary mt-2" OnClick="btnHora_ini_Click" />
        </div>
        <% }
            else
            {%>
        <div class="col-md-6">
            <asp:TextBox ID="txtHora_fin" runat="server" type="time" ReadOnly="true" step="1" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnHora_fin" runat="server" Text="Seleccionar" CssClass="btn btn-primary mt-2" OnClick="btnHora_fin_Click" />
        </div>
        <%} %>
       
    </div>
    <div Class="text-center">
        <div Class="col-6">
            <asp:GridView ID="dgvJornada" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField HeaderText="Fecha" DataField="fecha" />
                    <asp:BoundField HeaderText="Hora inicio" DataField="hora_Ini" />
                    <asp:BoundField HeaderText="Hora fin" DataField="hora_Fin" />

                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
