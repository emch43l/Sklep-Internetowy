window.onload = InitializeFunctions;

function InitializeFunctions() {
    InputRange();
    FilteringSortingForm();
}

function InputRange() {
    console.log("dasdadas");
    let input = document.getElementById("product-max-price");
    let output = document.getElementById("current-price");
    output.innerHTML = parseFloat(input.value).toFixed(2);
    input.addEventListener("input", e => {
        output.innerHTML = parseFloat(e.target.value).toFixed(2);
    })
}

function FilteringSortingForm() {

    let select = document.getElementById("sort-by-select");
    let order = document.getElementById("sort-order");

    let sortByInput = document.getElementById("sort-by");
    let orderInput = document.getElementById("sorting-order");

    let form = document.getElementById("filtering-form");
    let btn = document.getElementById("apply-sorting");

    let pageOutput = document.getElementById("page-number");
    let pageBtns = document.querySelectorAll(".change-page-button");

    pageBtns.forEach(b => {
        b.addEventListener("click", e => {
            pageOutput.value = e.target.value;
            form.submit();
        })
    })

    sortByInput.value = select.value;
    orderInput.value = order.value;

    btn.addEventListener("click", e => {
        sortByInput.value = select.value;
        orderInput.value = order.value;
        form.submit();
    })

}