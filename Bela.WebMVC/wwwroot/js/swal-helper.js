﻿var swalHelper = {

    alertError: function (error) {
        Swal.fire({
            icon: "error",
            text: error
        });
    },

    callCreateRoomModal: function (username) {
        Swal.fire({
            title: "Kreiraj sobu",
            html: "<div class='form-group'>" +
                    "<div class='text-left'>" +
                        "<label>Naziv sobe</label>" + 
                    "</div>" +
                  "<input id='roomName' name='roomName' type='text' class='form-control' value='" + username + " - soba' />" + 
                  "<span id='roomNameValidation' class='text-left text-danger d-block'></span>" +
                  "</div>" + 
                  "<div class='form-check text-left'>" +
                    "<input id='isPrivateRoomCheckbox' name='isPrivateRoomCheckbox' class='form-check-input' type='checkbox' onclick='toggleRoomPasswordTextbox(this)' />" +
                    "<label class='form-check-label' for='isPrivateRoomCheckbox'>Privatna soba</label>" +
                  "</div>" +
                  "<br />" +
                  "<div id='roomPwDiv' class='form-group' hidden>" +
                    "<div class='text-left'>" +
                        "<label>Lozinka sobe</label>" +
                    "</div>" +
                  "<input id='roomPassword' name='roomPassword' type='text' class='form-control' />" +
                  "<span id='roomPasswordValidation' class='text-left text-danger d-block'></span>" +
                  "</div>" +
                  "<div class='form-group'>" +
                    "<button class='btn btn-primary' onclick='createRoom()'>Kreiraj</button>&nbsp;&nbsp;" +
                    "<button class='btn btn-secondary' onclick='Swal.close()'>Odustani</button>" +
                  "</div>",
            showConfirmButton: false,
            showCancelButton: false,
            allowOutsideClick: false
        });
    },

    callJoinPrivateRoomModal: function (roomId) {
        Swal.fire({
            title: "Unesi lozinku sobe",
            html: "<div id='roomPwDiv' class='form-group'>" +
                    "<input id='roomPassword' name='roomPassword' type='text' class='form-control' placeholder='Lozinka sobe' />" +
                    "<span id='roomPasswordValidation' class='text-left text-danger d-block'></span>" +
                  "</div>" +
                  "<div class='form-group'>" +
                    "<button class='btn btn-primary' onclick=joinPrivateRoom(" + roomId + ")>Potvrdi</button>&nbsp;&nbsp;" +
                    "<button class='btn btn-secondary' onclick='Swal.close()'>Odustani</button>" +
                  "</div>",
            showConfirmButton: false,
            showCancelButton: false,
            allowOutsideClick: false
        });
    },

    alertDropedRoom: function () {
        Swal.fire({
            icon: "error",
            text: "Soba je raspuštena"
        }).then(function () {
            if (window.location.pathname.toLowerCase().includes("/room")) {
                window.location.href = "/Lobby";
            }
        });
    },

    alertKickedFromRoom: function () {
        Swal.fire({
            icon: "error",
            text: "Izbačeni ste iz sobe"
        }).then(function () {
            if (window.location.pathname.toLowerCase().includes("/room")) {
                window.location.href = "/Lobby";
            }
        });
    },

    showCallTrumpDialog: function (isLast) {
        Swal.fire({
            width: '24rem',
            background: '#36454f',
            backdrop: false,
            target: document.getElementById("gameContainer"),
            html: "<h3 style='color: white;'>Zovi aduta</h3><br />" +
                  "<a href='#' onclick='selectTrump(1)'><img src='imgs/suits/herc.png'/></a>&nbsp;&nbsp;" +
                  "<a href='#' onclick='selectTrump(2)'><img src='imgs/suits/pik.png'/></a>&nbsp;&nbsp;" +
                  "<a href='#' onclick='selectTrump(3)'><img src='imgs/suits/karo.png'/></a>&nbsp;&nbsp;" +
                  "<a href='#' onclick='selectTrump(4)'><img src='imgs/suits/tref.png'/></a><br /><br />" +
                  (isLast ? "<p style='color: red;'>Zadnji ste i morate zvati</p>" :
                  "<a type='button' style='color: white;' onclick='selectTrump(0)' class='btn btn-primary'>Dalje</a>"),
            allowEnterKey: false,
            showConfirmButton: false,
            showCancelButton: false,
            allowOutsideClick: false,
            allowEscapeKey: false
        });
    },

    showCallDialog: function () {
        Swal.fire({
            width: '24rem',
            background: '#36454f',
            backdrop: false,
            target: document.getElementById("gameContainer"),
            html: "<h3 style='color: white;'>Imate li zvanja?</h3><br />" +
                  "<a type='button' style='color: white;' onclick='makeACall(true)' class='btn btn-primary'>Zovi</a>&nbsp;&nbsp;" + 
                  "<a type='button' style='color: white;' onclick='makeACall(false)' class='btn btn-secondary'>Dalje</a>",
            allowEnterKey: false,
            showConfirmButton: false,
            showCancelButton: false,
            allowOutsideClick: false,
            allowEscapeKey: false
        });
    }
}