// JavaScript source code
$(document).ready(function () {

    var direction = document.documentElement.getAttribute('dir');
    var isRtl = (direction === 'rtl');

    /***** statistics ******** */
    $(window).scroll(startCounter);

    function startCounter() {
        if ($(window).scrollTop() > 200) {
            $(window).off("scroll", startCounter);
            $(".counter-count").each(function () {
                var $this = $(this);
                jQuery({
                    Counter: 0
                }).animate({
                    Counter: $this.text()
                }, {
                    duration: 5000,
                    easing: "swing",
                    step: function () {
                        $this.text(Math.ceil(this.Counter));
                    },
                });
            });
        }
    }

    // -------------news-----------------//

    $('.owl-news').owlCarousel({
        rtl: isRtl,
        margin: 20,
        responsiveClass: true,
        loop: false,
        nav: true,
        slideBy: 1,
        dotsEach: 1,
        navText: [
            isRtl ? '<i class="bi bi-chevron-right"></i>' : '<i class="bi bi-chevron-left"></i>', // Prev
            isRtl ? '<i class="bi bi-chevron-left"></i>' : '<i class="bi bi-chevron-right"></i>' // Next
        ],
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 2,
            },
            1000: {
                items: 3,
            },
        },
        onInitialized: function (event) {
            var $carousel = $(event.target);
            var $dots = $carousel.find('.owl-dots');
            var $nav = $carousel.find('.owl-nav');

            // Avoid duplicate wrapper
            if ($carousel.find('.custom-controls').length === 0) {
                $('<div class="custom-controls d-flex justify-content-between align-items-center mt-3"></div>')
                    .append($nav)
                    .append($dots)
                    .appendTo($carousel);
            }
        }
    });


    // --------------- services --------------//

    $('.owl-services').owlCarousel({
        rtl: isRtl,
        margin: 20,
        responsiveClass: true,
        loop: false,
        nav: true,
        slideBy: 1,
        dotsEach: 1,
        navText: [
            isRtl ? '<i class="bi bi-chevron-right"></i>' : '<i class="bi bi-chevron-left"></i>', // Prev
            isRtl ? '<i class="bi bi-chevron-left"></i>' : '<i class="bi bi-chevron-right"></i>' // Next
        ],
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 2,
            },
            1000: {
                items: 5,
            },
        },
        onInitialized: function (event) {
            var $carousel = $(event.target);
            var $dots = $carousel.find('.owl-dots');
            var $nav = $carousel.find('.owl-nav');

            // Avoid duplicate wrapper
            if ($carousel.find('.custom-controls').length === 0) {
                $('<div class="custom-controls d-flex justify-content-between align-items-center mt-3"></div>')
                    .append($nav)
                    .append($dots)
                    .appendTo($carousel);
            }
        }
    });




    // --------------- importantLinks --------------//

    $('.owl-importantlinks').owlCarousel({
        rtl: isRtl,
        margin: 20,
        responsiveClass: true,
        loop: false,
        nav: true,
        slideBy: 1,
        dotsEach: 1,
        navText: [
            isRtl ? '<i class="bi bi-chevron-right"></i>' : '<i class="bi bi-chevron-left"></i>', // Prev
            isRtl ? '<i class="bi bi-chevron-left"></i>' : '<i class="bi bi-chevron-right"></i>' // Next
        ],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 4
            }
        },
        onInitialized: function (event) {
            var $carousel = $(event.target);
            var $dots = $carousel.find('.owl-dots');
            var $nav = $carousel.find('.owl-nav');

            // Avoid duplicate wrapper
            if ($carousel.find('.custom-controls').length === 0) {
                $('<div class="custom-controls d-flex justify-content-between align-items-center mt-3"></div>')
                    .append($nav)
                    .append($dots)
                    .appendTo($carousel);
            }
        }
    });


    // -------------events-----------------//

    $('.owl-events').owlCarousel({
        rtl: isRtl,
        margin: 20,
        responsiveClass: true,
        loop: false,
        nav: true,
        slideBy: 1,
        dotsEach: 1,
        navText: [
            isRtl ? '<i class="bi bi-chevron-right"></i>' : '<i class="bi bi-chevron-left"></i>', // Prev
            isRtl ? '<i class="bi bi-chevron-left"></i>' : '<i class="bi bi-chevron-right"></i>' // Next
        ],
        responsive: {
            0: {
                items: 1,
            },
            400: {
                items: 1,
            },
            600: {
                items: 3,
            },
            1000: {
                items: 5,
                singleItem: true,
            },
        },
        onInitialized: function (event) {
            var $carousel = $(event.target);
            var $dots = $carousel.find('.owl-dots');
            var $nav = $carousel.find('.owl-nav');

            // Avoid duplicate wrapper
            if ($carousel.find('.custom-controls').length === 0) {
                $('<div class="custom-controls d-flex justify-content-between align-items-center mt-3"></div>')
                    .append($nav)
                    .append($dots)
                    .appendTo($carousel);
            }
        }
    });



    // -------------publications-----------------//

    $('.owl-publications').owlCarousel({
        rtl: isRtl,
        margin: 20,
        responsiveClass: true,
        loop: false,
        nav: true,
        slideBy: 1,
        dotsEach: 1,
        navText: [
            isRtl ? '<i class="bi bi-chevron-right"></i>' : '<i class="bi bi-chevron-left"></i>', // Prev
            isRtl ? '<i class="bi bi-chevron-left"></i>' : '<i class="bi bi-chevron-right"></i>' // Next
        ],
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 2,
            },
            1000: {
                items: 4,
            },
        },
        onInitialized: function (event) {
            var $carousel = $(event.target);
            var $dots = $carousel.find('.owl-dots');
            var $nav = $carousel.find('.owl-nav');

            // Avoid duplicate wrapper
            if ($carousel.find('.custom-controls').length === 0) {
                $('<div class="custom-controls d-flex justify-content-between align-items-center mt-3"></div>')
                    .append($nav)
                    .append($dots)
                    .appendTo($carousel);
            }
        }
    });

});
