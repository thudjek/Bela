var swalHelper = {

    alertError: function (error, target = 'body') {
        Swal.fire({
            icon: "error",
            text: error,
            target: target == 'body' ? target : document.getElementById(target)
        });
    },

    alertErrorWithCallback: function (error, callback, target = 'body', paramForCallback = null) {
        Swal.fire({
            icon: "error",
            text: error,
            target: target == 'body' ? target : document.getElementById(target)
        }).then(function () {
            if (paramForCallback == null)
                callback();
            else {
                callback(paramForCallback);
            }
        });
    },

    alertMessageWithTimer: function (message, miliseconds) {
        Swal.fire({
            width: '24rem',
            position: 'center',
            backdrop: false,
            target: document.getElementById("gameContainer"),
            title: message,
            showConfirmButton: false,
            timer: miliseconds
        })
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
                  "<a href='#' onclick='selectTrump(1, " + isLast + ")'><img src='imgs/suits/herc.png'/></a>&nbsp;&nbsp;" +
                  "<a href='#' onclick='selectTrump(2, " + isLast + ")'><img src='imgs/suits/pik.png'/></a>&nbsp;&nbsp;" +
                  "<a href='#' onclick='selectTrump(3, " + isLast + ")'><img src='imgs/suits/karo.png'/></a>&nbsp;&nbsp;" +
                  "<a href='#' onclick='selectTrump(4, " + isLast + ")'><img src='imgs/suits/tref.png'/></a><br /><br />" +
                  (isLast ? "<p style='color: red;'>Zadnji ste i morate zvati</p>" :
                  "<a type='button' style='color: white;' onclick='selectTrump(0)' class='btn btn-primary'>Dalje</a>"),
            allowEnterKey: false,
            showConfirmButton: false,
            showCancelButton: false,
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
            allowEscapeKey: false
        });
    },

    showBelaDialog: function (cardString, playCard) {
        Swal.fire({
            width: '24rem',
            background: '#36454f',
            backdrop: false,
            target: document.getElementById("gameContainer"),
            html: "<h2 style='color: white;'>Zovi belu?</h2>",
            showCancelButton: true,
            confirmButtonText: 'Zovi',
            cancelButtonText: 'Dalje',
            allowEnterKey: false,
            allowEscapeKey: false,
            allowOutsideClick: false
        }).then((result) => {
            if (result.isConfirmed) {
                lockedPlayCard = false;
                playCard(cardString, true);
            }
            else {
                lockedPlayCard = false;
                playCard(cardString, false);
            }
        });
    },

    showLeaveGameDialog: function (callback) {
        Swal.fire({
            width: '24rem',
            background: '#36454f',
            backdrop: true,
            target: document.getElementById("gameContainer"),
            html: "<h2 style='color: white;'>Želite li izači iz igre?</h2>",
            showCancelButton: true,
            confirmButtonText: 'Izađi',
            cancelButtonText: 'Odustani',
            allowEnterKey: false,
            allowEscapeKey: false,
            allowOutsideClick: false
        }).then((result) => {
            if (result.isConfirmed) {
                callback();
            }
        });
    }
}