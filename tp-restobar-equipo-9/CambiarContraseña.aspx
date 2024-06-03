<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CambiarContraseña.aspx.cs" Inherits="tp_restobar_equipo_9.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container containerbott" style="padding-top: 100px">
        <div class="row justify-content-center">
            <div class="col-6">
                <% if (!contraseñaCambiada) { %>
                <!-- Mostrar estos elementos solo si la contraseña no se ha verificado -->
                <div class="mb-2">
                    <asp:Label ID="lblContraseñaActual" runat="server" Class="form-label" Text="Contraseña Actual"></asp:Label>
                    <asp:TextBox ID="txtContraseñaActual" type="password" runat="server" CssClass="form-control" placeholder="Ingrese su contraseña"></asp:TextBox>
                </div>
                <div class="mb-2" style="padding-top: 15px; padding-bottom: 15px">
                    <asp:Label ID="lblNuevaContraseña" runat="server" Class="form-label" Text="Nueva Contraseña"></asp:Label>
                    <asp:TextBox ID="txtNuevaContraseña" type="password" runat="server" CssClass="form-control" placeholder="Ingrese su nueva contraseña"></asp:TextBox>
                </div>
                <div class="mb-2">
                    <asp:Label ID="lblConfirmarContraseña" runat="server" Class="form-label" Text="Vuelva a escribir su contraseña"></asp:Label>
                    <asp:TextBox ID="txtConfirmarContraseña" type="password" runat="server" CssClass="form-control" placeholder="Repita su contraseña"></asp:TextBox>
                </div>
                <div class="mb-2 d-flex justify-content-between" style="padding-top: 15px">
                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="Boton" OnClick="btnVolver_Click" />
                    <asp:Button ID="btnEnviar" runat="server" Text="Enviar Contraseña" CssClass="Boton" OnClick="btnEnviar_Click" />
                </div>
            <% } %>

            <% if (contraseñaCambiada) { %>
                <!-- Mostrar estos elementos solo si la contraseña se ha actualizado -->
                <div class="mb-2">
                    <asp:Label ID="lblContraseñaActualizada" runat="server" Class="form-label" Text="Contraseña actualizada con éxito!"></asp:Label> 
                    </div>
                <div class="mb-2">
                   
                    <asp:Label ID="lblAceptarIngresar" runat="server" Class="form-label" Text="Por favor, vuelva a iniciar la sesión"></asp:Label>
                    </div>
                <div class="mb-2">
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="Boton" OnClick="btnAceptar_Click" />
                </div>
            <% } %>
            </div>
        </div>
    </div>

</asp:Content>
