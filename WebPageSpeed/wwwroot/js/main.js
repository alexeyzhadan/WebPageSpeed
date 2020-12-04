document.addEventListener('DOMContentLoaded', function () {
    let elements = document.getElementsByClassName('load');

    for (let i = 0; i < elements.length; i++) {
        addEventClickForShowLoader(elements[i])
    }
}, false);

function addEventClickForShowLoader(element) {
    element.addEventListener('click', showLoader);
}

function showLoader() {
    document.getElementsByClassName('loader')[0].classList.add('loader--on');
}