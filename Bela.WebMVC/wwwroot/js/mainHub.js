var mainHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/mainHub")
    .build();

mainHubConnection.on("SendInviteToUser", function (ownerUsername, roomId) {
    showInviteToast(ownerUsername, roomId);
});

mainHubConnection.on("RoomDroped", function () {
    swalHelper.alertDropedRoom();
});

mainHubConnection.on("KickedFromRoom", function () {
    swalHelper.alertKickedFromRoom();
});

mainHubConnection.start();