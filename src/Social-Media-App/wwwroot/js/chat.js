getCurrentUserId()
    .then((currentUserId) => {
        if (currentUserId) {

            var chatConnection = new signalR.HubConnectionBuilder().withUrl('/chathub').build();

            document.getElementById('sendButton').addEventListener('click', function () {
                var messageInput = document.getElementById('messageInput');

                chatConnection.invoke('SendMessage', messageInput.value, currentUserId)
                    .then(function () {
                        console.log(currentUserId);
                    })
                    .catch(function (error) {
                        console.error('Error sending message:', error);
                    });
            });

            chatConnection.on('ReceiveMessage', function (senderId, message) {
                console.log('The message is ' + message + '; The sender is ' + senderId);
                var isSeenBySender = currentUserId == senderId;
                var generatedMessage = generateMessageHTML(message, isSeenBySender);
                appendMessage(generatedMessage);
            })

            chatConnection.start()
                .then(function () {
                    console.log('SignalR chat connection started.');
                })
                .catch(function (error) {
                    console.error('Error starting SignalR chat connection:', error);
                });
        }
    })
    .catch((error) => {
        console.error('Error retrieving current user ID:', error);
    });

function generateMessageHTML(message, isSeenBySender) {
    var messageClass = isSeenBySender ? "outgoing" : "incoming";
    var messageText = message;

    var messageHTML = '<div class="message ' + messageClass + '">';
    messageHTML += messageText;
    messageHTML += '</div>';
    console.log('Created Message!');

    return messageHTML;
}

function appendMessage(message) {
    var messagePanel = document.getElementById("message-panel");
    console.log(messagePanel);
    messagePanel.innerHTML += message;
    console.log('Appended Message!');
}

async function getCurrentUserId() {
    try {
        const response = await fetch('/api/user/current');
        if (!response.ok) {
            throw new Error('Failed to fetch current user ID');
        }
        const currentUserId = await response.text();
        return currentUserId;
    } catch (error) {
        console.error('Error retrieving current user ID:', error);
        return null;
    }
}