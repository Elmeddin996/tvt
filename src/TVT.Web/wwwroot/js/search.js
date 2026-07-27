function clearLiveSearch() {

    $('#rm_livesearch_close').removeClass('visible');
    $('#rm_overlay').removeClass('active');
    $('#rm_overlay').removeClass('transparent');

    $('#rm_livesearch')
        .removeClass('expanded')
        .empty();

    $('#input_search')
        .val('')
        .removeClass('active');
}

$(function () {

    let timer;
    const delay = 500;

    $('#input_search').on('keyup', function (event) {

        switch (event.keyCode) {

            case 37:
            case 38:
            case 39:
            case 40:
                return;

            case 27:
                clearLiveSearch();
                return;
        }

        clearTimeout(timer);

        timer = setTimeout(function () {

            const value = $('#input_search').val().trim();

            if (value.length >= 2) {
                tvtSearch.search(value);
            }
            else if (value.length === 0) {
                clearLiveSearch();
            }

        }, delay);

    });

});

const tvtSearch = {

    search: function (key) {

        $.ajax({

            url: '/Search/LiveSearch',

            type: 'GET',

            data: {
                keyword: key
            },

            cache: false,

            success: function (result) {

                $('#rm_livesearch')
                    .html(result)
                    .addClass('expanded');

                if (result && result.length > 0) {

                    $('#rm_livesearch_close').addClass('visible');

                    $('#input_search, #rm_overlay')
                        .addClass('active');

                    $('#rm_overlay')
                        .addClass('transparent');

                } else {

                    $('#rm_livesearch_close').removeClass('visible');

                    $('#input_search, #rm_overlay')
                        .removeClass('active');

                }

            },

            error: function () {
                clearLiveSearch();
            }

        });

    }

};
