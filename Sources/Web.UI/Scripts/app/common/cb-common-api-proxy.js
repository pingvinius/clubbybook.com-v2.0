function ApiProxy()
{
    var instance = this;

    $.ajaxSetup(
    {
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        error: function (xhr, status, error)
        {
            console.log("The ajax request to the server (" + this.url + ") has been failed.");

            if (isDefinedAndNotNull(xhr) && isDefinedAndNotNull(xhr.responseText))
            {
                console.log(xhr.responseText);
            }

            $.lighttooltip(
            {
                text: UIErrorMessages.Ajax_GenericError,
                title: UITooltipTitles.DefaultError,
                type: TooltipType.Error
            });
        }
    });

    instance.defaults =
    {
        onSuccess: function (data) { },
        onError: function() { }
    };

    instance.getCountries = function (selectedCountryId, options)
    {
        options = $.extend({}, instance.defaults, options);

        $.ajax(
        {
            url: "/Shared/Api/GetCountries",
            type: "GET",
            data: { selectedCountryId: selectedCountryId },
            cache: true,
            async: true,
            success: function (data)
            {
                if (isDefinedAndNotNull(data) && isDefinedAndNotNull(data.Result) && data.Result.toString().toLowerCase() === "true")
                {
                    options.onSuccess(data.Object);
                }
                else
                {
                    commonHelper.showError(UIErrorMessages.CityChooser_FailedToLoadCountries, data.ErrorMessage);
                    options.onError();
                }
            }
        });
    };

    instance.getDistricts = function (selectedCountryId, selectedDistrictId, options)
    {
        options = $.extend({}, instance.defaults, options);

        $.ajax(
        {
            url: "/Shared/Api/GetDistricts",
            type: "GET",
            data:
            {
                selectedCountryId: selectedCountryId,
                selectedDistrictId: selectedDistrictId
            },
            cache: true,
            async: true,
            success: function (data)
            {
                if (isDefinedAndNotNull(data) && isDefinedAndNotNull(data.Result) && data.Result.toString().toLowerCase() === "true")
                {
                    options.onSuccess(data.Object);
                }
                else
                {
                    commonHelper.showError(UIErrorMessages.CityChooser_FailedToLoadDistricts, data.ErrorMessage);
                    options.onError();
                }
            }
        });
    };

    instance.getCities = function (selectedCountryId, selectedDistrictId, selectedCityId, options)
    {
        options = $.extend({}, instance.defaults, options);

        $.ajax(
        {
            url: "/Shared/Api/GetCities",
            type: "GET",
            data:
            {
                selectedCountryId: selectedCountryId,
                selectedDistrictId: selectedDistrictId,
                selectedCityId: selectedCityId
            },
            cache: true,
            async: true,
            success: function (data)
            {
                if (isDefinedAndNotNull(data) && isDefinedAndNotNull(data.Result) && data.Result.toString().toLowerCase() === "true")
                {
                    options.onSuccess(data.Object);
                }
                else
                {
                    commonHelper.showError(UIErrorMessages.CityChooser_FailedToLoadCities, data.ErrorMessage);
                    options.onError();
                }
            }
        });
    };

    instance.uploadImage = function (dataToSend, options)
    {
        options = $.extend({}, instance.defaults, options);

        $.ajax(
        {
            url: "/Shared/Uploader/UploadImage/",
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
                    commonHelper.showError(UIErrorMessages.UploadImage_UploadingFailed, data.ErrorMessage);
                    options.onError();
                }
            }
        });
    };

    instance.sendFeedback = function(message, options)
    {
        options = $.extend({}, instance.defaults, options);

        var dataToSend = JSON.stringify(
        {
            message: message
        });

        $.ajax(
        {
            url: "/Shared/Api/SendFeedback",
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
                    commonHelper.showError(UIErrorMessages.Feedback_FailedToSend, data.ErrorMessage);
                    options.onError();
                }
            }
        });
    };
}

var apiProxy = new ApiProxy();