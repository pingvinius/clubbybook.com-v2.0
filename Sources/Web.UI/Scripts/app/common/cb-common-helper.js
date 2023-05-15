function CommonHelper()
{
    var instance = this;

    // #region Show Warning/Error/Message

    instance.showServerAjaxError = function (message, consoleMessage, xhr)
    {
        if (!isDefinedAndNotNull(message))
        {
            throwJsException("commonHelper.showServerAjaxError", "The 'message' parameter is missing.");
            return;
        }

        console.log(message);

        if (isDefinedAndNotNull(consoleMessage))
        {
            console.log(consoleMessage);
        }

        if (isDefinedAndNotNull(xhr) && isDefinedAndNotNull(xhr.responseText))
        {
            console.log(xhr.responseText);
        }

        $.lighttooltip(
        {
            text: message,
            title: UITooltipTitles.DefaultError,
            type: TooltipType.Error
        });
    };

    instance.showMessage = function (message, consoleMessage)
    {
        if (!isDefinedAndNotNull(message))
        {
            throwJsException("commonHelper.showMessage", "The 'message' parameter is missing.");
            return;
        }

        console.log(message);

        if (isDefinedAndNotNull(consoleMessage))
        {
            console.log(consoleMessage);
        }

        $.lighttooltip(
        {
            text: message,
            title: UITooltipTitles.DefaultInformation,
            type: TooltipType.Information,
            timeout: 5000
        });
    };

    instance.showError = function (message, consoleMessage)
    {
        if (!isDefinedAndNotNull(message))
        {
            throwJsException("commonHelper.showError", "The 'message' parameter is missing.");
            return;
        }

        console.log(message);

        if (isDefinedAndNotNull(consoleMessage))
        {
            console.log(consoleMessage);
        }

        $.lighttooltip(
        {
            text: message,
            title: UITooltipTitles.DefaultError,
            type: TooltipType.Error
        });
    };

    instance.showWarning = function (message, consoleMessage)
    {
        if (!isDefinedAndNotNull(message))
        {
            throwJsException("commonHelper.showWarning", "The 'message' parameter is missing.");
            return;
        }

        console.log(message);

        if (isDefinedAndNotNull(consoleMessage))
        {
            console.log(consoleMessage);
        }

        $.lighttooltip(
        {
            text: message,
            title: UITooltipTitles.DefaultWarning,
            type: TooltipType.Information
        });
    };

    //#endregion
}

var commonHelper = new CommonHelper();