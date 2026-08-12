const tvtCart = {

    open: function () {
        window.location.href = '/Cart';
    },

    getItems: function () {

        try {
            return JSON.parse(
                localStorage.getItem('tvt_cart') || '[]'
            );
        }
        catch (error) {
            console.error('Cart could not be loaded.', error);
            return [];
        }
    },

    getTotalQuantity: function () {

        const cart = this.getItems();

        return cart.reduce(function (total, item) {
            return total + item.quantity;
        }, 0);
    },


    add: async function (productId, quantity = 1) {

        productId = parseInt(productId, 10);
        quantity = parseInt(quantity, 10);

        if (!productId || !quantity || quantity < 1) {
            return;
        }

        try {

            const response = await fetch(
                '/Cart/Products?ids=' + productId
            );

            if (!response.ok) {
                throw new Error('Product could not be loaded.');
            }

            const products = await response.json();

            const product = products.find(function (item) {
                return item.id === productId;
            });

            if (!product) {
                console.error('Product not found:', productId);
                return;
            }

            const stockQuantity =
                parseInt(product.stockQuantity, 10) || 0;

            if (stockQuantity <= 0) {
                alert('Bu məhsul stokda yoxdur.');
                return;
            }


            let cart = this.getItems();

            const existingItem = cart.find(function (item) {
                return item.productId === productId;
            });


            if (existingItem) {

                const newQuantity =
                    existingItem.quantity + quantity;

                existingItem.quantity =
                    Math.min(newQuantity, stockQuantity);

            }
            else {

                cart.push({
                    productId: productId,
                    quantity: Math.min(quantity, stockQuantity)
                });

            }


            localStorage.setItem(
                'tvt_cart',
                JSON.stringify(cart)
            );


            this.updateHeader();


            if (window.location.pathname.toLowerCase() === '/cart') {
                window.location.reload();
            }


            console.log('Product added to cart:', {
                productId: productId,
                quantity: quantity,
                stockQuantity: stockQuantity
            });

        }
        catch (error) {

            console.error(
                'Product could not be added to cart.',
                error
            );

        }

    },
    updateHeader: function () {

        const totalQuantity = this.getTotalQuantity();

        const quantityElement =
            document.getElementById('cart-quantity');

        if (quantityElement) {
            quantityElement.textContent = totalQuantity;
        }

        const fixedQuantityElement =
            document.getElementById('fixed-cart-quantity');

        if (fixedQuantityElement) {
            fixedQuantityElement.textContent = totalQuantity;
        }
    }
};


document.addEventListener('DOMContentLoaded', function () {

    tvtCart.updateHeader();

});
