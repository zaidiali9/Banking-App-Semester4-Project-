<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreditCardDetails.aspx.cs" Inherits="Lab_Project_Final.CreditCardDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Card Details</title>
    <link href="Lib/bootstrap5.0.min.css" rel="stylesheet" />
    <link href="card.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="wrapper">
                <div class="upper">
                    <div class="card bg-dark">
                        <div class="details">
                            <div class="d-flex flex-column card-details">
                                <span class="plus">Platinum Plus</span>
                                <h4 class="amount">$54454.90</h4>
                            </div>
                            <img src="https://i.imgur.com/ZnvAG93.png" width="40" class="coral">
                        </div>
                        <div class="d-flex justify-content-between mt-3 align-items-center card-number">
                            <img src="https://i.imgur.com/xTeZOlU.png" width="30">
                            <div class="d-flex flex-column number">
                                <span>**** **** 9899</span>
                                <span class="text-right">11/33</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
    <script src="Lib/jquery.min.js"></script>
    <script src="Lib/bootstrap5.0.bundle.min.js"></script>
</body>
</html>
