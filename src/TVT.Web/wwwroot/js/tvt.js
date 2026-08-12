$(function () {

    $('#rm-menu > nav > .rm-menu-list > .rm-menu-list-item').hover(
        function () {

            $('#rm-menu > nav > .rm-menu-list > .rm-menu-list-item')
                .removeClass('active');

            $(this).addClass('active');

        },
        function () {

        }
    );

});
