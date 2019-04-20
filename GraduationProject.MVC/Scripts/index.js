(function () {
    localStorage.clear();
    let $eltInterestSelector = $('[InterestSelector]');
    if ($eltInterestSelector) {
        const interests = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            prefetch: "/api/interests"
        });
        interests.initialize();

        $eltInterestSelector.tagsinput({
            itemValue: 'Id',
            itemText: 'name',

            typeaheadjs: {
                name: 'interestInput',
                displayKey: 'name',
                highlight: true,
                hint: false,
                source: interests.ttAdapter()
            }
        });
    }

    let $eltGovernorateSelector = $('[GovernorateSelector]');
    if ($eltGovernorateSelector) {
        const govtList = [
            "Alexandria",
            "Aswan",
            "Asyut",
            "Beheira",
            "Beni Suef",
            "Cairo",
            "Dakahlia",
            "Damietta",
            "Faiyum",
            "Gharbia",
            "Giza",
            "Ismailia",
            "Kafr El Sheikh",
            "Luxor",
            "Matruh",
            "Minya",
            "Monufia",
            "New Valley",
            "North Sinai",
            "Port Said Go",
            "Qalyubia",
            "Qena",
            "Red Sea",
            "Sharqia",
            "Sohag",
            "South Sinai",
            "Suez"
        ];
        const value = $eltGovernorateSelector.attr('value');
        for (const govt of govtList) {
            let selected = '';
            if (govt === value) {
                selected = "selected='selected'";
            }
            let newGovtOption = $(`<option ${selected}>${govt}</option>`);
            $eltGovernorateSelector.append(newGovtOption);
        }
    }


    window.imageFileCheck = function (obj) {
        var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
        if ($.inArray($(obj).val().split('.').pop().toLowerCase(), fileExtension) === -1) {
            $(obj).val(null);
            alert("Only '.jpeg','.jpg', '.png', '.gif', '.bmp' formats are allowed.");
        }
    };
})();