<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PreventivoAuto.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Preventivo Auto</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <h1>Preventivo Auto</h1>
        <div class="container">
            <div class="row">
                <div class="dropdown col-md-6">
                    <label for="ddlAuto">Seleziona auto:</label>
                    <asp:DropDownList ID="ddlAuto" runat="server" 
                          DataSourceID="dsAuto" DataTextField="Modello" 
                          DataValueField="Id" OnSelectedIndexChanged="ddlAuto_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="dsAuto" runat="server" TypeName="PreventivoAuto.Default" SelectMethod="GetAuto">
                    </asp:ObjectDataSource>
                </div>
                <div class="col-md-6">
                    <asp:Image ID="imgAuto" runat="server" ImageUrl="#" />
                    <asp:Label ID="lblPrezzoBase" runat="server" Text="Prezzo base:"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h2>Optional</h2>
                    <asp:CheckBoxList ID="chkOptional" runat="server" 
                             DataSourceID="dsOptional" DataTextField="Descrizione" 
                             DataValueField="Id">
                    </asp:CheckBoxList>
                    <asp:ObjectDataSource ID="dsOptional" runat="server" 
                     SelectMethod="GetOptional" TypeName="PreventivoAuto.Default">
                   </asp:ObjectDataSource>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label for="txtNumeroAnniGaranzia">Numero anni garanzia:</label>
                    <asp:TextBox ID="txtNumeroAnniGaranzia" runat="server" 
                        Text="1" Width="50px"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="btn btn-secondary col-md-12">
                    <asp:Button ID="btnCalcola" runat="server" Text="Calcola preventivo" 
                        OnClick="btnCalcola_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="h3Totale" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
