(function () {
    localStorage.clear();
    let $elt = $('[InterestSelector]');
    if ($elt) {
        const interests = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            prefetch: "./api/interests"
        });
        interests.initialize();

        $elt.tagsinput({
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
})();