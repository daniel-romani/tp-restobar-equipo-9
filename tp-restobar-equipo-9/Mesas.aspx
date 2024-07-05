<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Mesas.aspx.cs" Inherits="tp_restobar_equipo_9.Mesas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function Btn_decreaseComensal(mesaId) {
            $.ajax({
                type: "POST",
                url: "Mesas.aspx/DecreaseComensal",
                data: JSON.stringify({ Id_Mesa: mesaId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        
                    } else {
                        alert("No se pueden quitar mas comensales");
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error:", error);
                    alert("Error al decrementar comensal.");
                }
            });
        }

        function Btn_increaseComensal(mesaId) {
            $.ajax({
                type: "POST",
                url: "Mesas.aspx/IncreaseComensal",
                data: JSON.stringify({ Id_Mesa: mesaId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        
                    } else {
                        alert("No se pueden agregar mas comensales");
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error:", error);
                    alert("Error al incrementar comensal.");
                }
            });
        }
        function AbrirPedido(Data){
            $.ajax({
                type: "POST",
                url: "Mesas.aspx/AbrirPedido",
                data: JSON.stringify({ mesa: Data }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {},
                error: function (xhr, status, error) {
                    console.error("Error:", error);
                    alert("Error al abrir el pedido.");
                }
            });
        }

        //function validarCheckout(comensales) {
        //    if (comensales === 0) {
        //        alert('No se puede hacer checkout sin comensales sentados.');
        //        return false;
        //    }
        //    return true;
        //}

        //Cuando tengamos finalizado lo de pedidos, utilizar este bloque de codigo:
        function validarCheckout(comensales, pedidoExistente) {
            if (comensales === 0) {
                alert('No se puede hacer checkout sin comensales sentados.');
                return false;
            }
            if (!pedidoExistente) {
                alert('No se puede hacer checkout sin un pedido hecho.');
                return false;
            }
            return true
        }
    </script>

    <div class="title">
        <h2>Mesas</h2>
    </div>
    <div class="container">
        <div class="row" id="mesasContainerDiv">
            <% foreach (Modelo.Mesa mesa in mesas)
               { %>
            <div class='col-md-3 mesa'>
                <div class="mesa-info">
                    <div class="datos">
                        <%if (mesa.Reservado)
                              { %>
                        <a><strong>Reservada</strong></a>
                        <%} %>
                        <div class="mesa-numero">
                            <a><strong>N°:</strong> <%= mesa.Id_Mesa %></a>
                        </div>
                        <div class="mesa-imagen">
                            <img src='Resources/mesa.png' alt='Mesa' />
                        </div>
                        <div class="mesa-capacidad <%if (mesa.Capacidad > mesa.ComensalesSentados)
                                { %> mesa-capacidad-verde <%}
                                else
                                { %> mesa-capacidad-rojo <%} %>">
                            <i class='bx bx-user bx-sm'></i>
                            <a><%= mesa.ComensalesSentados%> / <%= mesa.Capacidad%></a>
                        </div>
                    </div>
                </div>
                <div class="btn-container">
                    <div class="dropdown">
                        <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                            <i class='bx bx-user-plus'></i>Sentar Comensales
                        </button>
                        <ul class="dropdown-menu dropdown-menu-end">
                            <li id="botones">
                                <div>
                                    <button class="btn btn-secondary" onclick="Btn_decreaseComensal(<%= mesa.Id_Mesa %>)">-</button>
                                </div>
                                <span><%= mesa.ComensalesSentados %></span>
                                <button class="btn btn-secondary" onclick="Btn_increaseComensal(<%= mesa.Id_Mesa %>)">+</button>
                            </li>
                        </ul>
                    </div>

                    <%--<asp:Button ID="Btn_hacer_pedido" runat="server" CssClass="btn btn-info" OnClick="Btn_hacer_pedido_Click" Text="Hacer Pedido" Visible="true" data-mesa="m"/>--%>
                    <button type="button" id="btnmodal" class="btn btn-primary" data-toggle="modal" data-target="#mod_HacerPedido" data-idmesa="<%= mesa.Id_Mesa %>" data-idadmin="<%= mesa.Id_Admin %>" data-idmesero="<%= mesa.Id_Mesero %>">Hacer Pedido</button>


                    <%--<a href='Checkout.aspx?mesaId=<%= mesa.Id_Mesa %>&nroComensales=<%= mesa.ComensalesSentados %>&pedido=<%= mesa.Pedido %>' class='btn btn-success' onclick="return validarCheckout(<%= mesa.ComensalesSentados %>, <%= mesa.Pedido != null ? "true" : "false" %>)"><i class='bx bx-dollar-circle'></i>CheckOut</a>
                        <%--Cuando tengamos finalizado lo de pedidos, utilizar esta linea de codigo:--%>
                    <a href='Checkout.aspx?mesaId=<%= mesa.Id_Mesa %>&nroComensales=<%= mesa.ComensalesSentados %>&pedido=<%= mesa.Pedido %>' class='btn btn-success' onclick="return validarCheckout(<%= mesa.ComensalesSentados %>, <%= mesa.Pedido != null ? "true" : "false" %>)"><i class='bx bx-dollar-circle'></i>CheckOut</a>
                </div>
            </div>
            <% } %>
        </div>
    </div>
    <script>
                    $(document).on("click", "#btnmodal", function () {
                        var idmesa = $(this).data('idmesa')
                        var idadmin = $(this).data('idadmin')
                        var idmesero = $(this).data('idmesero')
                        var data = {Id_Mesa : idmesa, Id_Admin : idadmin, Id_Mesero : idmesero}
                        $("#inputmesa").val(idmesa);
                        AbrirPedido(data);
                        $('#mod_HacerPedido').modal('show');
                    })
    </script>

    <div class="container">
        <%--MODAL HACER PEDIDO--%>
        <div class="modal fade" id="mod_HacerPedido" role="dialog" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="staticBackdropLabel">Carga de pedido</h1>
                        <label>Mesa: </label>
                        <input type="text" id="inputmesa" name="">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <div class="row">
                                <asp:Repeater runat="server" ID="repRepetidor">
                                    <ItemTemplate>
                                        <div class="col">
                                            <div class="card" style="width: 18rem;">
                                                <img src='<%#Eval("UrlImagen") %>' class="card-img-top" alt="...">
                                                <div class="card-body">
                                                    <updatepanel>
                                                        <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                                        <p class="card-text"><%#Eval("Cantidad") %> <%#Eval("Unidad") %></p>
                                                        <p class="card-text">$ <%#Eval("Precio") %></p>
                                                        <p class="card-text">"Categoria: "<%#Eval("Tipo") %></p>
                                                        <asp:Button ID="bttAgregar" Text="Agregar" runat="server" CssClass="btn btn-primary" CommandArgument='<%#Eval("IdProducto") %>' CommandName="idProducto" OnClick="btn_AgregarItem_Click" />
                                                    </updatepanel>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="modalFooter" runat="server" class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="Btn_HacerPedidoConfirmar" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="Btn_HacerPedidoConfirmar_Click" />
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
