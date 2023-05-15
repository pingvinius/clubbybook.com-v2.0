function CityChooser()
{
    var instance = this;

    var countriesSelectControl = $(".edit-city-block #countries select");
    var countriesSelectedValueControl = $(".edit-city-block #countries input");

    var districtsSelectControl = $(".edit-city-block #districts select");
    var districtsSelectedValueControl = $(".edit-city-block #districts input");

    var citiesSelectControl = $(".edit-city-block #cities select");
    var citiesSelectedValueControl = $(".edit-city-block #cities input");

    this.init = function ()
    {
        loadCountries();
    }

    /*
        Private methods
    */
    function loadCountries()
    {
        $(countriesSelectControl).unbind("change");

        var selectedCountryId = $(countriesSelectedValueControl).val();

        $(countriesSelectControl).empty();
        apiProxy.getCountries(selectedCountryId,
        {
            onSuccess: function (data)
            {
                if (data.length > 0)
                {
                    $(countriesSelectControl).removeClass("hidden");
                    fillSelectList(countriesSelectControl, data, UIControlPreferenceTexts.CityChooser_SelectCountryPrompt);

                    $(countriesSelectControl).bind("change", function ()
                    {
                        $(countriesSelectedValueControl).val($(countriesSelectControl).val());
                        $(districtsSelectedValueControl).val("");
                        $(citiesSelectedValueControl).val("");
                        loadDistricts();
                    });

                    loadDistricts();
                }
                else
                {
                    $(countriesSelectControl).addClass("hidden");
                    $(districtsSelectControl).addClass("hidden");
                    $(citiesSelectControl).addClass("hidden");
                }
            }
        });
    }

    function loadDistricts()
    {
        $(districtsSelectControl).unbind("change");

        var selectedCountryId = $(countriesSelectedValueControl).val();
        var selectedDistrictId = $(districtsSelectedValueControl).val();

        $(districtsSelectControl).empty();

        apiProxy.getDistricts(selectedCountryId, selectedDistrictId,
        {
            onSuccess: function (data)
            {
                if (data.length > 0)
                {
                    $(districtsSelectControl).removeClass("hidden");
                    fillSelectList(districtsSelectControl, data, UIControlPreferenceTexts.CityChooser_SelectDistrictPrompt);

                    $(districtsSelectControl).bind("change", function ()
                    {
                        $(districtsSelectedValueControl).val($(districtsSelectControl).val());
                        $(citiesSelectedValueControl).val("");
                        loadCities();
                    });

                    loadCities();
                }
                else
                {
                    $(districtsSelectControl).addClass("hidden");
                    $(citiesSelectControl).addClass("hidden");
                }
            }
        });
    }

    function loadCities()
    {
        $(citiesSelectControl).unbind("change");

        var selectedCountryId = $(countriesSelectedValueControl).val();
        var selectedDistrictId = $(districtsSelectedValueControl).val();
        var selectedCityId = $(citiesSelectedValueControl).val();

        $(citiesSelectControl).empty();

        apiProxy.getCities(selectedCountryId, selectedDistrictId, selectedCityId,
        {
            onSuccess: function (data)
            {
                if (data.length > 0)
                {
                    $(citiesSelectControl).removeClass("hidden");
                    fillSelectList(citiesSelectControl, data, UIControlPreferenceTexts.CityChooser_SelectCityPrompt);

                    $(citiesSelectControl).bind("change", function ()
                    {
                        $(citiesSelectedValueControl).val($(citiesSelectControl).val());
                    });
                }
                else
                {
                    $(citiesSelectControl).addClass("hidden");
                }
            }
        });
    }
}

var cityChooser = null;

$().ready(function ()
{
    if (cityChooser === null)
    {
        cityChooser = new CityChooser();
        cityChooser.init();
    }
});