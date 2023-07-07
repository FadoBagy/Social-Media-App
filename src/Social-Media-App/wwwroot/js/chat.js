var chatConnection = new signalR.HubConnectionBuilder().withUrl('/chathub').build();

document.getElementById('sendButton').addEventListener('click', function () {
    chatConnection.invoke('SendMessage')
        .then(function () {
            console.log('Message sent successfully.');
        })
        .catch(function (error) {
            console.error('Error sending message:', error);
        });
});

chatConnection.on('ReceiveMessage', function () {
    console.log('Received message!');
})

chatConnection.start()
    .then(function () {
        console.log('SignalR chat connection started.');
    })
    .catch(function (error) {
        console.error('Error starting SignalR chat connection:', error);
    });