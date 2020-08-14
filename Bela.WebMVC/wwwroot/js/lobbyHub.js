var lobbyHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/lobbyHub")
    .build();

lobbyHubConnection.on("TryJoinAuthGroup", function () {
    if (isUserAuth)
        lobbyHubConnection.invoke("JoinAuthGroup");
});

lobbyHubConnection.on("UpdateUserList", function () {
    renderUserList($(".filter-username-input").val());
});

lobbyHubConnection.on("UpdateRoomList", function () {
    renderRoomList($("#filterRoomNameInput").val());
});

lobbyHubConnection.start();