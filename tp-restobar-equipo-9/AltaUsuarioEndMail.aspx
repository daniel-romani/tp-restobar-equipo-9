<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.Master" AutoEventWireup="true" CodeBehind="AltaUsuarioEndMail.aspx.cs" Inherits="tp_restobar_equipo_9.AltaUsuarioEndMail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
             <%-- REGISTRO DEL USUARIO --%>
 <div class="container form_top form_bottom containerbott">
     <div class="row align-items-center">
            <li class="list-group-item">
                <h1>Muchas gracias por elegirnos!</h1>
                <h3>Se enviaron las credenciales de ingreso al email proporcionado.</h3>
                <hr />
            </li>
         </div>
     <br />
     <a href='Default.aspx' class='btn btn-success'>Continuar</a>
 </div>
</asp:Content>
