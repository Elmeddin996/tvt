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


document.addEventListener("DOMContentLoaded", function () {
    const minRange = document.getElementById("tvt-min-range");
    const maxRange = document.getElementById("tvt-max-range");

    const minInput = document.getElementById("tvt-min-price");
    const maxInput = document.getElementById("tvt-max-price");

    if (!minRange || !maxRange || !minInput || !maxInput) {
        return;
    }

    function updateRanges() {
        let minValue = parseFloat(minRange.value);
        let maxValue = parseFloat(maxRange.value);

        const minLimit = parseFloat(minRange.min);
        const maxLimit = parseFloat(minRange.max);

        if (minValue > maxValue) {
            if (document.activeElement === minRange) {
                minValue = maxValue;
                minRange.value = minValue;
            } else {
                maxValue = minValue;
                maxRange.value = maxValue;
            }
        }

        minInput.value = minValue;
        maxInput.value = maxValue;

        updateRangeProgress(
            minValue,
            maxValue,
            minLimit,
            maxLimit
        );
    }


    function updateRangeProgress(minValue, maxValue, minLimit, maxLimit) {
        const progress = document.getElementById("tvt-range-progress");

        if (!progress) {
            return;
        }

        const range = maxLimit - minLimit;

        if (range <= 0) {
            progress.style.left = "0%";
            progress.style.width = "100%";
            return;
        }

        const leftPercent =
            ((minValue - minLimit) / range) * 100;

        const rightPercent =
            ((maxValue - minLimit) / range) * 100;

        progress.style.left = `${leftPercent}%`;
        progress.style.width =
            `${rightPercent - leftPercent}%`;
    }

    minRange.addEventListener("input", updateRanges);
    maxRange.addEventListener("input", updateRanges);

    minInput.addEventListener("change", function () {
        let value = parseFloat(minInput.value);

        if (isNaN(value)) {
            return;
        }

        const maxValue = parseFloat(maxRange.value);

        if (value > maxValue) {
            value = maxValue;
        }

        minRange.value = value;
        minInput.value = value;

        updateRanges();
    });

    maxInput.addEventListener("change", function () {
        let value = parseFloat(maxInput.value);

        if (isNaN(value)) {
            return;
        }

        const minValue = parseFloat(minRange.value);

        if (value < minValue) {
            value = minValue;
        }

        maxRange.value = value;
        maxInput.value = value;
        updateRanges();
    });

    updateRanges();
});

document.addEventListener("DOMContentLoaded", function () {
    const resetButton = document.getElementById("tvt-reset-filters");

    if (!resetButton) {
        return;
    }

    resetButton.addEventListener("click", function () {
        const url = new URL(window.location.href);

        // Filterləri təmizlə
        url.searchParams.delete("brandIds");
        url.searchParams.delete("minPrice");
        url.searchParams.delete("maxPrice");
        url.searchParams.delete("inStock");
        url.searchParams.delete("page");
        url.searchParams.delete("pageSize");

        window.location.href = url.toString();
    });
});


document.addEventListener("DOMContentLoaded", function () {

    const showBrandsButton = document.getElementById("tvt-show-brands");

    if (showBrandsButton) {

        showBrandsButton.addEventListener("click", function () {

            const hiddenBrands =
                document.querySelectorAll(".tvt-brand-hidden");

            const isOpen =
                showBrandsButton.classList.contains("is-open");

            hiddenBrands.forEach(function (brand) {
                brand.style.display = isOpen ? "none" : "flex";
            });

            showBrandsButton.classList.toggle("is-open");

            const text =
                showBrandsButton.querySelector(".tvt-show-more-text");

            if (text) {
                text.textContent = isOpen
                    ? `Daha ${hiddenBrands.length} göstər`
                    : "Daha az göstər";
            }
        });
    }

});


document.addEventListener("DOMContentLoaded", function () {

    const filterOpen =
        document.getElementById("tvt-filter-open");

    const filterClose =
        document.getElementById("tvt-filter-close");

    const filterColumn =
        document.getElementById("column-left");

    if (!filterOpen || !filterClose || !filterColumn) {
        return;
    }

    function openFilter() {
        filterColumn.classList.add("tvt-filter-mobile-open");
        document.body.classList.add("tvt-filter-lock");
    }

    function closeFilter() {
        filterColumn.classList.remove("tvt-filter-mobile-open");
        document.body.classList.remove("tvt-filter-lock");
    }

    filterOpen.addEventListener("click", openFilter);

    filterClose.addEventListener("click", closeFilter);

});



/* =========================================
TVT MOBILE MENU / CATALOG
========================================= */

document.addEventListener("DOMContentLoaded", function () {

    const mobileMenu =
        document.getElementById("tvt-mobile-menu");

    const hamburgerButton =
        document.getElementById("rm_mobile_menu_button");

    const categoryCatalogButton =
        document.getElementById("tvt-category-catalog-button");

    const closeButton =
        document.getElementById("tvt-mobile-menu-close");

    const backButton =
        document.getElementById("tvt-mobile-menu-back");

    const backText =
        document.getElementById("tvt-mobile-menu-back-text");

    if (!mobileMenu) {
        return;
    }

    const levels =
        mobileMenu.querySelectorAll(".tvt-mobile-menu-level");

    let currentLevel = 0;

    /*
     * Show selected menu level
     */
    function showLevel(level) {

        if (!level) {
            return;
        }

        levels.forEach(function (item) {
            item.classList.remove("active");
        });

        level.classList.add("active");

        currentLevel =
            parseInt(level.dataset.level || "0");

        const title =
            level.dataset.title || "";

        if (currentLevel === 0) {

            backButton.style.visibility = "hidden";
            backText.textContent = "";

        } else {

            backButton.style.visibility = "visible";
            backText.textContent = title;
        }

        const content =
            mobileMenu.querySelector(
                ".tvt-mobile-menu-content"
            );

        if (content) {
            content.scrollTop = 0;
        }
    }


    /*
     * Open mobile menu
     *
     * Hamburger:
     * Menu -> Level 0
     *
     * Category page catalog:
     * Product catalog -> Level 1
     */
    function openMobileMenu(startLevel) {

        mobileMenu.classList.add("active");

        document.body.classList.add(
            "tvt-mobile-menu-lock"
        );

        const level =
            mobileMenu.querySelector(
                '.tvt-mobile-menu-level[data-level="' +
                startLevel +
                '"]'
            );

        if (level) {
            showLevel(level);
        }
    }


    /*
     * Close mobile menu
     */
    function closeMobileMenu() {

        mobileMenu.classList.remove("active");

        document.body.classList.remove(
            "tvt-mobile-menu-lock"
        );

        showLevel(
            mobileMenu.querySelector(
                '.tvt-mobile-menu-level[data-level="0"]'
            )
        );
    }


    /*
     * Main mobile hamburger
     *
     
     *
     * Hamburger
     *      ↓
     * Menyu
     */
    if (hamburgerButton) {

        hamburgerButton.addEventListener(
            "click",
            function (event) {

                if (window.innerWidth > 991.98) {
                    return;
                }

                event.preventDefault();
                event.stopPropagation();

                openMobileMenu(0);
            }
        );
    }


    /*
     * Category page "Kataloq" button
     *
     * Category page:
     *
     * Kataloq
     *      ↓
     * Məhsul kataloqu
     *
     * So we start from level 1.
     */
    if (categoryCatalogButton) {

        categoryCatalogButton.classList.remove(
            "rm-category-buttons-catalog"
        );

        categoryCatalogButton.addEventListener("click", function (event) {

            event.preventDefault();
            event.stopPropagation();

            openMobileMenu(1);
        });
    }


    /*
     * Close button
     */
    if (closeButton) {

        closeButton.addEventListener(
            "click",
            function (event) {

                event.preventDefault();

                closeMobileMenu();
            }
        );
    }


    /*
     * Open next category level
     */
    const nextButtons =
        mobileMenu.querySelectorAll(
            ".tvt-mobile-menu-next"
        );

    nextButtons.forEach(function (button) {

        button.addEventListener(
            "click",
            function (event) {

                event.preventDefault();
                event.stopPropagation();

                const targetId =
                    button.dataset.target;

                const target =
                    document.getElementById(targetId);

                if (!target) {
                    return;
                }

                showLevel(target);
            }
        );
    });


    const catalogLink =
        mobileMenu.querySelector(
            ".tvt-mobile-menu-catalog-link"
        );

    if (catalogLink) {

        catalogLink.addEventListener(
            "click",
            function (event) {

                event.preventDefault();
                event.stopPropagation();

                const target =
                    document.getElementById(
                        "tvt-mobile-menu-catalog"
                    );

                if (target) {
                    showLevel(target);
                }
            }
        );
    }


    /*
     * Back button
     *
     * Every mobile level will contain:
     *
     * data-parent="previous-level-id"
     *
     * Therefore we don't need findParentId().
     */
    if (backButton) {

        backButton.addEventListener(
            "click",
            function (event) {

                event.preventDefault();

                if (currentLevel <= 0) {
                    return;
                }

                const current =
                    mobileMenu.querySelector(
                        ".tvt-mobile-menu-level.active"
                    );

                if (!current) {
                    return;
                }

                const parentId =
                    current.dataset.parent;

                if (!parentId) {
                    return;
                }

                const parent =
                    document.getElementById(parentId);

                if (parent) {
                    showLevel(parent);
                }
            }
        );
    }


    /*
     * ESC closes mobile menu
     */
    document.addEventListener(
        "keydown",
        function (event) {

            if (
                event.key === "Escape" &&
                mobileMenu.classList.contains("active")
            ) {
                closeMobileMenu();
            }
        }
    );

});
