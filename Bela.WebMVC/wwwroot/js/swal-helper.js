var swalHelper = {

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
    }
}