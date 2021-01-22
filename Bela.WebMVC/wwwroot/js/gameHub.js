var gameHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/gameHub")
    .build();

gameHubConnection.on("JoinGameGroup", function () {
    joinGameGroup();
});

gameHubConnection.on("TrumpCalled", function (data) {
    currentPhase = data.roundPhase;
    renderSelectedTrump(data.selectedTrump, data.trumpSelectedBy);
    renderCurrentPlayerScreen(getOnScreenPositionFromReturnedData(data, true, false, false, false));
    renderCardsInHand();
});

gameHubConnection.on("CallMade", function (data) {

    renderSpeechBubbleForCall(getOnScreenPositionFromReturnedData(data, false, true, false, false), data.callValue)

    if (currentPhase != data.roundPhase) {
        currentPhase = data.roundPhase;
        resetCountdownProgressBars();

        setTimeout(function () {
            resetSpeechBubbles();
            showCallsOnTable(data);
            getRoundScores();
            setTimeout(function () {
                resetCallCards();
                renderCurrentPlayerScreen(getOnScreenPositionFromReturnedData(data, true, false, false, false));
                renderCardsInHand();
            }, 2500);
        }, 2500);
    }
    else {
        renderCurrentPlayerScreen(getOnScreenPositionFromReturnedData(data, true, false, false, false));
    }
});

gameHubConnection.on("TurnPassed", function (data) {
    if (currentPhase != data.roundPhase) {
        currentPhase = data.roundPhase;
        resetCountdownProgressBars();

        setTimeout(function () {
            resetSpeechBubbles();
            showCallsOnTable(data);
            getRoundScores();
            setTimeout(function () {
                resetCallCards();
                renderCurrentPlayerScreen(getOnScreenPositionFromReturnedData(data, true, false, false, false));
                renderCardsInHand();
            }, 2500);
        }, 2500);
    }
    else {
        renderCurrentPlayerScreen(getOnScreenPositionFromReturnedData(data, true, false, false, false), data.isLast);
    }
});

gameHubConnection.on("CardPlayed", function (data) {
    renderPlayedCard(getOnScreenPositionFromReturnedData(data, false, false, true, false), data.playedCardUrl);
    if (!data.isGameOver) {
        if (data.isNewRound) {
            currentRoundId = data.currentRoundId;
            currentPhase = data.roundPhase;
            setTimeout(function () {
                resetTable();
                if (data.roundAlert != "") {
                    swalHelper.alertMessageWithTimer(data.roundAlert, 1000);
                    setTimeout(function () {
                        setForStartOfNewRound();
                        renderDealerIcon(getOnScreenPositionFromReturnedData(data, false, false, false, true));
                        renderCurrentPlayerScreen(getOnScreenPositionFromReturnedData(data, true, false, false, false));
                    }, 1000);
                }
                else {
                    setForStartOfNewRound();
                    renderDealerIcon(getOnScreenPositionFromReturnedData(data, false, false, false, true));
                    renderCurrentPlayerScreen(getOnScreenPositionFromReturnedData(data, true, false, false, false));
                }
            }, 1500);
        }
        else {
            if (data.belaCalled) {
                renderSpeechBubbleForCall(getOnScreenPositionFromReturnedData(data, false, false, true, false), "BELA")
                setTimeout(function () {
                    resetSpeechBubbles();
                }, 1500);
            }
            if (currentPhase != data.roundPhase) {
                currentPhase = data.roundPhase;
                setTimeout(function () {
                    getRoundScores();
                    resetTable();
                    renderCurrentPlayerScreen(getOnScreenPositionFromReturnedData(data, true, false, false, false));
                }, 1500);
            }
            else {
                if (data.belaCalled) {
                    getRoundScores();
                }
                renderCurrentPlayerScreen(getOnScreenPositionFromReturnedData(data, true, false, false, false));
            }
        }
    }
    else {
        redirectToEndScreen();
    }
});

gameHubConnection.on("GameQuit", function (quitUsername, opponent1Username, opponent2Username) {
    redirectToEndScreenQuit(quitUsername, opponent1Username, opponent2Username);
});

gameHubConnection.on("TimerElapsed", function (quitUsername, opponent1Username, opponent2Username) {
    redirectToEndScreenQuit(quitUsername, opponent1Username, opponent2Username);
});

gameHubConnection.start();