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
                             alt="${product.nameAz || ''}"
                             style="width: 90px; height: 90px; object-fit: contain;">

                    </div>

                    <div>

                        <a href="/Product/Index?slug=${encodeURIComponent(product.slugAz || '')}"
                           class="text-decoration-none">

                            <h5 class="mb-2">
                                ${product.nameAz || ''}
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
                    Sil
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


    /* Initial load */

    await loadCart();

});
