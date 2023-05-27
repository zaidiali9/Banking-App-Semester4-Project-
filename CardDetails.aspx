<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardDetails.aspx.cs" Inherits="Lab_Project_Final.CardDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cards Details</title><!-- Favicons -->
    <link href="assets/img/favicon.png" rel="icon">
    <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon" />
    <!-- Google Fonts -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,300;0,400;0,500;0,600;0,700;1,300;1,400;1,600;1,700&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;1,300;1,400;1,500;1,600;1,700&family=Raleway:ital,wght@0,300;0,400;0,500;0,600;0,700;1,300;1,400;1,500;1,600;1,700&display=swap" rel="stylesheet">
    <!-- Vendor CSS Files -->
    <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet">
    <link href="assets/vendor/aos/aos.css" rel="stylesheet">
    <link href="assets/vendor/glightbox/css/glightbox.min.css" rel="stylesheet">
    <link href="assets/vendor/swiper/swiper-bundle.min.css" rel="stylesheet">
    <!-- Template Main CSS File -->
    <link href="assets/css/main.css" rel="stylesheet">
    <link href="Lib/fontcss.css" rel="stylesheet" />
    <link href="Lib/bootstrap.min.css" rel="stylesheet" />
    <link href="card.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <header id="header" class="header d-flex align-items-center">

            <div class="container-fluid container-xl d-flex align-items-center justify-content-between">
                <a href="main.aspx" class="logo d-flex align-items-center">
                    <!-- Uncomment the line below if you also wish to use an image logo -->
                    <img src="assets/img/logo.png" alt="">
                    <h1>KingMorgan<span>.</span></h1>
                </a>
                <nav id="navbar" class="navbar">
                    <ul>
                        <li><a href="main.aspx">Home</a></li>
                        <!--<li><a href="#about">About</a></li>-->
                        <li><a href="#services">Cards</a></li>
                        <li class="dropdown"><a href="#"><span>Settings</span> <i class="bi bi-chevron-down dropdown-indicator"></i></a>
                            <ul>
                                <li><a href="#">Dispute Center</a></li>
                                <li><a href="#">Help</a></li>
                                <li><a href="LoginSignup.aspx">Logout</a></li>
                            </ul>
                        </li>
                    </ul>
                </nav><!-- .navbar -->

                <i class="mobile-nav-toggle mobile-nav-show bi bi-list"></i>
                <i class="mobile-nav-toggle mobile-nav-hide d-none bi bi-x"></i>

            </div>
        </header><!-- End Header -->
        <!-- End Header -->
        
        <!-- ======= Hero Section ======= -->
  <section id="hero" class="hero">
    <div class="container position-relative">
      <div class="row gy-5" data-aos="fade-in">
        <div class="col-lg-6 order-2 order-lg-1 d-flex flex-column justify-content-center text-center text-lg-start">
          <h2>Welcome to <span>Cards</span></h2>
        </div>
        <div class="col-lg-6 order-1 order-lg-2">
          <img src="assets/img/hero-img.svg" class="img-fluid" alt="" data-aos="zoom-out" data-aos-delay="100">
        </div>
      </div>
    </div>
  </section>
  <!-- End Hero Section -->
         <!-- ======= Our Services Section ======= -->
    <section id="services" class="services sections-bg">
      <div class="container" data-aos="fade-up">

        <div class="section-header">
          <h2>Cards</h2>
        </div>

        <div class="row gy-4" data-aos="fade-up" data-aos-delay="100">


          <div class="col-lg-4 col-md-6">
              <div class="upper">
                          <div class="card" style="background-color: #008374">
                              <div class="details">
                                  <div class="d-flex flex-column card-details">
                                      <span class="plus">Platinum Plus</span>
                                      <h4 class="amount">$ <asp:Literal ID="balance" runat="server"></asp:Literal></h4>
                                  </div>
                                  <img src="Sources/wifisig.png" width="40" class="coral">
                              </div>
                              <div class="d-flex justify-content-between mt-3 align-items-center card-number">
                                  <img src="Sources/sim.png" width="30">
                                  <div class="d-flex flex-column number">
                                      <span><asp:Literal ID="Cardnumber" runat="server"></asp:Literal></span>
                                      <span class="text-right">11/33</span>
                                  </div>
                              </div>
                          </div>
                      </div>
              <%--<div class="service-item position-relative">
              <div class="icon">
                <i class="bi bi-broadcast"></i>
              </div>
              <h3> Bill payments</h3>
            </div>--%>
          </div><!-- End Service Item -->
           <%-- <div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Donations</h3>
                </div>
            </div><!-- End Service Item -->
            <div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Transections</h3>
                </div>
            </div><!-- End Service Item -->
            <div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Card Details</h3>
                </div>
            </div><!-- End Service Item -->
            <div class="col-lg-4 col-md-6"> 
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Account Details</h3>
                </div>
            </div><!-- End Service Item -->
            <div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Mobile Topup</h3>
                </div>
            </div><!-- End Service Item -->
            <div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Loans</h3>
                </div>
            </div><!-- End Service Item -->
            <div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Traveling</h3>
                </div>
            </div><!-- End Service Item -->
            <div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Investments</h3>
                </div>
            </div><!-- End Service Item -->
            <div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Cheques</h3>
                </div>
            </div><!-- End Service Item -->
            <div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Budget Planner</h3>
                </div>
            </div><!-- End Service Item -->
            <div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <a href="">
                    <h3>Foreign Currency</h3>
                    </a>
                </div>
            </div>--%><!-- End Service Item -->
            <%--<div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Eosle Commodi</h3>
                    <p>Ut autem aut autem non a. Sint sint sit facilis nam iusto sint. Libero corrupti neque eum hic non ut nesciunt dolorem.</p>
                    <a href="#" class="readmore stretched-link">Read more <i class="bi bi-arrow-right"></i></a>
                </div>
            </div><!-- End Service Item -->
            <div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Eosle Commodi</h3>
                    <p>Ut autem aut autem non a. Sint sint sit facilis nam iusto sint. Libero corrupti neque eum hic non ut nesciunt dolorem.</p>
                    <a href="#" class="readmore stretched-link">Read more <i class="bi bi-arrow-right"></i></a>
                </div>
            </div><!-- End Service Item -->
            <div class="col-lg-4 col-md-6">
                <div class="service-item position-relative">
                    <div class="icon">
                        <i class="bi bi-broadcast"></i>
                    </div>
                    <h3>Eosle Commodi</h3>
                    <p>Ut autem aut autem non a. Sint sint sit facilis nam iusto sint. Libero corrupti neque eum hic non ut nesciunt dolorem.</p>
                    <a href="#" class="readmore stretched-link">Read more <i class="bi bi-arrow-right"></i></a>
                </div>
            </div><!-- End Service Item -->--%>

        </div>

      </div>
    </section><!-- End Our Services Section -->

        <!-- Vendor JS Files -->
        <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
        <script src="assets/vendor/aos/aos.js"></script>
        <script src="assets/vendor/glightbox/js/glightbox.min.js"></script>
        <script src="assets/vendor/purecounter/purecounter_vanilla.js"></script>
        <script src="assets/vendor/swiper/swiper-bundle.min.js"></script>
        <script src="assets/vendor/isotope-layout/isotope.pkgd.min.js"></script>
        <script src="assets/vendor/php-email-form/validate.js"></script>
    </form>
<script src="assets/js/main.js"></script>
<script src="Lib/popper.min.js"></script>
<script src="Lib/bootstrap.bundle.min.js"></script>
</body>
</html>
