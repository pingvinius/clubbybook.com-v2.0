/// <reference path="cb-admin-ui-constants.js" />
/// <reference path="cb-admin-helper.js" />

// Delete entity by specified URL and id.
ApiProxy.prototype.deleteEntity = function (url, id, options)
{
    options = $.extend({}, apiProxy.defaults, options);

    var dataToSend = JSON.stringify(
    {
        id: id
    });

    $.ajax(
    {
        url: url,
        type: "POST",
        data: dataToSend,
        cache: false,
        async: false,
        processData: false,
        success: function (data)
        {
            if (isDefinedAndNotNull(data) && isDefinedAndNotNull(data.Result) && data.Result.toString().toLowerCase() === "true")
            {
                options.onSuccess(data.Object);
            }
            else
            {
                commonHelper.showError(UIErrorMessages.DeleteEntity_GenericError, data.ErrorMessage);
                options.onError();
            }
    }
    });
};

// Mark feedback notification as read
ApiProxy.prototype.markFeedbackNotificationAsRead = function (id, options)
{
    options = $.extend({}, apiProxy.defaults, options);

    var dataToSend = JSON.stringify(
    {
        id: id
    });

    $.ajax(
    {
        url: "/Shared/Api/MarkFeedbackNotificationAsRead",
        type: "POST",
        data: dataToSend,
        cache: false,
        async: false,
        processData: false,
        success: function (data)
        {
            if (isDefinedAndNotNull(data) && isDefinedAndNotNull(data.Result) && data.Result.toString().toLowerCase() === "true")
            {
                options.onSuccess(data.Object);
            }
            else
            {
                commonHelper.showError(UIErrorMessages.MarkFeedbackAsRead_GenericError, data.ErrorMessage);
                options.onError();
            }
        }
    });
};