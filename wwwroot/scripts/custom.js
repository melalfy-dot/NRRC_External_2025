$(document).ready(function () {
  
  if ($(window).width() > 992) {
    $(".navbarMenu  .dropdown").hover(
      function () {
        $(this).find(".dropdown-menu").first().stop(true, true).slideDown(150);
        $(this).children(".dropdown-toggle").addClass("active");
      },
      function () {
        $(this).find(".dropdown-menu").first().stop(true, true).slideUp(105);
        $(this).children(".dropdown-toggle").removeClass("active");
      }
    );
  }
  $(".toggleOptions").click(function (e) {
    e.preventDefault();
    $(".navActions").toggleClass("show");
  });
  
});
