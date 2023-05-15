var TooltipType =
{
    None: 0,
    Information: 1,
    Error: 2
};

(function ($)
{
    var defaults =
    {
        text: "jQuery Light Tooltip Plugin",
        title: "Tooltip Title",
        type: TooltipType.None,
        timeout: 3000,
        tooltipClass: "lighttooltip",
        titleClass: "lighttooltip_title",
        contentClass: "lighttooltip_content",
        customWidth: 600
    };

    function TooltipItem(settings)
    {
        var tooltipUI = null;

        createTooltipUI();
        showInternal();

        function showInternal()
        {
            if (tooltipUI !== null && $(tooltipUI).css("display") !== "none")
            {
                throwJsException("TooltipItem.showInternal", "The tooltip is already shown.");
            }

            updateTooltipLayout();

            $(tooltipUI).fadeIn(500);

            subscribeEvents();
            setupTimer();
        }

        function closeInternal()
        {
            if (tooltipUI === null)
            {
                return;
            }

            describeEvents();

            $(tooltipUI).fadeOut(1000);
            $(tooltipUI).remove();

            tooltipUI = null;
        }

        function createTooltipUI()
        {
            tooltipUI = $("<div></div>").addClass(settings.tooltipClass)

            if (settings.type !== TooltipType.None)
            {
                tooltipUI.append($("<div></div>")
                    .addClass(settings.titleClass)
                    .append("<h1>" + settings.title + "</h1>"));
            }

            tooltipUI.append($("<div></div>")
                .addClass(settings.contentClass)
                .html(settings.text))
                .appendTo($("body"));
        }

        function updateTooltipLayout()
        {
            var documentSize = getDocumentSize();
            var documentScrollOffset = getDocumentScrollOffset();

            var windowHeight = $(tooltipUI).height();
            var windowWidth = isDefinedAndNotNull(settings.customWidth) ? settings.customWidth : $(tooltipUI).width();

            var showedTooltips = $("div." + settings.tooltipClass);
            var lastShowedTooltip = showedTooltips.length > 1 ? showedTooltips.get(showedTooltips.length - 2) : null;

            var topOffset = lastShowedTooltip !== null ?
                lastShowedTooltip.offsetTop + lastShowedTooltip.offsetHeight + 2 : // based on last showed tooltip
                documentSize.height / 4 + documentScrollOffset.top; // based on the display height

            if (isDefinedAndNotNull(settings.customWidth))
            {
                $(tooltipUI).css("width", settings.customWidth);
            }

            $(tooltipUI).css("top", topOffset);
            $(tooltipUI).css("left", documentSize.width / 2 - windowWidth / 2 + documentScrollOffset.left);
        }

        function setupTimer()
        {
            var timer = setTimeout(function ()
            {
                closeInternal();
                clearTimeout(timer);
            }, settings.timeout);
        }

        function subscribeEvents()
        {
            $(window).bind("scroll", function ()
            {
                updateTooltipLayout();
            });

            $(window).bind("resize", function ()
            {
                updateTooltipLayout();
            });

            $("." + settings.tooltipClass).bind("click", function (e)
            {
                e.preventDefault();
                closeInternal();
            });
        }

        function describeEvents()
        {
            $(window).unbind("scroll");
            $(window).unbind("resize");

            $("." + settings.tooltipClass).unbind("click");
        }

        function getDocumentScrollOffset()
        {
            return { left: window.pageXOffset, top: window.pageYOffset };
        }

        function getDocumentSize()
        {
            var documentWidth = 0;
            var documentHeight = 0;

            if (isDefined(window.innerWidth))
            {
                documentWidth = window.innerWidth,
                documentHeight = window.innerHeight
            }
            else if (isDefined(document.documentElement) && isDefined(document.documentElement.clientWidth) && document.documentElement.clientWidth != 0)
            {
                documentWidth = document.documentElement.clientWidth,
                documentHeight = document.documentElement.clientHeight
            }
            else
            {
                documentWidth = document.getElementsByTagName("body")[0].clientWidth,
                documentHeight = document.getElementsByTagName("body")[0].clientHeight
            }

            return { width: documentWidth, height: documentHeight };
        }
    }

    $.lighttooltip = function (options)
    {
        return new TooltipItem($.extend({}, defaults, options));
    };
})(jQuery);