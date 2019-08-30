<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PetsData.aspx.cs" Inherits="PetsData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <div class="row">
        <button id="btnCargar">Refresh data</button>
    </div>
    <div class="row">
        <asp:Table ID="tblDatapets" runat="server">

        </asp:Table>

    </div>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
