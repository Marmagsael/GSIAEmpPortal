
let files = [],
	uploadContainer = document.querySelector(".upload-container"),
	input = document.querySelector(".upload-container input"),
	imagesContainer = document.querySelector(".upload-container #image-display"),
	removeAllImages = document.querySelector(".remove-all-images"),

	expireLabel = document.querySelector(".form-date-expire label"),
	expireInput = document.querySelector(".form-date-expire input"),
	tabs = document.getElementsByTagName("li");


function activeTab(e) {
	/*Get data-bs-target of clicked element*/
	let tabVal = e.target.id;
	tab = document.getElementById(tabVal);

	let navLink = document.getElementsByClassName("nav-link");

	if (navLink.classList.contains(active)) navLink.classList.remove("active");
	navLink.classList.remove("active");
	if (input.files.length > 0) {
		let cancelUpload = confirm("Are you sure you want to cancel your upload?");
		if (cancelUpload) {
			switch (tabVal) {
				case "pills-nbi-tab":
					tab.classList.add("active");
					expireLabel.innerHTML = "NBI Expire"
					expireLabel.setAttribute('for', 'Exp_police');
					expireLabel.setAttribute('asp-for', 'Exp_police');
					expireInput.setAttribute('asp-for', 'Exp_police');
					expireInput.setAttribute('id', 'Exp_police');
					expireInput.setAttribute('name', 'Exp_police');
					break;
				case "pills-police-tab":
					tab.classList.add("active");
					expireLabel.innerHTML = "Police Expire"
					expireLabel.setAttribute('for', 'Exp_police');
					expireLabel.setAttribute('asp-for', 'Exp_police');
					expireInput.setAttribute('asp-for', 'Exp_police');
					expireInput.setAttribute('id', 'Exp_police');
					expireInput.setAttribute('name', 'Exp_police');
					break;

				case "pills-pnp-tab":
					tab.classList.add("active");
					expireLabel.innerHTML = "PNP Expire"
					expireLabel.setAttribute('for', 'Exp_pnp');
					expireLabel.setAttribute('asp-for', 'Exp_pnp');
					expireInput.setAttribute('asp-for', 'Exp_pnp');
					expireInput.setAttribute('id', 'Exp_pnp');
					expireInput.setAttribute('name', 'Exp_pnp');
					break;

				case "pills-brgy-tab":
					tab.classList.add("active");
					expireLabel.innerHTML = "Barangay Expire"
					expireLabel.setAttribute('for', 'Exp_brgy');
					expireLabel.setAttribute('asp-for', 'Exp_brgy');
					expireInput.setAttribute('asp-for', 'Exp_brgy');
					expireInput.setAttribute('id', 'Exp_brgy');
					expireInput.setAttribute('name', 'Exp_brgy');
					break;

				case "pills-neuro-tab":
					tab.classList.add("active");
					expireLabel.innerHTML = "Neuro Expire"
					expireLabel.setAttribute('for', 'Exp_neuro');
					expireLabel.setAttribute('asp-for', 'Exp_neuro');
					expireInput.setAttribute('asp-for', 'Exp_neuro');
					expireInput.setAttribute('id', 'Exp_neuro');
					expireInput.setAttribute('name', 'Exp_neuro');
					break;

				case "pills-drug-tab":
					expireLabel.innerHTML = "Drug Expire"
					expireLabel.setAttribute('for', 'Exp_drug');
					expireLabel.setAttribute('asp-for', 'Exp_drug');
					expireInput.setAttribute('asp-for', 'Exp_drug');
					expireInput.setAttribute('id', 'Exp_drug');
					expireInput.setAttribute('name', 'Exp_drug');
					break;

				case "pills-court-tab":
					tab.classList.add("active");
					expireLabel.innerHTML = "Court Expire"
					expireLabel.setAttribute('for', 'Exp_court');
					expireLabel.setAttribute('asp-for', 'Exp_court');
					expireInput.setAttribute('asp-for', 'Exp_court');
					expireInput.setAttribute('id', 'Exp_court');
					expireInput.setAttribute('name', 'Exp_court');
					break;

				case "pills-med-tab":
					tab.classList.add("active");
					expireLabel.innerHTML = "Medical Expire"
					expireLabel.setAttribute('for', 'Exp_med');
					expireLabel.setAttribute('asp-for', 'Exp_med');
					expireInput.setAttribute('asp-for', 'Exp_med');
					expireInput.setAttribute('id', 'Exp_med');
					expireInput.setAttribute('name', 'Exp_med');
					break;

				default:
				// code block\
			}
		} else {
			tab.classList.remove("active");
			return false;

		}
	} else {
		console.log(input.files.length)
		switch (tabVal) {
			case "pills-nbi-tab":
				tab.classList.add("active");
				expireLabel.innerHTML = "NBI Expire"
				expireLabel.setAttribute('for', 'Exp_police');
				expireLabel.setAttribute('asp-for', 'Exp_police');
				expireInput.setAttribute('asp-for', 'Exp_police');
				expireInput.setAttribute('id', 'Exp_police');
				expireInput.setAttribute('name', 'Exp_police');
				break;
			case "pills-police-tab":
				tab.classList.add("active");
				expireLabel.innerHTML = "Police Expire"
				expireLabel.setAttribute('for', 'Exp_police');
				expireLabel.setAttribute('asp-for', 'Exp_police');
				expireInput.setAttribute('asp-for', 'Exp_police');
				expireInput.setAttribute('id', 'Exp_police');
				expireInput.setAttribute('name', 'Exp_police');
				break;

			case "pills-pnp-tab":
				tab.classList.add("active");
				expireLabel.innerHTML = "PNP Expire"
				expireLabel.setAttribute('for', 'Exp_pnp');
				expireLabel.setAttribute('asp-for', 'Exp_pnp');
				expireInput.setAttribute('asp-for', 'Exp_pnp');
				expireInput.setAttribute('id', 'Exp_pnp');
				expireInput.setAttribute('name', 'Exp_pnp');
				break;

			case "pills-brgy-tab":
				tab.classList.add("active");
				expireLabel.innerHTML = "Barangay Expire"
				expireLabel.setAttribute('for', 'Exp_brgy');
				expireLabel.setAttribute('asp-for', 'Exp_brgy');
				expireInput.setAttribute('asp-for', 'Exp_brgy');
				expireInput.setAttribute('id', 'Exp_brgy');
				expireInput.setAttribute('name', 'Exp_brgy');
				break;

			case "pills-neuro-tab":
				tab.classList.add("active");
				expireLabel.innerHTML = "Neuro Expire"
				expireLabel.setAttribute('for', 'Exp_neuro');
				expireLabel.setAttribute('asp-for', 'Exp_neuro');
				expireInput.setAttribute('asp-for', 'Exp_neuro');
				expireInput.setAttribute('id', 'Exp_neuro');
				expireInput.setAttribute('name', 'Exp_neuro');
				break;

			case "pills-drug-tab":
				tab.classList.add("active");
				expireLabel.innerHTML = "Drug Expire"
				expireLabel.setAttribute('for', 'Exp_drug');
				expireLabel.setAttribute('asp-for', 'Exp_drug');
				expireInput.setAttribute('asp-for', 'Exp_drug');
				expireInput.setAttribute('id', 'Exp_drug');
				expireInput.setAttribute('name', 'Exp_drug');
				break;

			case "pills-court-tab":
				tab.classList.add("active");
				expireLabel.innerHTML = "Court Expire"
				expireLabel.setAttribute('for', 'Exp_court');
				expireLabel.setAttribute('asp-for', 'Exp_court');
				expireInput.setAttribute('asp-for', 'Exp_court');
				expireInput.setAttribute('id', 'Exp_court');
				expireInput.setAttribute('name', 'Exp_court');
				break;

			case "pills-med-tab":
				tab.classList.add("active");
				expireLabel.innerHTML = "Medical Expire"
				expireLabel.setAttribute('for', 'Exp_med');
				expireLabel.setAttribute('asp-for', 'Exp_med');
				expireInput.setAttribute('asp-for', 'Exp_med');
				expireInput.setAttribute('id', 'Exp_med');
				expireInput.setAttribute('name', 'Exp_med');
				break;

			default:
			// code block\
		}
	}
}

for (let tab of tabs) {
	tab.addEventListener("click", activeTab);
}


/* INPUT CHANGE EVENT */
input.addEventListener('change', () => {
	let file = input.files;
	console.log(input.files.length)

	for (let i = 0; i < file.length; i++) {
		// If image already exists, don't include the image.
		if (file[i].type.split("/")[0] != 'image') continue;
		if (!files.some(e => e.name == file[i].name)) files.push(file[i])
	}

	showImages();
});

///** DISPLAY CHOSEN IMAGES */
function showImages() {

	if (files.length > 0) {
		uploadContainer.classList.add("has-content");

	} else {
		uploadContainer.classList.remove("has-content");
	}
	imagesContainer.innerHTML = files.reduce((prev, curr, index) => {
		return `${prev}
		    <figure >
				<img src="${URL.createObjectURL(curr)}" />
				<figcaption class="d-block text-truncate">${curr.name}</figcaption>
				<button class="image-delete-btn" onclick="delImage(${index})">&times;</span>
			</figure>`
	}, '');
}

function uploadImage() {

}

/* DELETE IMAGE */
function delImage(index) {
	files.splice(index, 1);
	showImages();
}
/* REMOVE ALL IMAGES */
function removeAllImage() {
	files = [];
	showImages();
};

removeAllImages.addEventListener('click', removeAllImage);


/* DRAG & DROP */
uploadContainer.addEventListener('dragover', e => {
	e.preventDefault();
	event.stopPropagation();
	uploadContainer.classList.add('active')
})

/* DRAG LEAVE */
uploadContainer.addEventListener('dragleave', e => {
	e.preventDefault();
	event.stopPropagation();
	uploadContainer.classList.remove('active');
});

/* DROP EVENT */
uploadContainer.addEventListener('drop', e => {
	e.preventDefault();
	event.stopPropagation();
	uploadContainer.classList.remove('active');

	let file = e.dataTransfer.files;
	for (let i = 0; i < file.length; i++) {
		/** Check if selected file is image */
		if (file[i].type.split("/")[0] != 'image') continue;
		if (!files.some(e => e.name == file[i].name)) files.push(file[i])
	}
	showImages();
});
