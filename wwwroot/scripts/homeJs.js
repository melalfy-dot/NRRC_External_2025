$(document).ready(function () {
  let homBanner = new Swiper(".homBanner", {
    pagination: {
      el: ".homBanner .swiper-pagination",
      clickable: true
    },
  });
  let homeServices = new Swiper(".homeServices", {
    spaceBetween: 40,
    slidesPerView: 2,
    pagination: {
      el: ".homeServices .swiper-pagination",
      clickable: true
    },
    navigation: {
      nextEl: ".homeServices .swiper-button-next",
      prevEl: ".homeServices .swiper-button-prev",
    },
  });
  let puplcSwiper = new Swiper(".puplcSwiper", {
    spaceBetween: 16,
    slidesPerView: 4,
    pagination: {
      el: ".puplcSwiper .swiper-pagination",
      clickable: true
    },
    navigation: {
      nextEl: ".puplcSwiper .swiper-button-next",
      prevEl: ".puplcSwiper .swiper-button-prev",
    },
  });
  let partnersSwiper = new Swiper(".partnersSwiper", {
    slidesPerView: 6,
    spaceBetween: 16,
    navigation: {
      nextEl: ".partners .swiper-button-next",
      prevEl: ".partners .swiper-button-prev",
    },
        breakpoints: {
      // when window width is >= 320px
      320: {
        slidesPerView: 1,
        spaceBetween: 0,
      },
      // when window width is >= 640px
      767: {
        slidesPerView: 2,
      },
      992: {
        slidesPerView: 4,
      },
      1200: {
        slidesPerView: 5,
      },
      1400: {
        slidesPerView: 6,
      },
      1700: {
        slidesPerView: 6,
     
      },
    }
  });

  // disable swiper in desktop
  if (window.matchMedia('(min-width: 1100px)').matches){
    $('.disableDesktop').addClass('swiper-no-swiping');
    }

});

jQuery(function ($) {
  "use strict";

  var counterUp = window.counterUp["default"]; // import counterUp from "counterup2"

  var $counters = $(".counter");

  /* Start counting, do this on DOM ready or with Waypoints. */
  $counters.each(function (ignore, counter) {
    var waypoint = new Waypoint({
      element: $(this),
      handler: function () {
        counterUp(counter, {
          duration: 2000,
          delay: 10,
        });
        // this.destroy();
      },
      offset: "bottom-in-view",
    });
  });
});

