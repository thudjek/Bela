﻿var roomHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/roomHub")
    .build();

roomHubConnection.on("UpdateUserList", function () {
    renderUserList($(".filter-username-input").val());
});

roomHubConnection.on("UpdateUsersLayout", function () {
    renderUsersLayout();
});

roomHubConnection.on("JoinRoomGroup", function () {
    joinRoomGroup();
});

roomHubConnection.on("GameStarted", function () {
    window.location.href = "/Game";
});

roomHubConnection.start();