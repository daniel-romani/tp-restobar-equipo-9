<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Mesas.aspx.cs" Inherits="tp_restobar_equipo_9.Mesas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    .title{
        display: flex;
        justify-content: space-between;
        width: 100%;
        align-items: center;
    }
    .mesa {
        display: flex;
        flex-direction: column;
        align-items: center;
        border: 2px solid #000;
        padding: 10px;
        border-radius: 10px;
        margin: 15px;
        text-align: center;
    }

    .mesa-info {
        display: flex;
        justify-content: center;
        width: 100%;
        align-items: center;
    }

    .datos {
        display: flex;
        flex-direction: column;
        align-items: center;
        text-align: center;
    }

    .mesa-numero, .mesa-capacidad {
        display: flex;
        justify-content: center;
        align-items: center;
        border: 2px solid #000;
        border-radius: 50%;
        width: 60px;
        height: 60px;
        margin: 10px 0;
        background-color: #f0f0f0; /* Color de fondo para los círculos */
    }

    .mesa-capacidad-verde {
        background-color: green;
    }

    .mesa-capacidad-rojo {
        background-color: red;
    }

    .mesa-numero p, .mesa-capacidad p {
        margin: 0;
    }

    .mesa-imagen {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 50%;
    }

    .mesa-imagen img {
        width: 50%;
        height: auto;
    }

    .btn {
        margin-top: 20px;
    }

    .btn-container{
        flex-direction: row;
    }

    #botones {
    display: flex;
    align-items: center;
    justify-content: space-around;
    padding: 10px;
    }

    #botones li, #botones span, #botones button {
        margin: 0 5px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="title">
      <h2>Mesas</h2>
      </div>
      <div class="row" id="mesasContainerDiv">
          <asp:PlaceHolder ID="mesasContainer" runat="server"></asp:PlaceHolder>
  </div>
</asp:Content>
