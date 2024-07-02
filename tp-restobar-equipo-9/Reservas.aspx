<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Reservas.aspx.cs" Inherits="tp_restobar_equipo_9.Reservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Btn_Reservar_Click(idMesa, capacidadMesa) {
            document.getElementById('<%= hiddenFieldMesaId.ClientID %>').value = idMesa;
            document.getElementById('<%= hiddenFieldMesaCap.ClientID %>').value = capacidadMesa;
            $('#mod_Reserva').modal('show');
            return false;
        }

        function Btn_BajaReserva(reservaId, mesaId) {
            $.ajax({
                type: "POST",
                url: "Reservas.aspx/BajaReserva",
                data: JSON.stringify({ idReserva: reservaId, idMesa: mesaId}),
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
            <%if(!(usuario_actual.TipoUsuario == "Mesero")) 
                { %>
            
                <h2>Reservar Mesa</h2>
                <div class="container">
                        <div class="row" id="mesasContainerDiv">
                            <% foreach (Modelo.Mesa mesa in mesas)
                               {
                                    if (!mesa.Reservado)
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
                                                    <div class="mesa-capacidad mesa-capacidad-verde">
                                                        <i class='bx bx-user bx-sm'></i>
                                                        <a><%= mesa.Capacidad%></a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="btn-container">
                                                <button class="btn btn-success" onclick="return Btn_Reservar_Click(<%= mesa.Id_Mesa %>, <%= mesa.Capacidad %>)">Hacer una Reserva</button>
                                            </div>
                                        </div>
                                    <%}
                               }%>
                        </div>
                    </div>
                <hr />
             <% } %>

        <asp:HiddenField ID="hiddenFieldMesaId" runat="server" />
        <asp:HiddenField ID="hiddenFieldMesaCap" runat="server" />

        <div="container mt-5">
            
            <h2>Lista de reservas</h2>
            <div class="row" id="reservasContainerDiv">
                <% foreach (Modelo.Reserva reserva in reservas)
                    {
                        if (reserva.Estado)
                        {%>
                            <div class='col-md-3 mesa'>
                                <div class="mesa-info">
                                    <div class="datos">
                                            <strong>Mesa </strong>
                                            <div class="mesa-numero">
                                                <a><strong>N°:</strong> <%= reserva.Id_Mesa %></a>
                                            </div>
                                            <a><strong>Dni:</strong> <%= reserva.DniComensal %></a>
                                        <div class="mesa-imagen">
                                            <img src='Resources/mesa.png' alt='Mesa' />
                                        </div>
                                        <div class="mesa-capacidad mesa-capacidad-rojo">
                                            <i class="bx bx-user bx-sm"></i>
                                            <a><%= reserva.Cantidad_Comensales%></a>
                                        </div>
                                    </div>
                                </div>
                                    <div class="btn-container">
                                    <button class="btn btn-danger" onclick="Btn_BajaReserva(<%=reserva.Id %>, <%= reserva.Id_Mesa %>)">Dar de baja</button>
                                </div>
                            </div>
                     <% }
                   } %>
            </div>
        </div>

        <!-- MODAL RESERVA -->
        <div class="modal fade" id="mod_Reserva" tabindex="-1" role="dialog" aria-labelledby="reservaModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="reservaModalLabel">Reserva de Mesa</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <li class="list-group-item">
                            <asp:Label CssClass="fs-4 font-monospace" runat="server" Text="DNI del titular: "></asp:Label>
                            <asp:TextBox ID="txtDniComensal" CssClass="form-control" runat="server" onkeypress="return soloNumeros(event);" MaxLength="8"></asp:TextBox>
                            <asp:Label ID="lblErrorDniComensal" CssClass="lbl lbl_error" Text="El dni solo debe contener números" runat="server" Visible="false"></asp:Label>
                        </li>
                        <li class="list-group-item">
                            <asp:Label CssClass="fs-4 font-monospace" runat="server" Text="Cantidad de comensales: "></asp:Label>
                            <asp:TextBox ID="txtCantComensal" CssClass="form-control" runat="server" onkeypress="return soloNumeros(event);" MaxLength="1"></asp:TextBox>
                            <asp:Label ID="lblErrorComensal" CssClass="lbl lbl_error" Text="La cantidad solo debe contener números" runat="server" Visible="false"></asp:Label>
                        </li>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btnReservar" runat="server" CssClass="btn btn-primary" Text="Reservar" OnClick="btnReservar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
