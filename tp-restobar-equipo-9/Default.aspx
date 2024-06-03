<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp_restobar_equipo_9.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <%-- Labels y TextBoxs --%>
    <div class="container containerbott form_top">
        <div class="row justify-content-center">
            <div class="col-12 text-center">
                <img src="./Resources/logoIcon.png" alt="RestEasy" class="img-fluid" />
            </div>
        </div>
        <h1 style="text-align: center;">Ingreso al Sistema</h1>
        <div class="row justify-content-center">
            <div class="col-6">
                <div class="mb-3">
                    <asp:Label ID="lblUsuario" runat="server" Class="form-label" Text="Usuario"></asp:Label>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Nombre de Usuario"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblContraseña" runat="server" Class="form-label" Text="Contraseña"></asp:Label>
                    <asp:TextBox ID="txtContraseña" type="password" runat="server" CssClass="form-control" placeholder="Ingrese su Contraseña"></asp:TextBox>
                </div>
                <div class="mb-2 d-flex justify-content-between">
                    <a href="DefaultCambioContraseña.aspx">¿Olvidaste tu contraseña?</a>
                    <asp:Button ID="btnIngresar" class="Boton" runat="server" OnClick="btnIngresar_Click" Text="Ingresar"/>
                </div>

                <hr /> 
                <br />
                <div class="mb-2 text-center">
                    <asp:Label ID="lblRegistrar" runat="server" Class="form-label" Text="¿No poseé una cuenta?"></asp:Label>
                </div>
                <div class="text-center">
                    <asp:Button ID="btnRegistrar" class="Boton" runat="server" Text="Registrarme" OnClick="btnRegistrar_Click"/>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
