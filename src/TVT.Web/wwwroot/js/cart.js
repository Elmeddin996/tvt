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

            console.error(
                'Cart could not be loaded.',
                error
            );

            return [];

        }

    },


    getTotalQuantity: function () {

        const cart = this.getItems();

        return cart.reduce(function (total, item) {

            return total + (
                parseInt(item.quantity, 10) || 0
            );

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
                throw new Error(
                    'Product could not be loaded.'
                );
            }

            const products = await response.json();

            const product = products.find(function (item) {

                return item.id === productId;

            });

            if (!product) {

                console.error(
                    'Product not found:',
                    productId
                );

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

                const currentQuantity =
                    parseInt(existingItem.quantity, 10) || 0;

                const newQuantity =
                    currentQuantity + quantity;

                existingItem.quantity =
                    Math.min(
                        newQuantity,
                        stockQuantity
                    );

            }
            else {

                cart.push({

                    productId: productId,

                    quantity: Math.min(
                        quantity,
                        stockQuantity
                    )

                });

            }


            localStorage.setItem(
                'tvt_cart',
                JSON.stringify(cart)
            );


            await this.updateHeader();


            if (
                window.location.pathname.toLowerCase() ===
                '/cart'
            ) {

                window.location.reload();

            }


            console.log(
                'Product added to cart:',
                {
                    productId: productId,
                    quantity: quantity,
                    stockQuantity: stockQuantity
                }
            );

        }
        catch (error) {

            console.error(
                'Product could not be added to cart.',
                error
            );

        }

    },


    updateHeader: async function () {

        const cart = this.getItems();


        /*
         * Quantity
         */

        const totalQuantity =
            cart.reduce(function (total, item) {

                return total + (
                    parseInt(item.quantity, 10) || 0
                );

            }, 0);


        const quantityElement =
            document.getElementById('cart-quantity');

        if (quantityElement) {

            quantityElement.textContent =
                totalQuantity;

        }


        const fixedQuantityElement =
            document.getElementById(
                'fixed-cart-quantity'
            );

        if (fixedQuantityElement) {

            fixedQuantityElement.textContent =
                totalQuantity;

        }


        /*
         * Amount elements
         */

        const amountElement =
            document.getElementById(
                'cart-total-amount'
            );

        const fixedAmountElement =
            document.getElementById(
                'fixed-cart-total-amount'
            );


        if (!amountElement && !fixedAmountElement) {
            return;
        }


        /*
         * Empty cart
         */

        if (cart.length === 0) {

            if (amountElement) {

                amountElement.textContent =
                    '0.00 ₼';

            }

            if (fixedAmountElement) {

                fixedAmountElement.textContent =
                    '0.00 ₼';

            }

            return;

        }


        try {

            /*
             * Build query correctly:
             *
             * /Cart/Products?ids=1&ids=2
             *
             * instead of:
             *
             * /Cart/Products?ids=1,2
             */

            const params =
                new URLSearchParams();


            cart.forEach(function (item) {

                const productId =
                    parseInt(item.productId, 10);

                if (productId) {

                    params.append(
                        'ids',
                        productId.toString()
                    );

                }

            });


            const response = await fetch(
                '/Cart/Products?' +
                params.toString()
            );


            if (!response.ok) {

                throw new Error(
                    'Cart products could not be loaded.'
                );

            }


            const products =
                await response.json();


            let totalAmount = 0;


            cart.forEach(function (cartItem) {

                const product =
                    products.find(function (item) {

                        return item.id ===
                            parseInt(
                                cartItem.productId,
                                10
                            );

                    });


                if (!product) {
                    return;
                }


                const price =
                    Number(product.price) || 0;


                const quantity =
                    parseInt(
                        cartItem.quantity,
                        10
                    ) || 0;


                totalAmount +=
                    price * quantity;

            });


            const formattedAmount =
                totalAmount.toFixed(2) + ' ₼';


            if (amountElement) {

                amountElement.textContent =
                    formattedAmount;

            }


            if (fixedAmountElement) {

                fixedAmountElement.textContent =
                    formattedAmount;

            }

        }
        catch (error) {

            console.error(
                'Cart total amount could not be calculated.',
                error
            );

        }

    }

};


document.addEventListener(
    'DOMContentLoaded',
    function () {

        tvtCart.updateHeader();

    }
);
