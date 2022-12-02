window.onload = InitializeFunctions;

function InitializeFunctions() {

    let starBtns = document.getElementsByClassName("input-star");

    let input = document.getElementById("product-rating");

    [...starBtns].forEach(e => {
        e.addEventListener("click", b => {
            b.preventDefault();
            if (b.target.dataset.value === undefined) {
                input.value = b.target.parentElement.dataset.value;
            } else {
                input.value = b.target.dataset.value;
            }

            StylizeButtons();
        })
    });

    const StylizeButtons = () => {
        let number = parseInt(input.value);

        [...starBtns].forEach(e => {
            e.firstChild.classList.replace("fas", "far");
        })

        for (let i = 0; i < number; i++) {
            let element = starBtns[i];
            if (element !== undefined) {
                element.firstChild.classList.replace("far", "fas");
            }
        }
    } 


    $('.owl-carousel').owlCarousel({
        items: 1,
        margin: 10,
        center: true

    });
}


