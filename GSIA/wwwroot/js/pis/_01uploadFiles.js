let files = [],
	dragArea = document.querySelector('.drag-area'),
	input = document.querySelector('.drag-area input'),
	button = document.querySelector('.card button'),
	select = document.querySelector('.drag-area .select'),
	container = document.querySelector('.container');
	previewContainer = document.querySelector('.image-preview');
filesNum = document.querySelector('.num-of-files');
btnsContainer = document.querySelector(".btns-container");

/** CLICK LISTENER */
select.addEventListener('click', () => input.click());

/* INPUT CHANGE EVENT */
input.addEventListener('change', () => {
	let file = input.files;
	console.log('file', file)

	// if user select no image
	if (file.length == 0) return;

	for (let i = 0; i < file.length; i++) {
		if (file[i].type.split("/")[0] != 'image') continue;
		if (!files.some(e => e.name == file[i].name)) files.push(file[i])
	}

	showImages();
});

/** SHOW IMAGES */
function showImages() {

	if (files.length > 0) {
		previewContainer.classList.remove('d-none');
		container.classList.add('image-preview-bdr')
		filesNum.innerHTML = `Number of files to upload: <span class="num-of-files">${files.length}</span>`;
		//btnsContainer.innerHTML = `
		//	<button onclick="removeAllImage()" class="btn b-btn-secondary w-md-full ">Cancel</button>
		//	<button onclick="uploadImage()" class="btn b-btn-primary">Upload</button>
		//`
	} else {
		filesNum.innerHTML = '';
		container.classList.remove('image-preview-bdr')
		btnsContainer.innerHTML = '';
	}


	container.innerHTML = files.reduce((prev, curr, index) => {
		return `${prev}
		    <div class="image-item-container border">
			    <div class="image-val">
					<img src="${URL.createObjectURL(curr)}" />
					<span class="image-desc">
						<div class=" image-name text-truncate b-body-lg-text">${curr.name}</div>
						<div class="b-body-md-text text-muted">${curr.size}</div>
					</span>
				</div>
			    
				<button class="image-delete-btn" onclick="delImage(${index})">&times;</button>
			</div>`
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
}

/* DRAG & DROP */
dragArea.addEventListener('dragover', e => {
	e.preventDefault()
	dragArea.classList.add('dragover')
})

/* DRAG LEAVE */
dragArea.addEventListener('dragleave', e => {
	e.preventDefault()
	dragArea.classList.remove('dragover')
});

/* DROP EVENT */
dragArea.addEventListener('drop', e => {
	e.preventDefault()
	dragArea.classList.remove('dragover');

	let file = e.dataTransfer.files;
	for (let i = 0; i < file.length; i++) {
		/** Check if selected file is image */
		if (file[i].type.split("/")[0] != 'image') continue;
		if (!files.some(e => e.name == file[i].name)) files.push(file[i])
	}
	showImages();
});
