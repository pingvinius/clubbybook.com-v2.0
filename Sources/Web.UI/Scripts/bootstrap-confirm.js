(function ($)
{
    $.fn.extend(
    {
        confirmModal: function (options)
        {
            var html =
                '<div class="modal" id="modalConfirmContainer" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">' +
                    '<div class="modal-dialog">' +
                        '<div class="modal-content">' +
                            '<div class="modal-header">' +
                                '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
                                '<h4 id="myModalLabel" class="modal-title">#Heading#</h4>' +
                            '</div>' +
                            '<div class="modal-body text-center">' +
                                '#Body#' +
                            '</div>' +
                            '<div class="modal-footer">' +
                                '<button type="button" class="btn btn-default btn-sm" data-dismiss="modal">#CloseButtonName#</button>' +
                                '<button type="button" class="btn btn-primary btn-sm" id="confirmYesBtn">#ConfirmButtonName#</button>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>';

            var defaults = {
                heading: "Please confirm",
                body: "Body contents",
                confirmButtonName: "Confirm",
                closeButtonName: "Close",
                callback: null
            };

            var options = $.extend(defaults, options);

            html = html
                .replace('#Heading#', options.heading)
                .replace('#Body#', options.body)
                .replace('#ConfirmButtonName#', options.confirmButtonName)
                .replace('#CloseButtonName#', options.closeButtonName);

            $("body").append(html);

            var modalDialogDiv = $("body div#modalConfirmContainer");
            $(modalDialogDiv).modal("show");

            $("body div#modalConfirmContainer button#confirmYesBtn").click(function ()
            {
                if (options.callback != null)
                {
                    options.callback();
                }
                $(modalDialogDiv).modal("hide");
            });

            $(modalDialogDiv).on("hidden.bs.modal", function ()
            {
                $(modalDialogDiv).remove();
            })
        }
    });
})(jQuery);