document.addEventListener('DOMContentLoaded', function () {
    let elements = document.getElementsByClassName('load');

    for (let i = 0; i < elements.length; i++) {
        elements[i].addEventListener('click', showLoader);
    }
}, false);

function showLoader() {
    document.getElementsByClassName('loader')[0].classList.add('loader--on');
}