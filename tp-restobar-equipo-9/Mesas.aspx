<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Mesas.aspx.cs" Inherits="tp_restobar_equipo_9.Mesas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
        .mesa {
            border: 1px solid #ddd;
            border-radius: 4px;
            padding: 10px;
            margin: 10px;
            text-align: center;
        }
        .mesa img {
            max-width: 100%;
            height: auto;
        }
        .mesa-info {
            margin-top: 10px;
        }
    </style>

    <script type="text/javascript">
        function abrirModalMesa(id, numero, capacidad) {
            document.getElementById('<%= txtMesaId.ClientID %>').value = id || '';
            document.getElementById('<%= txtNumeroMesa.ClientID %>').value = numero || '';
            document.getElementById('<%= txtCapacidad.ClientID %>').value = capacidad || '';
            $('#abmModal').modal('show');
        }

        function guardarMesa() {
            __doPostBack('<%= btnGuardarMesa.UniqueID %>', '');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="container">
      <h2>Mesas</h2>
      <div class="row" id="mesasContainerDiv">
          <asp:PlaceHolder ID="mesasContainer" runat="server"></asp:PlaceHolder>
      </div>
      <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#abmModal" onclick="abrirModalMesa()">Agregar Mesa</button>
  </div>

  <%-- Modal para ABM de mesas --%>
  <div class="modal fade" id="abmModal" tabindex="-1" aria-labelledby="abmModalLabel" aria-hidden="true">
      <div class="modal-dialog">
          <div class="modal-content">
              <div class="modal-header">
                  <h5 class="modal-title" id="abmModalLabel">Agregar/Editar Mesa</h5>
                  <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                  <asp:Label ID="lblMesaId" runat="server" Text="ID de Mesa" Visible="false"></asp:Label>
                  <asp:TextBox ID="txtMesaId" runat="server" Visible="false"></asp:TextBox>
                  <div class="mb-3">
                      <asp:Label ID="lblNumeroMesa" runat="server" Text="Número de Mesa"></asp:Label>
                      <asp:TextBox ID="txtNumeroMesa" runat="server" CssClass="form-control"></asp:TextBox>
                  </div>
                  <div class="mb-3">
                      <asp:Label ID="lblCapacidad" runat="server" Text="Capacidad de Comensales"></asp:Label>
                      <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control"></asp:TextBox>
                  </div>
              </div>
              <div class="modal-footer">
                  <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                  <button type="button" class="btn btn-primary" onclick="guardarMesa()">Guardar</button>
              </div>
          </div>
      </div>
  </div>

  <%-- Botón invisible para realizar el postback desde JavaScript --%>
  <asp:Button ID="btnGuardarMesa" runat="server" OnClick="GuardarMesa" Style="display: none;" />
</asp:Content>
