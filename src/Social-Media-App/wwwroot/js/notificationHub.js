var notificationConnection = new signalR.HubConnectionBuilder().withUrl('/notificationhub').build();

notificationConnection.on('ReceiveNotification', function () {
    let chatElement = document.getElementById('chat');
    chatElement.setAttribute('style', 'color:red !important');
});

notificationConnection.start()
    .then(function () {
        console.log('SignalR connection started.');
    })
    .catch(function (error) {
        console.error('Error starting SignalR connection:', error);
    });