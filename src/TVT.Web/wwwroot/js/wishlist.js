const tvtWishlist = {

    storageKey: 'tvt_wishlist',


    /* =========================
       Get Wishlist
       ========================= */

    getItems: function () {

        try {

            return JSON.parse(
                localStorage.getItem(this.storageKey) || '[]'
            );

        } catch (error) {

            console.error(
                'Wishlist could not be loaded.',
                error
            );

            return [];
        }
    },


    /* =========================
       Save Wishlist
       ========================= */

    saveItems: function (items) {

        localStorage.setItem(
            this.storageKey,
            JSON.stringify(items)
        );

    },


    /* =========================
       Check Product
       ========================= */

    contains: function (productId) {

        productId = parseInt(productId, 10);

        if (!productId) {
            return false;
        }

        const items = this.getItems();

        return items.some(function (item) {
            return item.productId === productId;
        });

    },


    /* =========================
       Add Product
       ========================= */

    add: function (productId) {

        productId = parseInt(productId, 10);

        if (!productId) {
            return;
        }

        const items = this.getItems();

        const exists = items.some(function (item) {
            return item.productId === productId;
        });

        if (!exists) {

            items.push({
                productId: productId
            });

            this.saveItems(items);

        }

        this.updateHeader();

        this.updateButtons();

        this.showNotification(
            'Məhsul arzu siyahısına əlavə edildi'
        );

        console.log(
            'Product added to wishlist:',
            productId
        );

    },

    /* =========================
       Remove Product
       ========================= */

    remove: function (productId) {

        productId = parseInt(productId, 10);

        if (!productId) {
            return;
        }

        let items = this.getItems();

        items = items.filter(function (item) {
            return item.productId !== productId;
        });

        this.saveItems(items);

        this.updateHeader();

        this.updateButtons();

        this.showNotification(
            'Məhsul arzu siyahısından silindi'
        );

        console.log(
            'Product removed from wishlist:',
            productId
        );

    },

    /* =========================
       Toggle Product
       ========================= */

    toggle: function (productId) {

        if (this.contains(productId)) {

            this.remove(productId);

        } else {

            this.add(productId);

        }

    },


    /* =========================
       Header Quantity
       ========================= */

    updateHeader: function () {

        const count = this.getItems().length;


        const elements = document.querySelectorAll(
            '.oct-fixed-bar-wishlist-quantity, .oct-header-wishlist-quantity'
        );


        elements.forEach(function (element) {

            element.textContent = count;

        });

    },



    /* =========================
   Notification
   ========================= */

    showNotification: function (message) {

        let notification =
            document.getElementById('tvt-notification');

        if (!notification) {

            notification =
                document.createElement('div');

            notification.id =
                'tvt-notification';

            notification.className =
                'tvt-notification';

            document.body.appendChild(notification);
        }

        notification.textContent = message;

        notification.classList.add('show');

        clearTimeout(
            notification._timeout
        );

        notification._timeout =
            setTimeout(function () {

                notification.classList.remove('show');

            }, 2500);

    },

    /* =========================
       Button State
       ========================= */

    updateButtons: function () {

        const items = this.getItems();


        document
            .querySelectorAll('[data-wishlist-product-id]')
            .forEach(function (button) {

                const productId =
                    parseInt(
                        button.dataset.wishlistProductId,
                        10
                    );


                const exists = items.some(function (item) {
                    return item.productId === productId;
                });


                button.classList.toggle(
                    'active',
                    exists
                );

            });

    },


    /* =========================
       Initialize
       ========================= */

    init: function () {

        this.updateHeader();

        this.updateButtons();

    }

};


document.addEventListener(
    'DOMContentLoaded',
    function () {

        tvtWishlist.init();

    }
);
