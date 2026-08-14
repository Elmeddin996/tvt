document.addEventListener('DOMContentLoaded', async function () {

    const loadingElement =
        document.getElementById('wishlist-loading');

    const emptyElement =
        document.getElementById('wishlist-empty');

    const contentElement =
        document.getElementById('wishlist-content');

    const itemsElement =
        document.getElementById('wishlist-items');


    /* =========================
       Wishlist
       ========================= */

    function getWishlist() {

        try {

            return JSON.parse(
                localStorage.getItem('tvt_wishlist') || '[]'
            );

        } catch (error) {

            console.error(
                'Wishlist could not be loaded.',
                error
            );

            return [];
        }
    }


    function saveWishlist(wishlist) {

        localStorage.setItem(
            'tvt_wishlist',
            JSON.stringify(wishlist)
        );

        if (typeof tvtWishlist !== 'undefined') {

            tvtWishlist.updateHeader();

            tvtWishlist.updateButtons();

        }
    }


    /* =========================
       Empty
       ========================= */

    function showEmptyWishlist() {

        loadingElement.style.display = 'none';

        contentElement.style.display = 'none';

        emptyElement.style.display = 'block';

    }


    /* =========================
       Image
       ========================= */

    function getProductImage(product) {

        const image =
            product.images && product.images.length > 0
                ? product.images[0].image
                : '/images/no-image.png';

        return image;
    }


    /* =========================
       Render
       ========================= */

    function renderWishlist(products, wishlist) {

        if (!products || products.length === 0) {

            showEmptyWishlist();

            return;
        }


        itemsElement.innerHTML = '';


        products.forEach(function (product) {

            const wishlistItem =
                wishlist.find(function (item) {

                    return item.productId === product.id;

                });


            if (!wishlistItem) {
                return;
            }


            const image =
                getProductImage(product);


            const price =
                Number(product.price) || 0;


            const oldPrice =
                product.oldPrice !== null &&
                    product.oldPrice !== undefined
                    ? Number(product.oldPrice)
                    : null;


            const stockQuantity =
                Number(product.stockQuantity) || 0;


            const hasOldPrice =
                oldPrice !== null &&
                oldPrice > price;


            const itemElement =
                document.createElement('div');


            itemElement.className =
                'wishlist-item';


            itemElement.innerHTML = `

                <div class="wishlist-item-info">

                    <a href="/Product/Index?slug=${encodeURIComponent(product.slugAz || '')}"
                       class="wishlist-item-image-link">

                        <img src="${image}"
                             class="wishlist-item-image"
                             alt="${product.nameAz || ''}">

                    </a>


                    <div class="wishlist-item-details">

                        <a href="/Product/Index?slug=${encodeURIComponent(product.slugAz || '')}"
                           class="wishlist-item-title">

                            ${product.nameAz || ''}

                        </a>


                        <div class="wishlist-item-code">

                            Məhsulun kodu:
                            ${product.code || ''}

                        </div>


                        ${stockQuantity > 0

                    ? `
                                    <div class="wishlist-item-stock">
                                        Stokda var
                                    </div>
                                  `

                    : `
                                    <div class="wishlist-item-stock out-of-stock">
                                        Stokda yoxdur
                                    </div>
                                  `
                }

                    </div>

                </div>


                <div class="wishlist-item-price">

                    ${hasOldPrice

                    ? `
                                <span class="wishlist-item-price-old">
                                    ${oldPrice.toFixed(2)} ₼
                                </span>
                              `

                    : ''
                }

                    <span class="wishlist-item-price-current">
                        ${price.toFixed(2)} ₼
                    </span>

                </div>


                <div class="wishlist-item-actions">


                    <!-- Quantity -->

                    <div class="wishlist-item-quantity">

                        <button type="button"
                                class="wishlist-quantity-minus"
                                data-product-id="${product.id}"
                                ${stockQuantity <= 0 ? 'disabled' : ''}>
                            −
                        </button>


                        <span class="wishlist-quantity"
                              data-product-id="${product.id}">
                            1
                        </span>


                        <button type="button"
                                class="wishlist-quantity-plus"
                                data-product-id="${product.id}"
                                ${stockQuantity <= 1 ? 'disabled' : ''}>
                            +
                        </button>

                    </div>


                    <!-- Cart -->

                    <button type="button"
                            class="rm-btn secondary wishlist-item-cart d-flex align-items-center justify-content-center"
                            data-product-id="${product.id}"
                            ${stockQuantity <= 0 ? 'disabled' : ''}>

                        <span class="rm-cart-btn-icon"></span>

                        <span class="rm-btn-text">
                            Səbətə at
                        </span>

                    </button>


                    <!-- Remove -->

                    <button type="button"
                            class="wishlist-item-remove"
                            data-product-id="${product.id}"
                            aria-label="Arzu siyahısından sil">

                        <i class="fas fa-times"></i>

                    </button>

                </div>

            `;


            itemsElement.appendChild(itemElement);

        });


        loadingElement.style.display = 'none';

        emptyElement.style.display = 'none';

        contentElement.style.display = 'block';

    }


    /* =========================
       Load Products
       ========================= */

    let loadedProducts = [];


    async function loadWishlist() {

        const wishlist =
            getWishlist();


        if (!wishlist.length) {

            showEmptyWishlist();

            return;
        }


        const productIds =
            wishlist.map(function (item) {

                return item.productId;

            });


        try {

            const response =
                await fetch('/Wishlist/GetItems', {

                    method: 'POST',

                    headers: {
                        'Content-Type': 'application/json'
                    },

                    body: JSON.stringify(productIds)

                });


            if (!response.ok) {

                throw new Error(
                    'Wishlist products could not be loaded.'
                );

            }


            loadedProducts =
                await response.json();


            renderWishlist(
                loadedProducts,
                wishlist
            );

        }
        catch (error) {

            console.error(error);

            showEmptyWishlist();

        }

    }


    /* =========================
       Actions
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


            const wishlist =
                getWishlist();


            const product =
                loadedProducts.find(function (product) {

                    return product.id === productId;

                });


            if (!product) {
                return;
            }


            const stockQuantity =
                Number(product.stockQuantity) || 0;


            /* =========================
               Quantity Minus
               ========================= */

            if (
                button.classList.contains(
                    'wishlist-quantity-minus'
                )
            ) {

                const quantityElement =
                    itemsElement.querySelector(
                        `.wishlist-quantity[data-product-id="${productId}"]`
                    );


                if (!quantityElement) {
                    return;
                }


                let quantity =
                    parseInt(
                        quantityElement.textContent,
                        10
                    ) || 1;


                if (quantity > 1) {

                    quantity--;

                    quantityElement.textContent =
                        quantity;

                }


                return;
            }


            /* =========================
               Quantity Plus
               ========================= */

            if (
                button.classList.contains(
                    'wishlist-quantity-plus'
                )
            ) {

                const quantityElement =
                    itemsElement.querySelector(
                        `.wishlist-quantity[data-product-id="${productId}"]`
                    );


                if (!quantityElement) {
                    return;
                }


                let quantity =
                    parseInt(
                        quantityElement.textContent,
                        10
                    ) || 1;


                if (quantity < stockQuantity) {

                    quantity++;

                    quantityElement.textContent =
                        quantity;

                }


                return;
            }


            /* =========================
               Add To Cart
               ========================= */

            if (
                button.classList.contains(
                    'wishlist-item-cart'
                )
            ) {

                const quantityElement =
                    itemsElement.querySelector(
                        `.wishlist-quantity[data-product-id="${productId}"]`
                    );


                let quantity =
                    parseInt(
                        quantityElement?.textContent,
                        10
                    ) || 1;


                if (quantity > stockQuantity) {

                    quantity =
                        stockQuantity;

                }


                if (quantity <= 0) {
                    return;
                }


                if (
                    typeof tvtCart !== 'undefined' &&
                    typeof tvtCart.add === 'function'
                ) {

                    tvtCart.add(
                        productId,
                        quantity
                    );

                }


                return;
            }


            /* =========================
               Remove Wishlist
               ========================= */

            if (
                button.classList.contains(
                    'wishlist-item-remove'
                )
            ) {

                if (
                    typeof tvtWishlist !== 'undefined' &&
                    typeof tvtWishlist.remove === 'function'
                ) {

                    tvtWishlist.remove(productId);

                }

                loadWishlist();

                return;
            }

        }
    );


    /* =========================
       Initial Load
       ========================= */

    await loadWishlist();

});
