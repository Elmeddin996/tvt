document.addEventListener('DOMContentLoaded', async function () {

    const loadingElement = document.getElementById('cart-loading');
    const emptyElement = document.getElementById('cart-empty');
    const contentElement = document.getElementById('cart-content');
    const itemsElement = document.getElementById('cart-items');

    const totalQuantityElement =
        document.getElementById('cart-total-quantity');

    const totalPriceElement =
        document.getElementById('cart-total-price');


    /* =========================
       Load Cart
       ========================= */

    function getCart() {

        try {

            return JSON.parse(
                localStorage.getItem('tvt_cart') || '[]'
            );

        } catch (error) {

            console.error('Cart could not be loaded.', error);

            return [];
        }
    }


    function saveCart(cart) {

        localStorage.setItem(
            'tvt_cart',
            JSON.stringify(cart)
        );

        if (typeof tvtCart !== 'undefined') {
            tvtCart.updateHeader();
        }
    }


    /* =========================
       Render Empty Cart
       ========================= */

    function showEmptyCart() {

        loadingElement.style.display = 'none';
        contentElement.style.display = 'none';
        emptyElement.style.display = 'block';

    }


    /* =========================
       Render Cart
       ========================= */


    function getCurrentCulture() {

        const lang =
            document.documentElement.lang || 'az';

        return lang.toLowerCase().split('-')[0];
    }


    function getLocalizedProductField(product, field) {

        const culture = getCurrentCulture();

        const suffixMap = {
            az: 'Az',
            en: 'En',
            ru: 'Ru'
        };

        const suffix =
            suffixMap[culture] || 'Az';

        return product[field + suffix]
            || product[field + 'Az']
            || '';
    }


    function renderCart(products, cart) {

        if (!products || products.length === 0) {

            showEmptyCart();

            return;
        }


        itemsElement.innerHTML = '';

        let totalQuantity = 0;
        let totalPrice = 0;


        products.forEach(function (product) {

            const cartItem = cart.find(function (item) {
                return item.productId === product.id;
            });

            if (!cartItem) {
                return;
            }


            const stockQuantity = product.stockQuantity || 0;

            if (stockQuantity <= 0) {

                const index = cart.indexOf(cartItem);

                cart.splice(index, 1);

                return;
            }

            let quantity = cartItem.quantity;

            if (quantity > stockQuantity) {
                quantity = stockQuantity;
                cartItem.quantity = stockQuantity;
            }

            const price = product.price || 0;

            const subtotal = price * quantity;


            totalQuantity += quantity;
            totalPrice += subtotal;


            const image =
                product.images && product.images.length > 0
                    ? product.images[0].image
                    : '/images/no-image.png';


            const itemElement =
                document.createElement('div');

            itemElement.className =
                'cart-item d-flex align-items-center justify-content-between mb-3 p-3 border';


            itemElement.innerHTML = `

                <div class="d-flex align-items-center">

                    <div class="me-3">

                        <img src="${image}"
                             alt="${getLocalizedProductField(product, 'name')}"
                             style="width: 90px; height: 90px; object-fit: contain;">

                    </div>

                    <div>

                        <a href="/Product/Index?slug=${encodeURIComponent(
                            getLocalizedProductField(product, 'slug')
                        )}"
                           class="text-decoration-none">

                            <h5 class="mb-2">
                                ${getLocalizedProductField(product, 'name')}
                            </h5>

                        </a>

                        <div class="text-muted">
                            ${price.toFixed(2)} ₼
                        </div>

                    </div>

                </div>


                <div class="d-flex align-items-center">

                    <button type="button"
                            class="btn btn-sm btn-outline-secondary cart-quantity-minus"
                            data-product-id="${product.id}">
                        −
                    </button>

                    <span class="mx-3 cart-item-quantity">
                        ${quantity}
                    </span>

                    <button type="button"
        class="btn btn-sm btn-outline-secondary cart-quantity-plus"
        data-product-id="${product.id}"
        ${quantity >= stockQuantity ? 'disabled' : ''}>
    +
</button>

                </div>


                <div class="fw-bold ms-4">
                    ${subtotal.toFixed(2)} ₼
                </div>


                <button type="button"
        class="btn btn-sm btn-outline-danger ms-4 cart-remove"
        data-product-id="${product.id}">
    ${window.tvtCartLocalization.remove}
</button>

            `;


            itemsElement.appendChild(itemElement);

        });


        totalQuantityElement.textContent =
            totalQuantity;


        totalPriceElement.textContent =
            totalPrice.toFixed(2) + ' ₼';


        loadingElement.style.display = 'none';
        emptyElement.style.display = 'none';
        contentElement.style.display = 'block';

    }


    /* =========================
       Load Products
       ========================= */

    let loadedProducts = [];
    async function loadCart() {

        const cart = getCart();


        if (!cart.length) {

            showEmptyCart();

            return;
        }


        const ids = cart
            .map(function (item) {
                return item.productId;
            })
            .join('&ids=');


        try {

            const response = await fetch(
                '/Cart/Products?ids=' + ids
            );


            if (!response.ok) {

                throw new Error(
                    'Cart products could not be loaded.'
                );

            }


            loadedProducts = await response.json();

            renderCart(loadedProducts, cart);

        }
        catch (error) {

            console.error(error);

            showEmptyCart();

        }

    }


    /* =========================
       Quantity + / -
       ========================= */

    itemsElement.addEventListener(
        'click',
        function (event) {

            const button =
                event.target.closest('button');

            if (!button) {
                return;
            }


            const productId =
                parseInt(
                    button.dataset.productId,
                    10
                );


            if (!productId) {
                return;
            }


            const cart = getCart();


            const item =
                cart.find(function (cartItem) {
                    return cartItem.productId === productId;
                });


            if (!item) {
                return;
            }


            /* Minus */

            if (
                button.classList.contains(
                    'cart-quantity-minus'
                )
            ) {

                item.quantity--;

                if (item.quantity <= 0) {

                    const index =
                        cart.indexOf(item);

                    cart.splice(index, 1);

                }

            }


            /* Plus */

            if (
                button.classList.contains(
                    'cart-quantity-plus'
                )
            ) {

                const product = loadedProducts.find(function (product) {
                    return product.id === productId;
                });

                if (product) {

                    const stockQuantity =
                        product.stockQuantity || 0;

                    if (item.quantity < stockQuantity) {
                        item.quantity++;
                    }

                }

            }


            /* Remove */

            if (
                button.classList.contains(
                    'cart-remove'
                )
            ) {

                const index =
                    cart.indexOf(item);

                cart.splice(index, 1);

            }


            saveCart(cart);

            loadCart();

        }
    );


    /* =========================
   Checkout
   ========================= */

    const checkoutButton =
        document.getElementById('checkout-button');

    const checkoutFormContainer =
        document.getElementById('checkout-form-container');

    const submitOrderButton =
        document.getElementById('submit-order-button');


    if (submitOrderButton) {

        submitOrderButton.addEventListener('click', async function () {

            const customerName =
                document.getElementById('checkout-customer-name').value.trim();

            const phone =
                document.getElementById('checkout-phone').value.trim();

            const email =
                document.getElementById('checkout-email').value.trim();

            const address =
                document.getElementById('checkout-address').value.trim();

            const errorElement =
                document.getElementById('checkout-error');


            errorElement.style.display = 'none';
            errorElement.textContent = '';


            if (!customerName || !phone || !address) {

                errorElement.textContent =
                    'Please fill in all required fields.';

                errorElement.style.display = 'block';

                return;
            }


            const phoneDigits =
                phone.replace(/\D/g, '');

            if (phoneDigits.length < 10) {

                errorElement.textContent =
                    'Please enter a valid phone number. Minimum 10 digits required.';

                errorElement.style.display = 'block';

                return;
            }


            if (email) {

                const emailRegex =
                    /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

                if (!emailRegex.test(email)) {

                    errorElement.textContent =
                        'Please enter a valid email address.';

                    errorElement.style.display = 'block';

                    return;
                }

            }


            const cart = getCart();


            if (!cart.length) {

                errorElement.textContent =
                    'Your cart is empty.';

                errorElement.style.display = 'block';

                return;
            }


            const orderData = {

                customerName: customerName,

                phone: phone,

                email: email || null,

                address: address,

                items: cart.map(function (item) {

                    return {
                        productId: item.productId,
                        quantity: item.quantity
                    };

                })

            };


            submitOrderButton.disabled = true;

            submitOrderButton.textContent = 'Processing...';


            try {

                const response = await fetch('/Order/Create', {

                    method: 'POST',

                    headers: {
                        'Content-Type': 'application/json'
                    },

                    body: JSON.stringify(orderData)

                });


                const result = await response.json();


                if (!response.ok || !result.success) {

                    throw new Error(
                        result.message || 'Order could not be created.'
                    );

                }


                localStorage.removeItem('tvt_cart');


                if (typeof tvtCart !== 'undefined') {
                    tvtCart.updateHeader();
                }


                window.location.href =
                    '/Order/Success?id=' + result.orderId;

            }
            catch (error) {

                console.error(error);

                errorElement.textContent =
                    error.message || 'Order could not be created.';

                errorElement.style.display = 'block';

                submitOrderButton.disabled = false;

                submitOrderButton.textContent =
                    'Checkout';

            }

        });

    }


    if (checkoutButton) {

        checkoutButton.addEventListener('click', function () {

            checkoutFormContainer.style.display = 'block';

            checkoutFormContainer.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });

        });

    }



    /* Initial load */

    await loadCart();

});
