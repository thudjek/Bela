var gameHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/gameHub")
    .build();

gameHubConnection.on("JoinGameGroup", function () {
    joinGameGroup();
});

gameHubConnection.on("TrumpCalled", function (data) {
    currentPhase = data.roundPhase;
    renderSelectedTrump(data.selectedTrump, data.trumpSelectedBy);
    renderCurrentPlayerScreen(getOnScreenPositionFromReturnedData(data));
    renderCardsInHand();
});

gameHubConnection.on("TurnPassed", function (data) {
    renderCurrentPlayerScreen(getOnScreenPositionFromReturnedData(data), data.isLast);
});

gameHubConnection.start();