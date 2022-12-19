$(document).ready(function () {
    $.connection.hub.url = hubConnectionUrl;

    var notificationHub = $.connection.alertHub;

    notificationHub.client.notify = function (updateNode) {
        $.jGrowl(updateNode.Message + ' MessageID:' + updateNode.ID);
    }

    $.connection.hub.start().done(function () {

//    $('#testme').click(function () {
//        tester.server.hello();
//    });

    });
});