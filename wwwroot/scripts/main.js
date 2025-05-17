$(document).ready(function () {
    /***** statistics ******** */
    $(window).scroll(startCounter);
    function startCounter() {
        if ($(window).scrollTop() > 200) {
            $(window).off("scroll", startCounter);
            $('.counter-count').each(function () {
                var $this = $(this);
                jQuery({ Counter: 0 }).animate({ Counter: $this.text() }, {
                    duration: 5000,
                    easing: 'swing',
                    step: function () {
                        $this.text(Math.ceil(this.Counter));
                    }
                });
            });
        }
    }
    // services
    var owlServices = $('.services-carousel');
    owlServices.owlCarousel({
        stagePadding: 100,
        margin: 20,
        loop: true,
        rtl: true,
        dotsContainer: '.services .owlcarousel-control .owl-dots',
        responsive: {
            0: {
                items: 1,
                stagePadding: 30,
            },
            600: {
                items: 2,
                stagePadding: 50,
            },
            1000: {
                items: 4
            }
        }
    });
    // Go to the next item
    $('.services .owlcarousel-control .side-nav .next').click(function () {
        owlServices.trigger('next.owl.carousel', [300]);
    })
    // Go to the previous item
    $('.services .owlcarousel-control .side-nav .prev').click(function () {
        owlServices.trigger('prev.owl.carousel', [300]);
    })
    $('.services .owlcarousel-control .owl-dots .owl-dot').click(function () {
        owlServices.trigger('to.owl.carousel', [$(this).index(), 300]);
    });
    // importantLinks
    var owlImportantLinks = $('.importantLinks-carousel');
    owlImportantLinks.owlCarousel({
        stagePadding: 100,
        margin: 20,
        loop: true,
        rtl: true,
        dotsContainer: '.importantLinks .owlcarousel-control .owl-dots',
        responsive: {
            0: {
                items: 1,
                stagePadding: 30,
            },
            600: {
                items: 2,
                stagePadding: 50,
            },
            1000: {
                items: 4
            }
        }
    });
    // Go to the next item
    $('.importantLinks .owlcarousel-control .side-nav .next').click(function () {
        owlImportantLinks.trigger('next.owl.carousel', [300]);
    })
    // Go to the previous item
    $('.importantLinks .owlcarousel-control .side-nav .prev').click(function () {
        owlImportantLinks.trigger('prev.owl.carousel', [300]);
    })
    $('.importantLinks .owlcarousel-control .owl-dots .owl-dot').click(function () {
        owlImportantLinks.trigger('to.owl.carousel', [$(this).index(), 300]);
    });
    // events
    var owlEvents = $('.events-carousel');
    owlEvents.owlCarousel({
        stagePadding: 100,
        margin: 20,
        loop: true,
        rtl: true,
        dotsContainer: '.events .owlcarousel-control .owl-dots',
        responsive: {
            0: {
                items: 1,
                stagePadding: 30,
            },
            600: {
                items: 2,
                stagePadding: 50,
            },
            1000: {
                items: 4
            }
        }
    });
    // Go to the next item
    $('.events .owlcarousel-control .side-nav .next').click(function () {
        owlEvents.trigger('next.owl.carousel', [300]);
    })
    // Go to the previous item
    $('.events .owlcarousel-control .side-nav .prev').click(function () {
        owlEvents.trigger('prev.owl.carousel', [300]);
    })
    $('.events .owlcarousel-control .owl-dots .owl-dot').click(function () {
        owlEvents.trigger('to.owl.carousel', [$(this).index(), 300]);
    });
    // publications
    var owlPublications = $('.publications-carousel');
    owlPublications.owlCarousel({
        stagePadding: 100,
        margin: 20,
        loop: true,
        rtl: true,
        dotsContainer: '.publications .owlcarousel-control .owl-dots',
        responsive: {
            0: {
                items: 1,
                stagePadding: 30,
            },
            600: {
                items: 2,
                stagePadding: 50,
            },
            1000: {
                items: 4
            }
        }
    });
    // Go to the next item
    $('.publications .owlcarousel-control .side-nav .next').click(function () {
        owlPublications.trigger('next.owl.carousel', [300]);
    })
    // Go to the previous item
    $('.publications .owlcarousel-control .side-nav .prev').click(function () {
        owlPublications.trigger('prev.owl.carousel', [300]);
    })
    $('.publications .owlcarousel-control .owl-dots .owl-dot').click(function () {
        owlPublications.trigger('to.owl.carousel', [$(this).index(), 300]);
    });
});