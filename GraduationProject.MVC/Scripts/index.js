
(async function () {
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

    let $eltCoursesSelector = $('[CoursesSelector]');
    if ($eltCoursestSelector) {
        const courses = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            prefetch: "/api/courses"
        });
        interests.initialize();

        $eltCoursesSelector.tagsinput({
            itemValue: 'Id',
            itemText: 'name',

            typeaheadjs: {
                name: 'coursesInput',
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
            $tansiqSelectionComponent.append($(await TansiqCreationComponentTemplate()));
        }
    }

    $('input[tansiqActivationBox]').change(event => {
        let checked = event.target.checked;
        let $target = $(event.target);
        let $containerWrapper = $target.parent().parent().find('tansiqInfoContainer');
        let type = $target.attr('colleger-tansiqType');
        let index = $target.attr('colleger-tansiqIndex');
        if (checked) {
            $containerWrapper.append($(TansiqCreationInfoTemplate(type, index)));
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

async function TansiqCreationComponentTemplate(forDivision = false) {
    let tansiqTemplate = `<div class="col w-100">`;
    try {
        let specialzationObjectsArray = await $.get('/api/specializations');
        for (const j in specialzationObjectsArray) {
            const i = specialzationObjectsArray[j];
            let specializationObjectTemplate = `<div>
                <div class="form-check">
                    <input class="form-check-input" tansiqActivationBox colleger-tansiqType="${i.Id}" colleger-tansiqIndex="${j}" type="checkbox" value="" id="tansiqCheck-${j}">
                    <label class="form-check-label" for="tansiqCheck-${j}">
                    Accepts ${i.Name}
                    </label>
                </div>
                <tansiqInfoContainer></tansiqInfoContainer>
            </div>`;
            tansiqTemplate += specializationObjectTemplate;
        }
    } catch (Err) {
        console.log(Err);
    }
    //let arrayOfSpecializations = ['3lmy 3loom', '3lmyryada', 'adaby'];
    tansiqTemplate += `</div>`;
    return tansiqTemplate;
}

function TansiqCreationInfoTemplate(forType, index, isEdit = false) {
    let tansiqInfoTemplate = `<div class="row">
        <input type="hidden" name="Tansiqs[${index}].SpecializationId" value="${forType}">
    <div class="col-auto">
        <label class="sr-only" for="Tansiqs[${index}].Startgrade">From</label>
        <input type="number" class="form-control mb-2" id="Tansiqs[${index}].Startgrade" placeholder="From" name="Tansiqs[${index}].Startgrade">
    </div>
    <div class="col-auto">
        <label class="sr-only" for="Tansiqs[${index}].Endgrade">To</label>
        <input type="number" class="form-control mb-2" id="Tansiqs[${index}].Endgrade" placeholder="To" name="Tansiqs[${index}].Endgrade">
    </div>
    ${isEdit ? `
    <div class="col-auto">
        <label class="sr-only" for="Tansiqs[${index}].Actual">Actual</label>
        <input type="number" class="form-control mb-2" id="Tansiqs[${index}].Actual" placeholder="Actual" name="Tansiqs[${index}].Actual">
    </div>
`: ''}
    </div>`;
    return tansiqInfoTemplate;
}

function createFaculty(event) {
    event.preventDefault();
    console.log(event);
}