function FeedbackHelper()
{
    var instance = this;

    this.init = function ()
    {
        $("#feedback-modal-div .modal-footer .btn-primary").bind("click", function (e)
        {
            // Fetch message
            var message = $("#feedback-modal-div .modal-body textarea").val();

            // Send message
            apiProxy.sendFeedback(message,
            {
                onSuccess: function(data)
                {
                    commonHelper.showMessage(UIMessages.Feedback_Success);
                }
            });

            // Clear entered message
            $("#feedback-modal-div .modal-body textarea").val("");

            // Close modal window
            var modalDialog = $("#feedback-modal-div")[0];
            $(modalDialog).modal("hide");
        });
    }
}

var feedbackHelper = null;

$().ready(function ()
{
    if (feedbackHelper === null)
    {
        feedbackHelper = new FeedbackHelper();
        feedbackHelper.init();
    }
});