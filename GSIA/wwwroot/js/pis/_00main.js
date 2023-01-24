$(document).ready(function () {

    function _01ToggleMenuInSmallScreen() {
        $("#menu-toggle-icon").click(function () {
            $(this).toggleClass("active");
            $("#pisLeft").toggleClass("active");
            $(".lmd-content").removeClass("active");
            _03MakeMenuActiveByUrl()
        });
    };

    function _02ToggleSubMenu() {
        $('.lmd-hdr').click(function (event) {
            event.stopPropagation();
            let dwn = $(this).parent().children('.lmd-hdr-icon2').children("i:last");
            let dwnIsVisible = $(dwn).is(":visible");

            $('.lmd-content').removeClass("active");
            $(this).parent().toggleClass("active");

            if (dwnIsVisible) {
                $(this).parent().addClass("active");
            } else {
                $(this).parent().removeClass("active");
            }
        });
    };

    function _03MakeMenuActiveByUrl() {
        var currentpath = location.pathname;
        $('.lm-body .lm-dtls .lmd-content').each(function () {
            if ($(this).children('.lmd-subcontent-wrapper').length > 0) {
                $('.lm-body .lm-dtls .lmd-content .lmd-subcontent-wrapper .lmd-subcontent .lmd-subcontent-item ').each(function () {
                    let submenuPath = $(this);
                    // if the current path is like this link, make it active
                    if (submenuPath.attr('href').indexOf(currentpath) !== -1) {
                        $(this).addClass('active');
                        $(this).parent().parent().parent().addClass("active");
                    }

                })
            } 
        })
    };

    function _04ToggleLeftMenuInLargeScreen(){
        $("#lm-toggle-menu").click(function () {
            $("#pisLeft").toggleClass("lg-active");
            $("#pisRight").toggleClass("pisLeftIsActive");

            
            $(".lmd-content").removeClass("active");
            _03MakeMenuActiveByUrl()
            
        })
    }

    function _05ExpandLeftTMenuWithMenuIcon() {
        $(".lmd-hdr").click(function () {

            if (!$("#pisLeft").hasClass("lg-active")) {
                $("#pisLeft").toggleClass("lg-active");
                $("#pisRight").toggleClass("pisLeftIsActive");
                $(this).parent().addClass("active");
            }
        })

       
    }

    function _06ToggleAvatarDropDown() {
        $("#toggle-avatar-icon").click(function (event) {
            event.stopPropagation();
            $(this).parent().toggleClass("active");
        })
    };

    function _07CloseAvatarDropDown() {
        $(document).on("click", function () {

         $(".avatar-wrapper").removeClass("active");
        });
    };

   


    _01ToggleMenuInSmallScreen();
    _02ToggleSubMenu();
    _03MakeMenuActiveByUrl();
    _04ToggleLeftMenuInLargeScreen(); // with the  left/right arrow icon
    _05ExpandLeftTMenuWithMenuIcon();
    _06ToggleAvatarDropDown();
    _07CloseAvatarDropDown();
})