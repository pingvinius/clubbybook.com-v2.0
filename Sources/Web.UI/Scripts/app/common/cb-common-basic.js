//#region Basic Methods

function isDefined(variable)
{
    return typeof variable !== "undefined";
}

function isNull(variable)
{
    return variable === null;
}

function isStringEmpty(variable)
{
    return typeof variable === "string" && variable === "";
}

function isDefinedAndNotNull(variable)
{
    return isDefined(variable) && !isNull(variable);
}

function throwJsException(method, message)
{
    var fullMessage = "The exception has occurred in " + method + " method. Details: " + message;
    console.log(fullMessage);
    throw fullMessage;
}

//#endregion

//#region String Extensions

String.prototype.format = function ()
{
    var args = arguments;
    return this.replace(/{(\d+)}/g, function (match, number)
    {
        return isDefined(args[number]) ? args[number] : match;
    });
};

String.prototype.toArrayList = function (separator)
{
    if (!isDefinedAndNotNull(separator))
    {
        separator = Constants.defaultValuesSeparator;
    }

    var values = new Array();

    if (this.length > 0)
    {
        $.each(this.split(separator), function (index, value)
        {
            if (!isStringEmpty(value))
            {
                values.push(parseInt(value, 10));
            }
        });
    }

    return values;
};

//#endregion

//#region Array Extensions

Array.prototype.joinToString = function (separator)
{
    if (!isDefinedAndNotNull(separator))
    {
        separator = Constants.defaultValuesSeparator;
    }

    return this.join(separator);
};

Array.prototype.indexOf = function (searchValue, searchPredicate)
{
    if (!isDefinedAndNotNull(searchPredicate))
    {
        searchPredicate = function (val1, val2) { return val1 == val2; };
    }

    var foundIndex = -1;
    $.each(this, function (index, value)
    {
        if (searchPredicate(value, searchValue))
        {
            foundIndex = index;
            return;
        }
    });

    return foundIndex;
};

Array.prototype.removeAt = function (index)
{
    if (index < 0 && index >= this.length)
    {
        throwJsException("Array.removeAt", "The argument is out of range.");
    }

    return this.splice(index, 1);
};

Array.prototype.remove = function (value, searchPredicate)
{
    var index = this.indexOf(value, searchPredicate);
    if (index !== -1)
    {
        return this.removeAt(index);
    }

    return null;
};

//#endregion

//#region Page Related Methods

function getQueryParameters()
{
    var parameters = window.location.search.substr(1).split('&');
    if (isStringEmpty(parameters))
    {
        return {};
    }

    var variables = {};
    for (var index = 0; index < parameters.length; index++)
    {
        var pair = parameters[index].split('=');
        if (pair.length !== 2)
        {
            continue;
        }

        variables[pair[0].toLowerCase()] = decodeURIComponent(pair[1].replace(/\+/g, " "));
    }

    return variables;
};

function reloadPage()
{
    document.location.reload(false);
};

//#endregion

//#region List Factory Methods

function fillSelectList(selectControl, selectList, emptyText)
{
    if (!isDefined(selectControl))
    {
        throwJsException("fillSelectList", "The 'selectControl' should be defined.");
    }

    if (!isDefined(selectList))
    {
        throwJsException("fillSelectList", "The 'selectList' should be defined.");
    }

    if (!isDefined(emptyText))
    {
        emptyText = UIControlPreferenceTexts.Select_EmptyElement;
    }

    var options = [];
    var selectDefaultValue = true;

    $.each(selectList, function (index, value)
    {
        options.push(createSelectOption(value.Text, value.Value, value.Selected));
        if (value.Selected === true)
        {
            selectDefaultValue = false;
        }
    });

    var defaultOption = createSelectOption(emptyText, "", selectDefaultValue);
    options.splice(0, 0, defaultOption);

    $(selectControl).html(options.join(''));
}

function createSelectOption(text, value, isSelected)
{
    if (!isDefined(text))
    {
        throwJsException("createSelectOption", "The 'text' should be defined.");
    }

    if (!isDefined(value))
    {
        throwJsException("createSelectOption", "The 'value' should be defined.");
    }

    if (!isDefined(isSelected))
    {
        isSelected = false;
    }

    if (isSelected === true)
    {
        return "<option value='" + value + "' selected='selected'>" + text + "</option>";
    }

    return "<option value='" + value + "'>" + text + "</option>";
}

//#endregion