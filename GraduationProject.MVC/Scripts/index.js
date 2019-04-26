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

        const $tansiqSelectionComponent = $('tansiq');
        if ($tansiqSelectionComponent) {
            if ($tansiqSelectionComponent.attr('for') === 'division') {
                console.log('its for division');
            }
            console.log('tansiq exists');
            $tansiqSelectionComponent.append($(TansiqCreationComponentTemplate()));
        }
    }

    $('input[tansiqActivationBox]').change(event => {
        let checked = event.target.checked;
        let $target = $(event.target);
        let $containerWrapper = $target.parent().parent().find('tansiqInfoContainer');
        let type = $target.attr('colleger-tansiqType');
        if (checked) {
            $containerWrapper.append($(TansiqCreationInfoTemplate(type)));
        } else {
            $containerWrapper.empty();
        }
    });


    window.imageFileCheck = function (obj) {
        var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
        if ($.inArray($(obj).val().split('.').pop().toLowerCase(), fileExtension) === -1) {
            $(obj).val(null);
            alert("Only '.jpeg','.jpg', '.png', '.gif', '.bmp' formats are allowed.");
        }
    };
})();

function TansiqCreationComponentTemplate(forDivision = false) {
    let tansiqTemplate = `<div class="col w-100">`;
    let arrayOfSpecializations = ['3lmy 3loom', '3lmyryada', 'adaby'];
    for (const i of arrayOfSpecializations) {
        let specializationObjectTemplate = `<div>
            <div class="form-check">
              <input class="form-check-input" tansiqActivationBox colleger-tansiqType="${i}" type="checkbox" value="" id="tansiqCheck-${i}">
              <label class="form-check-label" for="tansiqCheck-${i}">
                Accepts ${i}
              </label>
            </div>
            <tansiqInfoContainer></tansiqInfoContainer>
        </div>`;
        tansiqTemplate += specializationObjectTemplate;
    }
    tansiqTemplate += `</div>`;
    return tansiqTemplate;
}

function TansiqCreationInfoTemplate(forType, isEdit = false) {
    let tansiqInfoTemplate = `<div class="row">
    <div class="col-auto">
        <label class="sr-only" for="tansiq['${forType}'].from">From</label>
        <input type="number" class="form-control mb-2" id="tansiq['${forType}'].from" placeholder="From" name="tansiq['${forType}'].from">
    </div>
    <div class="col-auto">
        <label class="sr-only" for="tansiq['${forType}'].to">To</label>
        <input type="number" class="form-control mb-2" id="tansiq['${forType}'].to" placeholder="To" name="tansiq['${forType}'].to">
    </div>
    ${isEdit ? `
    <div class="col-auto">
        <label class="sr-only" for="tansiq['${forType}'].actual">Actual</label>
        <input type="number" class="form-control mb-2" id="tansiq['${forType}'].actual" placeholder="Actual" name="tansiq['${forType}'].actual">
    </div>
`: ''}
    </div>`;
    return tansiqInfoTemplate;
}