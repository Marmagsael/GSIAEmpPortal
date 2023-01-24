
let files = [];
let mainContainer = document.querySelector("#pills-nbi .upload-container");
let input = document.querySelector("#pills-nbi .upload-container input");
let imagesContainer = document.querySelector("#pills-nbi .upload-container #image-display");
let tabs = document.getElementsByTagName("li");
let removeAllImages = document.querySelector(".remove-all-images");

function activeTab(e) {
	files = [];
	mainContainer = "";
	input = "";
	imagesContainer = "";

	/*Get data-bs-target of clicked element*/
	let elVal = e.target.dataset.bsTarget; 
	let val1 = elVal + " .upload-container";
	let val2 = elVal + " .upload-container input";
	let val3 = elVal + " .upload-container #image-display"
	mainContainer = document.querySelector(val1);
	input = document.querySelector(val2); 
	imagesContainer = document.querySelector(val3); 

	console.log("@click", val1, input, imagesContainer)
}


for (let tab of tabs) {
	tab.addEventListener("click", activeTab);
}


/* INPUT CHANGE EVENT */
input.addEventListener('change', () => {
	let file = input.files;

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
		mainContainer.classList.add("has-content");

	} else {
		mainContainer.classList.remove("has-content");
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
mainContainer.addEventListener('dragover', e => {
	e.preventDefault();
	event.stopPropagation();
	mainContainer.classList.add('active')
})

/* DRAG LEAVE */
mainContainer.addEventListener('dragleave', e => {
	e.preventDefault();
	event.stopPropagation();
	mainContainer.classList.remove('active');
	
});

/* DROP EVENT */
mainContainer.addEventListener('drop', e => {
	e.preventDefault();
	event.stopPropagation();
	mainContainer.classList.remove('active');

	let file = e.dataTransfer.files;
	for (let i = 0; i < file.length; i++) {
		/** Check if selected file is image */
		if (file[i].type.split("/")[0] != 'image') continue;
		if (!files.some(e => e.name == file[i].name)) files.push(file[i])
	}
	showImages();
});
