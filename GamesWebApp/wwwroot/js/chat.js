var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
$("#sendMessage").prop('disabled', true);

connection.on("ReceiveMessage", function (user, message) {
    var d = new Date();
    var datestring = ("0" + d.getDate()).slice(-2) + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" +
        d.getFullYear() + " " + ("0" + d.getHours()).slice(-2) + ":" + ("0" + d.getMinutes()).slice(-2);
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = datestring + " " + user + ": " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    $("#messagesList").append(li);

});

connection.start().then(function () {
    $("#sendMessage").prop('disabled', false);
}).catch(function (err) {
    return console.error(err.toString());
});


$("#sendMessage").click(function () {

    var sender = $("#sender").val();

    var receiver = $("#receiver").val();
    var message = $("#message").val();


    $.ajax({
        type: 'GET',
        url: '/Games/Chat?message=' + message
    });

    if (receiver != "") {
        sender += " pw";
        //send to a user
        var d = new Date();
        var datestring = ("0" + d.getDate()).slice(-2) + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" +
            d.getFullYear() + " " + ("0" + d.getHours()).slice(-2) + ":" + ("0" + d.getMinutes()).slice(-2);
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = datestring + " " + sender + " to " + receiver + ": " + msg;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        $("#messagesList").append(li);

        connection.invoke("SendMessageToGroup", sender, receiver, message).catch(function (err)
        {
            return console.error(err.toString());
        });
    }
    else {
        //send to all
        connection.invoke("SendMessage", sender, message).catch(function (err)


        {
            return console.error(err.toString());
        });
    }


    event.preventDefault();

});