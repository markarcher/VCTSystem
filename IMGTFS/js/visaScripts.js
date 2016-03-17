$('input[type="checkbox"]').checkbox({
    checkedClass: 'icon-check',
    uncheckedClass: 'icon-check-empty'
});

function displayDataTableWithAgents() {

    var elem = document.getElementById('searchAgentDataTableContainer');
    elem.style.display = 'block';
}