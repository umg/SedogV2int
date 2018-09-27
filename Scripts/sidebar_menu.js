function initMenu() {
    $('#menu ul').hide();
    $('#menu ul').children('.current').parent().show();
    $('#menu li a').click(
      function (event) {
          var checkElement = $(this).next();
          if ((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
              $('#menu ul:visible').slideUp('normal');
              checkElement.slideDown('normal');
              return false;
          }
          if ((checkElement.is('ul')) && (checkElement.is(':visible'))) {
              //$('#menu ul:visible').slideUp('normal');
              return false;
          }          
      }
      );
    $('#menu li a').each(function () {
        var checkElement = $(this).next();
        if (checkElement.is('ul')) {
            $(this).addClass("subMenu").append("<span class='fa-stack fa-lg pull-right'><i class='fa fa-angle-down fa-stack-1x'></i></span>");
        }
    });
    //$("#wrapper #sidebar-wrapper").hover(
    //    function () { }, function () { $('#menu ul:visible').slideUp('normal'); }
    //    );

}
$(document).ready(function () {
       
    $("#wrapper #sidebar-wrapper").hover(function() {
        $(this).width(250);
    });
    $("#wrapper #page-content-wrapper, .navbar").hover(function () {
        $("#wrapper #sidebar-wrapper").width(50);
        $('#menu ul:visible').slideUp('normal');
    });
    initMenu();
});