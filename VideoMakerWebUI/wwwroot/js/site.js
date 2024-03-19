const myDropzone = new Dropzone('#videoDropZone', {
    url: '#',
    autoQueue: false,
    clickable: "#addFiles"
});


myDropzone.on("addedfile", function (file) {
    $("#startUpload").click(function () {
        myDropzone.enqueueFile(file)
    });
})

myDropzone.on("totaluploadprogress", function (progress) {
    document.querySelector("#total-progress .progress-bar").style.width = progress + "%"
})

myDropzone.on("sending", function (file) {
    document.querySelector("#total-progress").style.opacity = "1"
    //$("#startUpload").attr("disabled", "disabled")
})

myDropzone.on("queuecomplete", function (progress) {
    document.querySelector("#total-progress").style.opacity = "0"
})

//$('#startUpload').click(function () {
//    myDropzone.enqueueFiles(myDropzone.getFilesWithStatus(Dropzone.ADDED));
//});

$('#cancelUpload').click(function () {
    myDropzone.removeAllFiles(true);
});

var maxSizeAllowed = 1024 * 1024 * 1024;

$('#tranformVideos').click(function () {
    let filesDropzone = myDropzone.getAcceptedFiles();
    let formData = new FormData();
    var totalSize = 0;

    for (let i = 0; i < filesDropzone.length; i++) {
        formData.append('files', filesDropzone[i]);
        totalSize += filesDropzone[i].size;
        console.log(totalSize);
    }

    if (totalSize > maxSizeAllowed) {
        alert('The files sizes are larger than allowed!!');
        return;
    }

    //fazer enviar de 1 em 1

    $.ajax({
        url: '/Video/TransformVideos',
        type: "POST",
        data: formData,
        contentType: false,
        processData: false
    }).done(function (response) {
        if (response.files == 'ok') {
            alert('ok');
        } else {
            alert('not ok :/')
        }
    });
});