/// <reference path="cb-admin-ui-constants.js" />
/// <reference path="cb-admin-api-proxy.js" />

function AdminCommonHelper()
{
    var instance = this;

    instance.deleteEntity = function (sender, url, id)
    {
        if (!isDefinedAndNotNull(url))
        {
            throwJsException("adminCommonHelper.deleteEntity", "The 'url' parameter is missing.");
        }

        if (!isDefinedAndNotNull(id))
        {
            throwJsException("adminCommonHelper.deleteEntity", "The 'id' parameter is missing.");
        }

        // Skip removing profiles for a while
        if (url.indexOf("/admin/profile/delete") !== -1)
        {
            commonHelper.showWarning(UIErrorMessages.DeleteEntity_NotSupportedYet);
            return;
        }

        // Show modal window to confirm and do it if user agrees
        $.fn.confirmModal({
            heading: UIMessages.DeleteEntity_PopupHeader,
            body: UIMessages.DeleteEntity_Question,
            confirmButtonName: UIMessages.Generic_Yes,
            closeButtonName: UIMessages.Generic_Cancel,
            callback: function ()
            {
                apiProxy.deleteEntity(url, id, 
                {
                    onSuccess: function(data)
                    {
                        commonHelper.showMessage(UIMessages.DeleteEntity_Success);
                        reloadPage();
                    },
                });
            }
        });
    };

    instance.markFeedbackNotificationAsRead = function (sender, id)
    {
        if (!isDefinedAndNotNull(id))
        {
            throwJsException("adminCommonHelper.markFeedbackNotificationAsRead", "The 'id' parameter is missing.");
        }

        apiProxy.markFeedbackNotificationAsRead(id,
        {
            onSuccess: function (data)
            {
                commonHelper.showMessage(UIMessages.MarkFeedbackAsRead_Success);
                $($(sender).parents("tr")[0]).removeClass("success");
            },
        });
        
    };
}

var adminCommonHelper = new AdminCommonHelper();