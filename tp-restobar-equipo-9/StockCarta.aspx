<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StockCarta.aspx.cs" Inherits="tp_restobar_equipo_9.StockCarta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <% if (Estado)
        { %>
          <div class="container-md">
            <asp:Panel runat="server" ID="Panel1">
                <div class="mb-3 row">
                    <label class="col-sm-2 col-form-label" for="TxtNombre">Nombre producto</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="mb-3 row">
                    <label class="col-sm-2 col-form-label"  for="TxtCantidad">Cantidad</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="TxtCantidad" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                </div>
                <div class="mb-3 row">
                    <label class="col-sm-2 col-form-label" for="ddlUnidad">Unidad</label>
                    <div class="col-sm-10">
                        <asp:DropDownList runat="server" ID="ddlUnidad" CssClass="form-control" />
                    </div>
                </div>
                <div class="mb-3 row">
                    <label class="col-sm-2 col-form-label" for="ddlTipo">Tipo</label>
                    <div class="col-sm-10">
                        <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control" />
                    </div>
                </div>
                <div class="mb-3 row">
                    <label class="col-sm-2 col-form-label" for="TxtCargarImagen">Cargue la imagen del producto</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="TxtCargarImagen" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="mb-3 row">
                    <label class="col-sm-2 col-form-label" for="TxtPrecio">Precio</label>
                    <div class="col-sm-10 input-group">
                        <asp:TextBox ID="TxtPrecio" runat="server" CssClass="form-control" aria-label="Dollar amount (with dot and two decimal places)" />
                        <span class="input-group-text">$</span>
                        <span class="input-group-text">0.00</span>
                    </div>
                </div>
                <div class="mb-3 row">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-10">
                        <asp:Button ID="bttAceptar" Text="Aceptar" runat="server" CssClass="btn btn-primary me-2" OnClick="bttAceptar_Click" />
                        <asp:Button ID="bttEliminarModItem" Text="Eliminar" runat="server" CssClass="btn btn-danger" OnClick="bttEliminar_Click1" />
                    </div>
                </div>
            </asp:Panel>
        </div>
    <%}
        else
        {
            if (!Estado)
            { %>

    <asp:Button ID="bttAgregarItem" Text="Nuevo Producto +" runat="server" CssClass="btn btn-primary" OnClick="bttAgregarItem_Click" />
    <asp:Repeater runat="server" ID="repRepetidor">
        <HeaderTemplate>
            <div class="container text-center">
                <div class="row">
        </HeaderTemplate>

        <ItemTemplate>
            <div class="col-lg-4 col-md-6 mb-4">
                <div class="card" style="width: 18rem;">
                    <img src='<%#Eval("UrlImagen") %>' class="card-img-top" alt="...">
                    <div class="card-body">
                        <h5 class="card-title"><%#Eval("Nombre") %></h5>
                        <p class="card-text"><%#Eval("Cantidad") %>" "<%#Eval("Unidad") %></p>
                        <p class="card-text">"$ "$<%#Eval("Precio") %></p>
                        <p class="card-text">"Categoria: "<%#Eval("Tipo") %></p>
                        <asp:Button ID="bttModificar" Text="Modificar" runat="server" CssClass="btn btn-primary" CommandArgument='<%#Eval("IdProducto") %>' CommandName="idProducto" OnClick="bttModificar_Click" />
                        <asp:Button ID="bttEliminar" Text="Eliminar" runat="server" CssClass="btn btn-danger" CommandArgument='<%#Eval("IdProducto") %>' CommandName="idProducto" OnClick="bttEliminar_Click" />
                    </div>
                </div>
            </div>
        </ItemTemplate>

        <FooterTemplate>
            </div>
        </div>
   
        </FooterTemplate>

    </asp:Repeater>
    <%}
        } %>

</asp:Content>
