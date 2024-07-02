<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="tp_restobar_equipo_9.Configuracion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">  
        function Btn_Asignar_Click(idMesa) {
            document.getElementById('<%= hiddenFieldMesaId.ClientID %>').value = idMesa;
            $('#mod_Asignar').modal('show');
            return false;
        }

        function Btn_BajaAsignacion(mesaId) {
            $.ajax({
                type: "POST",
                url: "Configuracion.aspx/BajaAsignacion",
                data: JSON.stringify({ idMesa: mesaId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <style>
            .title {
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

            .btn-container {
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

        <div class="container mt-5">
            <h2>Mesas disponibles para asignacion</h2>
            <div class="container">
                    <div class="row" id="mesasContainerDiv">
                        <% foreach (Modelo.Mesa mesa in mesas)
                            {
                                if (mesa.Id_Mesero == -1)
                                {%>
                                    <div class='col-md-3 mesa'>
                                        <div class="mesa-info">
                                            <div class="datos">
                                                <div class="mesa-numero">
                                                    <a><strong>N°:</strong> <%= mesa.Id_Mesa %></a>
                                                </div>
                                                <div class="mesa-imagen">
                                                    <img src='Resources/mesa.png' alt='Mesa' />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="btn-container">
                                            <button class="btn btn-success" onclick="return Btn_Asignar_Click(<%= mesa.Id_Mesa %>)">Asignar Mesa</button>
                                        </div>
                                    </div>
                                <%}
                            }%>
                    </div>
                </div>
            <hr />

            <asp:HiddenField ID="hiddenFieldMesaId" runat="server" />

            <div="container mt-5">
                <h2>Mesas Asignadas</h2>
                <div class="row" id="reservasContainerDiv">
                    <% foreach (Modelo.Mesa mesa in mesas)
                        {
                            if (!(mesa.Id_Mesero == -1))
                            {%>
                                <div class='col-md-3 mesa'>
                                    <div class="mesa-info">
                                        <div class="datos">
                                                <strong>Mesa </strong>
                                                <div class="mesa-numero">
                                                    <a><strong>N°:</strong> <%= mesa.Id_Mesa %></a>
                                                </div>
                                                <a><strong>Mesero: <%= mesa.MeseroAsignado.Apellido %>, <%= mesa.MeseroAsignado.Nombre %></strong></a>
                                            <div class="mesa-imagen">
                                                <img src='Resources/mesa.png' alt='Mesa' />
                                            </div>
                                        </div>
                                    </div>
                                        <div class="btn-container">
                                        <button class="btn btn-danger" onclick="Btn_BajaAsignacion(<%= mesa.Id_Mesa %>)">Quitar asignacion</button>
                                    </div>
                               </div>
                         <% }
                       } %>
                </div>
            </div>

            <!-- MODAL ASIGNAR -->
            <div class="modal fade" id="mod_Asignar" tabindex="-1" role="dialog" aria-labelledby="reservaModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="reservaModalLabel">Asignar mesa</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <li class="list-group-item">
                                <asp:Label CssClass="fs-4 font-monospace" runat="server" Text="DNI del Mesero: "></asp:Label>
                                <asp:TextBox ID="txtDniMesero" CssClass="form-control" runat="server" onkeypress="return soloNumeros(event);" MaxLength="8"></asp:TextBox>
                                <asp:Label ID="lblErrorDniMesero" CssClass="lbl lbl_error" Text="El dni solo debe contener números" runat="server" Visible="false"></asp:Label>
                            </li>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                            <asp:Button ID="btnReservar" runat="server" CssClass="btn btn-primary" Text="Asignar" OnClick="btnAsignar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
