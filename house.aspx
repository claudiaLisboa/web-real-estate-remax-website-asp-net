<%@ Page Language="C#" AutoEventWireup="true" CodeFile="house.aspx.cs" Inherits="house" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

    <title>A real estate broker or agency to buy or sell your house, condo, cottage, RE/MAX-Quebec</title>
    </head>
<body>
    <form id="form1" runat="server">
    <div class="container">
    <div class="header">
        <div id="logo">
            <a href="index.aspx">
            <img src="imgs/logo.png" width="200px" alt=""  ""/>
            </a>
        </div>
        <div id="logo2">
            <img src="imgs/myRemax.png" width="200px" alt="" />
        </div>
    </div>
    <div style="clear: both;"></div>
    <div id="middleRow" style="background-color:lightgrey; >
      
        <h3>HOUSE DETAILS</h3>
        <asp:Label ID="myLabel" runat="server"></asp:Label>
        <p>&nbsp;</p>
         <div id="myCarousel" class="carousel slide" data-ride="carousel">
          <!-- Indicators -->
          <ol class="carousel-indicators">
            <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
            <li data-target="#myCarousel" data-slide-to="1"></li>
            <li data-target="#myCarousel" data-slide-to="2"></li>
            <li data-target="#myCarousel" data-slide-to="3"></li>
          </ol>

          <!-- Wrapper for slides -->
          <div class="carousel-inner" role="listbox">
            <div class="item active">
              <img src="imgs/houses/sale/apartment.jpg"  height='300' width='300'>
            </div>

            <div class="item">
              <img src="imgs/houses/sale/sale1.jpg"  height='300' width='300'>
            </div>

            <div class="item">
              <img src="imgs/houses/sale/sale4.jpe"  height='300' width='300'>
            </div>

            <div class="item">
              <img src="imgs/houses/sale/ap1_2.jpg"  height='300' width='300'>
            </div>
          </div>

          <!-- Left and right controls -->
          <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
          </a>
          <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
            <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
          </a>

        </div>  
         <br />
        <a href="index.aspx">
        <h3><strong>BACK TO THE SEARCH PAGE</strong></h3>
        </a>
    </div>

    </div>

    </form>
</body>
</html>
