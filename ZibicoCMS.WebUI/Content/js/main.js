/*
	Foxstar Theme Scripts
*/
var lat;
var lon;
(function ($) {
    "use strict";

    $(window).on('load', function () {
        $('body').addClass('loaded');
    });

    /*=========================================================================
        Sticky Header
    =========================================================================*/
    $(function () {
        var header = $("#header"),
            yOffset = 0,
            triggerPoint = 80;
        $(window).on('scroll', function () {
            yOffset = $(window).scrollTop();

            if (yOffset >= triggerPoint) {
                header.addClass("navbar-fixed-top");
            } else {
                header.removeClass("navbar-fixed-top");
            }

        });
    });

    /*=========================================================================
        Initialize smoothscroll plugin
    =========================================================================*/
    smoothScroll.init({
        offset: 60
    });

    /*=========================================================================
        Active venobox
    =========================================================================*/
    var vbSelector = $('.img_popup');
    vbSelector.venobox({
        numeratio: true,
        infinigall: true
    });

    /*=========================================================================
        Active Tooltip
    =========================================================================*/
    $('[data-toggle="tooltip"]').tooltip();

    /*=========================================================================
        Scroll To Top
    =========================================================================*/
    $(window).on('scroll', function () {
        if ($(this).scrollTop() > 100) {
            $('#scroll-to-top').fadeIn();
        } else {
            $('#scroll-to-top').fadeOut();
        }
    });

    /*=========================================================================
            Google Map Settings
    =========================================================================*/

    google.maps.event.addDomListener(window, 'load', init);

    function init() {

        var mapOptions = {
            zoom: 11,
            center: new google.maps.LatLng(lat, lon),
            scrollwheel: false,
            navigationControl: false,
            mapTypeControl: false,
            scaleControl: false,
            draggable: false,
            styles: [{ "featureType": "administrative", "elementType": "all", "stylers": [{ "saturation": "-100" }] }, { "featureType": "administrative.province", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "landscape", "elementType": "all", "stylers": [{ "saturation": -100 }, { "lightness": 65 }, { "visibility": "on" }] }, { "featureType": "poi", "elementType": "all", "stylers": [{ "saturation": -100 }, { "lightness": "50" }, { "visibility": "simplified" }] }, { "featureType": "road", "elementType": "all", "stylers": [{ "saturation": "-100" }] }, { "featureType": "road.highway", "elementType": "all", "stylers": [{ "visibility": "simplified" }] }, { "featureType": "road.arterial", "elementType": "all", "stylers": [{ "lightness": "30" }] }, { "featureType": "road.local", "elementType": "all", "stylers": [{ "lightness": "40" }] }, { "featureType": "transit", "elementType": "all", "stylers": [{ "saturation": -100 }, { "visibility": "simplified" }] }, { "featureType": "water", "elementType": "geometry", "stylers": [{ "hue": "#ffff00" }, { "lightness": -25 }, { "saturation": -97 }] }, { "featureType": "water", "elementType": "labels", "stylers": [{ "lightness": -25 }, { "saturation": -100 }] }]
        };

        var mapElement = document.getElementById('google_map');

        var map = new google.maps.Map(mapElement, mapOptions);

        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(lat, lon),
            map: map,
            title: 'Location!'
        });
    }

})(jQuery);


/*=========================================================================
        Partial Views Load
=========================================================================*/
$(document).ready(function () {
    $("#Anasayfa").load("/Home/PartialHome");
    $("#Hakkimizda").load("/Home/PartialAbout");
    $("#Kategoriler").load("/Home/PartialCategories");
    $("#Menu").load("/Home/PartialMenuFoods");
    $("#ShineFood").load("/Home/PartialShineFood/");
    $("#stuff").load("/Home/PartialStaff/");
    $("#Galeri").load("/Home/PartialPhotoGalleries/");
    $("#Rezervasyon").load("/Home/PartialReservation/");
    $("#Announcement").load("/Home/PartialAnnouncement/");
    $("#Blog").load("/Home/PartialBlogs/");
    $("#Footer").load("/Home/PartialFooter/");

});