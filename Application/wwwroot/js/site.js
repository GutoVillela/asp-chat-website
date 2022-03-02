var errorToast;
var toastInstance;

$(document).ready(function () {
    errorToast = document.getElementById('errorToast');
    toastInstance = bootstrap.Toast.getOrCreateInstance(errorToast);


    $('#sendMessageButton').on('click', function () {

        let chatRoomId = $('#selectedChat').val();
        let message = $('#message').val();

        $.ajax({
            method: 'POST',
            url: sendMessageUrl,
            data: { chatRoomId: chatRoomId, message: message }
        }).done(function (response) {
            $('#message').val('');
        }).fail(function (error) {
            ToastErrorMessage(error.responseText);
        });
    });

});

function AppendMessageToView(messageData, messageOwner) {
    console.log(messageData);
    $('#messagesArea').append(CreateMessageCard(messageData.authorName, messageData.message, messageOwner));
    $('#messagesArea').scrollTop($('#messagesArea').prop("scrollHeight"));
}

function CreateMessageCard(authorName, messageText, messageOwner) {
    let alignClass = messageOwner ? 'ms-auto' : 'me-auto';
    return '<div class="card text-dark bg-light mb-3 ' + alignClass + '" style="max-width: 18rem;">'
        + '<div class="card-header">' + authorName + '</div>'
        + '<div class="card-body">'
        + '<p class="card-text">' + messageText + '</p>'
        + '</div>'
        + '</div>';
}


function ToastErrorMessage(errorMessage) {
    if (toastInstance === 'undefined') {
        console.error('It was not possible to get your Toast instance');
        return;
    }

    $('#errorToast #errorText').text(errorMessage);
    toastInstance.show();
}