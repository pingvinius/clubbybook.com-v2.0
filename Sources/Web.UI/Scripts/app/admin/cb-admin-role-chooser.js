/// <reference path="cb-admin-ui-constants.js" />
/// <reference path="cb-admin.js" />
/// <reference path="cb-admin-api-proxy.js" />

function AdminRoleChooser()
{
    var instance = this;

    var selectedValueIdsControl = $(".edit-role-block input[type='hidden']");

    this.init = function ()
    {
        $(".edit-role-block input[type='checkbox']").bind("change", function ()
        {
            updateSelectedValueIds();
        });
    }

    /*
        Private methods
    */
    function updateSelectedValueIds()
    {
        var selectedValueIds = [];
        $(".edit-role-block input[type='checkbox']:checked").each(function ()
        {
            selectedValueIds.push($(this).val());
        });

        $(selectedValueIdsControl).val(selectedValueIds.joinToString());
    }
}

var adminRoleChooser = null;

$().ready(function ()
{
    if (adminRoleChooser === null)
    {
        adminRoleChooser = new AdminRoleChooser();
        adminRoleChooser.init();
    }
});