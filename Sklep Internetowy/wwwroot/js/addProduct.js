window.onload = InitializeFunctions;

function InitializeFunctions() {

    CategorySelection();

    $('.owl-carousel').owlCarousel({
        items: 2,
        margin: 10,
        center: true
    });

    let addBtn = document.getElementById("information-add");
    let deleteBtn = document.getElementById("information-remove");

    let inputContainer = document.getElementById("inputs-container");
    let imageInput = document.getElementById("Images");

    imageInput.addEventListener("change", x => DisplaySelectedImages());

    addBtn.addEventListener("click", e => {
        let child = CreateInput();
        inputContainer.appendChild(child);
    })

    deleteBtn.addEventListener("click", e => {
        if (inputContainer.children.length != 1) {
            inputContainer.lastChild.remove();
        }
    })
    
}

function DisplaySelectedImages() {

    let reader = new FileReader();
    let imagesContainer = $('.owl-carousel');
    let images = document.getElementById("Images").files;
    let itemsL = $('.owl-item').length;

    for (let i = 0; i < itemsL; i++) {
        imagesContainer.owlCarousel('remove', i).owlCarousel('update');
    }

    const PreviewImage = (file) => {
        if (/\.(jpe?g|png|gif)$/i.test(file.name)) {
            const reader = new FileReader();

            reader.addEventListener("load", x => {
                const image = new Image();
                image.height = 200;
                image.classList.add("selected-image");
                image.src = x.target.result;
                imagesContainer
                    .trigger("add.owl.carousel", [image])
                    .trigger('refresh.owl.carousel');
            }, false);

            reader.readAsDataURL(file);
        }
    }


    if (images) {
        Array.prototype.forEach.call(images, PreviewImage)
    }
}

function CategorySelection() {
    let CategoryInputs = document.getElementsByName("CategoryId");
    let SelectedCatNumber = document.getElementById("num-of-selected-cat");

    const CountSelection = () => {
        let count = 0;
        CategoryInputs.forEach(b => {
            if (b.checked)
                count++;
        })

        SelectedCatNumber.innerHTML = count;
    }

    CategoryInputs.forEach(b => {
        if (b.checked) {
            b.parentNode.classList.add("category-selected")
        } else {
            b.parentNode.classList.remove("category-selected")
        }
    })

    CategoryInputs.forEach(b => {
        b.addEventListener("change", x => {
            if (x.target.checked) {
                x.target.parentNode.classList.add("category-selected")
            } else {
                x.target.parentNode.classList.remove("category-selected")
            }
            CountSelection();
        })
    });

    CountSelection();
}

function CreateInput() {
    let element = document.createElement("input");
    element.setAttribute("name", INPUTNAME);
    element.setAttribute("type", "text");
    element.classList.add("aditional-information-input");
    element.classList.add("form-control");
    return element;
}