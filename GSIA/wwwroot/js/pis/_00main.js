$(document).ready(function () {

 
    // ---- Call Functions -------------
    _00DocumentIsClicked();
    _01ToggleHdrDropDown();
    _02ToggleLeftMenu();
    _05ToggleSubMenus();
    _08MakeMenuActiveByUrl();
    _09AddTooltipWhenHovered();

    
    function _00DocumentIsClicked() {
        $(document).on("click", function () {
            $(".nav-dd-dtls").hide();
        });
    }

    function _01ToggleHdrDropDown() {
        $('.nav-dd-user-img').click(function (event) {
            event.stopPropagation();
            $('.nav-dd-dtls').toggle();
        })
    };

    function _02ToggleLeftMenu() {
        $('.lm-hdr-icon').click(function (event) {
            event.stopPropagation();
            $('#pisLeft').toggleClass('lm-collapse');
            if ($('#pisLeft').hasClass('lm-collapse')) {
                _03LeftMenuIsCollapsed();

            } else {
                _04LeftMenuIsExpanded();

                //Expand Submenu if Menu is Active
                $(".lmd-content .lmd-hdr").each(function () {
                    if ($(this).hasClass("lmd-isActive-Menu")) {
                        $(this).parent().children('.lmd-subcontent-wrapper').children('.lmd-subcontent').removeClass('hide');
                        $(this).parent().children('.lmd-hdr-icon2').children("span:first-of-type").removeClass('hide');
                        $(this).parent().children('.lmd-hdr-icon2').children("span:last-of-type").hide();
                    }
                });
            }
        })
    }

    function _03LeftMenuIsCollapsed() {
        $('.lm-hdr-text').hide();
        $('.lm-hdr-icon:first').hide();
        $('.lm-hdr-icon:last').removeClass('hide');
        $('.lm-category').hide();
        $('.lmd-hdr-text').hide();
        $('.lmd-subcontent').addClass("hide");
        $('.lmd-hdr-icon2').children("span:first-of-type").addClass('hide');
        $('.lmd-hdr-icon2').children("span:last-of-type").hide();
    }

    function _04LeftMenuIsExpanded() {
        $('.lm-hdr-text').show();
        $('.lm-hdr-icon:first').show();
        $('.lm-hdr-icon:last').addClass('hide');
        $('.lm-category').show();
        $('.lmd-hdr-text').show();
        $('.lmd-hdr-icon2').children("span:first-of-type").addClass('hide');
        $('.lmd-hdr-icon2').children("span:last-of-type").show();
    }

    function _05ToggleSubMenus() {
        $('.lmd-hdr').click(function (event) {
            event.stopPropagation();

            let up = $(this).parent().children('.lmd-hdr-icon2').children("span:first");
            let dwn = $(this).parent().children('.lmd-hdr-icon2').children("span:last");

            let dwnIsVisible = $(dwn).is(":visible");

            let ups = $('.lmd-hdr-icon2').children("span:first-of-type");
            let dwns = $('.lmd-hdr-icon2').children("span:last-of-type");

            ups.addClass('hide');
            dwns.show();

            let submenus = $('.lmd-subcontent');
            submenus.addClass("hide");

            let submenu = $(this).parent().children('.lmd-subcontent-wrapper').children('.lmd-subcontent');
            _07RemoveActiveClassToMenu();

            //Check if Left Menu is Collapsed;
            if (!$('#pisLeft').hasClass('lm-collapse')) {

                _06AddActiveClassToMenu($(this));

                if (dwnIsVisible) {
                    up.removeClass('hide');
                    dwn.hide();

                    submenu.removeClass('hide');
                    _06AddActiveClassToMenu($(this));

                } else {
                    console.log('false')
                    up.addClass('hide');
                    dwn.show();
                    if ($(this).parent().children('.lmd-hdr-icon2').length > 0) {
                        _07RemoveActiveClassToMenu();
                    }
                }
            } else {
                $('#pisLeft').removeClass('lm-collapse');
                dwnIsVisible = true;
                _04LeftMenuIsExpanded();
                if (dwnIsVisible) {
                    up.removeClass('hide');
                    dwn.hide();

                    submenu.removeClass('hide');
                    _06AddActiveClassToMenu($(this));
                }
            }
        });
    }
    function _06AddActiveClassToMenu(el) {

        let menuWrapper = $(el);
        let menuText = $(el).children('.lmd-hdr-text');
        let menuIcon = $(el).children('.lmd-hdr-icon1');

        menuWrapper.addClass('lmd-isActive-Menu')

        menuText.addClass('lmd-isActive-text');
        menuIcon.addClass('lmd-isActive-icon');
    };

    function _07RemoveActiveClassToMenu() {
        $('.lmd-hdr').removeClass('lmd-isActive-Menu')
        $('.lmd-hdr').children('.lmd-hdr-text').removeClass('lmd-isActive-text');
        $('.lmd-hdr').children('.lmd-hdr-icon1').removeClass('lmd-isActive-icon');
    };

    function _08MakeMenuActiveByUrl() {
        var currentpath = location.pathname;
        $('.lm-body .lm-dtls .lmd-content').each(function () {
            if ($(this).children('.lmd-subcontent-wrapper').length > 0) {
                $('.lm-body .lm-dtls .lmd-content .lmd-subcontent-wrapper .lmd-subcontent a ').each(function () {
                    let submenuPath = $(this);
                    // if the current path is like this link, make it active
                    if (submenuPath.attr('href').indexOf(currentpath) !== -1) {
                        _06AddActiveClassToMenu(submenuPath.parent().parent().parent().children('.lmd-hdr'));
                        submenuPath.parent().parent().children('.lmd-subcontent').removeClass('hide');
                        submenuPath.parent().parent().parent().children('.lmd-hdr-icon2').children("span:first").removeClass('hide');;
                        submenuPath.parent().parent().parent().children('.lmd-hdr-icon2').children("span:last").hide();
                        submenuPath.addClass('lmd-subcontent-isActive');
                    }   

                })
            } else {
                $('.lm-body .lm-dtls .lmd-content  a ').each(function () {
                    let menuPath = $(this);
                    // if the current path is like this link, make it active
                    if (menuPath.attr('href').indexOf(currentpath) !== -1) {
                      return _06AddActiveClassToMenu($(this));
                    } 
                })
            }
        })
    };

    function _09AddTooltipWhenHovered(){
        $(".lmd-hdr").hover(function () {
            if ($('#pisLeft').hasClass('lm-collapse')) {
                $(this).css('cursor', 'pointer').attr('myitle');
            }
           
        });
    }

    




})
