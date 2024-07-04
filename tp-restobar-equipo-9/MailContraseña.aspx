<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.Master" AutoEventWireup="true" CodeBehind="MailContraseña.aspx.cs" Inherits="tp_restobar_equipo_9.MailContraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container containerbott form_top">
        <h1 style="text-align: center;">Recupera tu cuenta</h1>
        <div class="row justify-content-center containerbott">
            <div class="col-6">
                <div class="mb-2">
                    <asp:Label ID="lblDNI" runat="server" Class="form-label" Text="Ingrese su DNI"></asp:Label>
                    <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" placeholder="Ingrese su DNI"></asp:TextBox>
                </div>
                <div class="mb-2">
                    <asp:Label ID="lblNuevaContraseña" runat="server" Class="form-label" Text="Nueva Contraseña"></asp:Label>
                    <asp:TextBox ID="txtNuevaContraseña" type="password" runat="server" CssClass="form-control" placeholder="Ingrese su nueva contraseña"></asp:TextBox>
                </div>
                <div class="mb-2">
                    <asp:Label ID="lblConfirmarContraseña" runat="server" Class="form-label" Text="Vuelva a escribir su contraseña"></asp:Label>
                    <asp:TextBox ID="txtConfirmarContraseña" type="password" runat="server" CssClass="form-control" placeholder="Repita su contraseña"></asp:TextBox>
                </div>
                <div class="mb-2">
                    <asp:Button ID="btnEnviar" runat="server" Text="Enviar Contraseña" CssClass="Boton" OnClick="btnEnviar_Click" />
                </div>
                <div class="mb-2">
                    <asp:Label ID="lblMostrarDNIUsuario" runat="server" Class="form-label" ></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
