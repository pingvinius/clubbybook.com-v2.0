/// <reference path="cb-admin-ui-constants.js" />
/// <reference path="cb-admin.js" />
/// <reference path="cb-admin-api-proxy.js" />

function AdminAuthorListChooser()
{
    var instance = this;

    var selectedValueIdsControl = $(".edit-author-list-block input[type='hidden']");
    var autoComplateTextBox = $(".edit-author-list-block input[type='text']");

    this.init = function ()
    {
        // Fetch initial values from html
        var initialValues = new Array();
        $.each($(".edit-author-list-block select#initialValues option"), function (index, optionControl)
        {
            var authorId = parseInt($(optionControl).val(), 10);
            var authorFullName = $(optionControl).text();
            initialValues.push({ id: authorId, name: authorFullName });
        });

        // Create auto complete text box for book authors
        $(autoComplateTextBox).tokenInput("/Admin/Author/GetAutoCompleteList",
        {
            method: "POST",
            minChars: 1,
            preventDuplicates: true,
            queryParam: "searchKey",
            hintText: UIControlPreferenceTexts.AutoComplete_HintText,
            noResultsText: UIControlPreferenceTexts.AutoComplete_NoResultsText,
            searchingText: UIControlPreferenceTexts.AutoComplete_SearchingText,
            prePopulate: initialValues,
            onAdd: function (item)
            {
                updateSelectedValueIds(item.id, null);
            },
            onDelete: function (item)
            {
                updateSelectedValueIds(null, item.id);
            }
        });
    }

    /*
        Private methods
    */
    function updateSelectedValueIds(addedId, removedId)
    {
        var selectedValueIds = $(selectedValueIdsControl).val().toArrayList();

        if (isDefinedAndNotNull(addedId))
        {
            selectedValueIds.push(addedId);
        }

        if (isDefinedAndNotNull(removedId))
        {
            var idIndex = selectedValueIds.indexOf(removedId);
            if (idIndex >= 0)
            {
                selectedValueIds.splice(idIndex, 1);
            }
        }

        $(selectedValueIdsControl).val(selectedValueIds.joinToString());
    }
}

var adminAuthorListChooser = null;

$().ready(function ()
{
    if (adminAuthorListChooser === null)
    {
        adminAuthorListChooser = new AdminAuthorListChooser();
        adminAuthorListChooser.init();
    }
});