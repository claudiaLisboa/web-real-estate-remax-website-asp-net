<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="css/default.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="css/nivo-slider.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="css/style.css" type="text/css" media="screen" />
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="css/estilo.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="bootstrap/js/jquery.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="js/jquery.nivo.slider.js"></script>
    <script type="text/javascript">
    $(window).load(function () {
        $('#slider').nivoSlider();
    });
    
    //function reload page when click in remax logo
    function ReloadPage()
    {
         window.location = window.location.href.split('?')[0] + '?t=' + new Date().getTime();            
    }

    </script>
    <title>A real estate broker or agency to buy or sell your house, condo, cottage, RE/MAX-Quebec</title>
    <style type="text/css">
        /*.auto-style1 {
            height: 82px;
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <div id="logo">
                    <img src="imgs/logo.png" width="200px" alt="" onclick="ReloadPage();"/>
                </div>
                <div id="logo2">
                    <img src="imgs/myRemax.png" width="200px" alt="" />
                </div>
            </div>
            <div style="clear: both;"></div>
            <div id="middleRow" style="background-color:lightgrey; height:300px">
                <div class="col-md-8">
                    <ul id="myTabs" class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#home">Search a House</a></li>
                        <li><a data-toggle="tab" href="#menu1">Search an Agent</a></li>
                        <li><a data-toggle="tab" href="#menu2">Search House by Agent</a></li>
                    </ul>
                    <div class="tab-content">
                        <div id="home" class="tab-pane fade in active">
                            <table class="nav-justified table-responsive">
                                <tr>
                                    <td class="auto-style1">
                                        <div class="form-group">
                                            <asp:Label ID="lblType" runat="server" Text="Type: "></asp:Label>
                                            <asp:DropDownList ID="cboType" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td class="auto-style1">
                                        <div class="form-group">
                                            <asp:Label ID="lblBedrooms" runat="server" Text="Number of Bedrooms: "></asp:Label>
                                            <asp:DropDownList ID="cboBedrooms" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="-- Choose --" Value="" />
                                                <asp:ListItem Text="1" Value="1" />
                                                <asp:ListItem Text="2" Value="2" />
                                                <asp:ListItem Text="3" Value="3" />
                                                <asp:ListItem Text="4" Value="4" />
                                                <asp:ListItem Text="5" Value="5" />
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td class="auto-style1">
                                        <div class="form-group">
                                            <asp:Label ID="lblPriceMin" runat="server" Text="Price Min:"></asp:Label>
                                            <asp:DropDownList ID="cboPriceMin" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="$0" Value="0" />
                                                <asp:ListItem Text="$100,000" Value="100000" />
                                                <asp:ListItem Text="$200,000" Value="200000" />
                                                <asp:ListItem Text="$300,000" Value="300000" />
                                                <asp:ListItem Text="$400,000" Value="400000" />
                                                <asp:ListItem Text="$500,000" Value="500000" />
                                                <asp:ListItem Text="$600,000" Value="600000" />
                                                <asp:ListItem Text="$700,000" Value="700000" />
                                                <asp:ListItem Text="$800,000" Value="800000" />
                                                <asp:ListItem Text="$900,000" Value="900000" />
                                                <asp:ListItem Text="$1,000,000" Value="1000000" />
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td class="auto-style1">
                                        <div class="form-group">
                                            <asp:Label ID="lblPriceMax" runat="server" Text="Price Max:"></asp:Label>
                                            <asp:DropDownList ID="cboPriceMax" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="$0" Value="0" />
                                                <asp:ListItem Text="$100,000" Value="100000" />
                                                <asp:ListItem Text="$200,000" Value="200000" />
                                                <asp:ListItem Text="$300,000" Value="300000" />
                                                <asp:ListItem Text="$400,000" Value="400000" />
                                                <asp:ListItem Text="$500,000" Value="500000" />
                                                <asp:ListItem Text="$600,000" Value="600000" />
                                                <asp:ListItem Text="$700,000" Value="700000" />
                                                <asp:ListItem Text="$800,000" Value="800000" />
                                                <asp:ListItem Text="$900,000" Value="900000" />
                                                <asp:ListItem Text="$1,000,000" Value="1000000" />
                                                <asp:ListItem Text="Unlimited" Value="100000000" />
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="form-group">
                                            <asp:Label ID="lblHouseCity" runat="server" Text="City:"></asp:Label>
                                            <asp:DropDownList ID="cboHouseCity" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="form-group">
                                            <asp:Label ID="lblPostalCode" runat="server" Text="Postal Code:"></asp:Label>
                                            <asp:TextBox ID="txtPostalCode" runat="server" Width="109px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Button ID="btnSearchHouse" runat="server" Text="Search" OnClick="btnSearchHouse_Click" class="btn btn-primary"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="menu1" class="tab-pane fade">
                            <div class="form-group form-inline">
                                <asp:Label ID="lblGender" runat="server" Text="Gender:"></asp:Label>
                                <asp:DropDownList ID="cboGender" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:Label ID="lblAgentCity" runat="server" Text="City:"></asp:Label>
                                <asp:DropDownList ID="cboAgentCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:Button ID="btnSearchAgent" runat="server" Text="Search" OnClick="btnSearchAgent_Click" class="btn btn-primary"/>
                            </div>
                        </div>
                        <div id="menu2" class="tab-pane fade">
                            <p><asp:Label ID="Label8" runat="server" Text="Choose an Agent:"></asp:Label></p>
                            <asp:ListBox ID="ListBoxAgent" runat="server" OnSelectedIndexChanged="ListBoxAgent_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4" style="float:left">
                    <div class="slider-wrapper theme-default">
                        <div id="slider" class="nivoSlider">
                            <img src="imgs/collection_en.jpg" width="300px" alt="" />
                            <img src="imgs/oes_billets_2015_en.jpg" width="300px" alt=""  />
                            <img src="imgs/tranquillit_en.jpg" width="300px" alt=""  />
                        </div>
                    </div>
                </div>
            </div>
        <div style="clear: both;"> </div>
        <div id="middleRow">
            <asp:GridView ID="GridResult" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" GridLines="None" CellSpacing="1" Width="321px">
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#33276A" />
            </asp:GridView>
            <asp:Literal ID="LiteralResult" runat="server"></asp:Literal>
        </div>
        <div style="clear: both;"></div>
        <div id="footer">
            <img src="imgs/footer.png"  alt="" />
        </div>
    </div>
</form>
</body>
</html>
