var notyf = new Notyf({
    duration: 5000,
    position: {
        x: 'right',
        y: 'top',
    },
    types: [
        {
            type: 'invite',
            background: '#6E4A54',
            icon: false,
            dismissible: true
        }
    ]
});

function showInviteToast(username, roomId) {
    notyf.open({
        type: "invite",
        message: username + " Vas poziva u igru <br /> <a href='#' onclick='joinRoom(" + roomId + ", false, null, true)'>Prihvati</a>"
    });
}

function joinRoom(roomId, isPrivate, roomPassword = "", isInvite = false) {
    $.ajax({
        url: "/Room/JoinRoom",
        dataType: "json",
        data: { roomId: roomId, isPrivate: isPrivate, roomPassword: roomPassword, isInvite: isInvite },
        cache: false,
        success: function (data) {
            if (data.success) {
                window.location.href = "/Room";
            }
            else {
                if (isPrivate && data.error.includes("lozink")) {
                    $("#roomPasswordValidation").html(data.error);
                }
                else {
                    swalHelper.alertError(data.error);
                }
            }
        },
        error: function (data) {
            if (data.status == '401') {
                window.location.href = "/Home";
            }
            else {
                swalHelper.alertError("Dogodila se greška");
            }
        }
    });
}

function openUserDetailsModal(modalElementId, userId = null) {
    var modalElement = $("#" + modalElementId);
    var url = modalElement.data("url");
    if(url != null)
        url += "/" + userId;

    $.get(url, function (data) {
        modalElement.html(data);
        modalElement.modal("show");
    });
}
