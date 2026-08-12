document.addEventListener('DOMContentLoaded', function () {

    const gallery = document.querySelector('.rm-product-gallery');

    if (!gallery) {
        return;
    }

    const imagesJson = gallery.dataset.images;

    if (!imagesJson) {
        return;
    }

    let images;

    try {
        images = JSON.parse(imagesJson);
    } catch (error) {
        console.error('Product gallery images could not be parsed.', error);
        return;
    }

    const mainImage = document.getElementById('productMainImage');
    const thumbnails = document.querySelectorAll(
        '.rm-product-gallery-thumbnail'
    );

    const prevButton = document.querySelector(
        '.rm-product-gallery-prev'
    );

    const nextButton = document.querySelector(
        '.rm-product-gallery-next'
    );

    if (!mainImage || images.length === 0) {
        return;
    }

    let currentIndex = images.findIndex(function (image) {
        return image === mainImage.src;
    });

    if (currentIndex < 0) {
        currentIndex = 0;
    }


    /* =========================
       Main Gallery
       ========================= */

    function showImage(index) {

        if (index < 0) {
            index = images.length - 1;
        }

        if (index >= images.length) {
            index = 0;
        }

        currentIndex = index;

        mainImage.src = images[currentIndex];

        thumbnails.forEach(function (thumbnail, thumbnailIndex) {

            thumbnail.classList.toggle(
                'active',
                thumbnailIndex === currentIndex
            );

        });
    }


    /* =========================
       Lightbox
       ========================= */

    const lightbox = document.getElementById(
        'productGalleryLightbox'
    );

    const lightboxImage = document.getElementById(
        'productGalleryLightboxImage'
    );

    const lightboxClose = document.getElementById(
        'productGalleryLightboxClose'
    );

    const lightboxPrev = document.getElementById(
        'productGalleryLightboxPrev'
    );

    const lightboxNext = document.getElementById(
        'productGalleryLightboxNext'
    );

    if (!lightbox ||
        !lightboxImage ||
        !lightboxClose ||
        !lightboxPrev ||
        !lightboxNext) {

        return;
    }


    function openLightbox() {

        lightboxImage.src = images[currentIndex];

        lightbox.classList.add('active');

        document.body.style.overflow = 'hidden';
    }


    function closeLightbox() {

        lightbox.classList.remove('active');

        document.body.style.overflow = '';
    }


    /* Main image click */

    mainImage.addEventListener('click', function () {
        openLightbox();
    });


    /* Close */

    lightboxClose.addEventListener('click', function () {
        closeLightbox();
    });


    /* Lightbox previous */

    lightboxPrev.addEventListener('click', function () {

        showImage(currentIndex - 1);

        lightboxImage.src = images[currentIndex];
    });


    /* Lightbox next */

    lightboxNext.addEventListener('click', function () {

        showImage(currentIndex + 1);

        lightboxImage.src = images[currentIndex];
    });


    /* Close by clicking overlay */

    lightbox.addEventListener('click', function (event) {

        if (event.target === lightbox) {
            closeLightbox();
        }

    });


    /* Keyboard */

    document.addEventListener('keydown', function (event) {

        if (!lightbox.classList.contains('active')) {
            return;
        }

        if (event.key === 'Escape') {
            closeLightbox();
        }

        if (event.key === 'ArrowLeft') {

            showImage(currentIndex - 1);

            lightboxImage.src = images[currentIndex];
        }

        if (event.key === 'ArrowRight') {

            showImage(currentIndex + 1);

            lightboxImage.src = images[currentIndex];
        }

    });


    /* Thumbnails */

    thumbnails.forEach(function (thumbnail, index) {

        thumbnail.addEventListener('click', function () {
            showImage(index);
        });

    });


    /* Previous */

    if (prevButton) {

        prevButton.addEventListener('click', function () {
            showImage(currentIndex - 1);
        });

    }


    /* Next */

    if (nextButton) {

        nextButton.addEventListener('click', function () {
            showImage(currentIndex + 1);
        });

    }


    /* =========================
   Product Quantity
   ========================= */

    const quantityInput = document.getElementById('input-quantity');
    const plusButton = document.querySelector(
        '.rm-product-quantity-btn-plus'
    );
    const minusButton = document.querySelector(
        '.rm-product-quantity-btn-minus'
    );

    const minQuantityInput = document.getElementById(
        'min-product-quantity'
    );

    const maxQuantityInput = document.getElementById(
        'max-product-quantity'
    );

    if (quantityInput) {

        const minQuantity = parseInt(
            minQuantityInput?.value || '1',
            10
        );

        const maxQuantity = parseInt(
            maxQuantityInput?.value || '999999',
            10
        );

        function getQuantity() {

            let quantity = parseInt(
                quantityInput.value,
                10
            );

            if (isNaN(quantity)) {
                quantity = minQuantity;
            }

            return quantity;
        }

        function setQuantity(quantity) {

            quantity = Math.max(
                minQuantity,
                Math.min(quantity, maxQuantity)
            );

            quantityInput.value = quantity;
        }

        if (plusButton) {

            plusButton.addEventListener('click', function () {

                setQuantity(getQuantity() + 1);

            });

        }

        if (minusButton) {

            minusButton.addEventListener('click', function () {

                setQuantity(getQuantity() - 1);

            });

        }

        quantityInput.addEventListener('change', function () {

            setQuantity(getQuantity());

        });

    }


    /* =========================
       Add To Cart
       ========================= */

    const addToCartButton = document.getElementById('button-cart');

    if (addToCartButton) {

        addToCartButton.addEventListener('click', function () {

            const productIdInput = document.querySelector(
                'input[name="product_id"]'
            );

            const quantityInput = document.getElementById(
                'input-quantity'
            );

            if (!productIdInput || !quantityInput) {
                return;
            }

            const productId = parseInt(
                productIdInput.value,
                10
            );

            const quantity = parseInt(
                quantityInput.value,
                10
            );

            if (!productId || !quantity || quantity < 1) {
                return;
            }

            if (typeof tvtCart !== 'undefined') {

                tvtCart.add(productId, quantity);

            }
        });

    }

});
