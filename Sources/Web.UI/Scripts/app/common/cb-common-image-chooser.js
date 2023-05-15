function ImageChooser()
{
    var instance = this;

    var loadingBlockControl = $(".edit-image-block .loading");
    var errorBlockControl = $(".edit-image-block .error");
    var previewBlockControl = $(".edit-image-block .preview");
    var actionsBlockControl = $(".edit-image-block .actions");

    var previewImageControl = $(".edit-image-block .preview img")[0];

    var fileInputControl = $(".edit-image-block .actions #image-file-input");
    var fileSelectorControl = $(".edit-image-block .actions #image-file-select");

    var modalDialog = $(".edit-image-block .modal")[0];
    var btnApplyModal = $(".edit-image-block .modal .btn-primary")[0];

    var maxImageHeight = 450;
    var maxImageWidth = 450;
    var defaultAspectRatio = { w: 192, h: 300 };
    var maxFileSize = 1024 * 1024 * 3; // 3Mb

    var selectedImageBase64String = null;
    var selectedImageWidth = 0;
    var selectedImageHeight = 0;

    var jcropApi = null;

    this.init = function ()
    {
        // Set initial state
        $(loadingBlockControl).hide();
        $(errorBlockControl).hide();
        $(previewBlockControl).hide();
        $(actionsBlockControl).show();

        // Handle file selector
        $(fileSelectorControl).bind("click", function (e)
        {
            e.preventDefault();

            $(fileInputControl).click();
        });

        // Handle file
        $(fileInputControl).bind("change", function (e)
        {
            handleFile(this.files[0]);
        });

        // Override apply button of modal window
        $(btnApplyModal).bind("click", function (e)
        {
            e.preventDefault();

            uploadImage();

            dispose();

            $(modalDialog).modal("hide");
        });

        // On closing modal window dispose it
        $(modalDialog).on("hide.bs.modal", function ()
        {
            dispose();
        });
    }

    /*
        Private methods
    */
    function handleFile(file)
    {
        // Destroy previous cropping if there is one
        destroyCropping();

        // Reset selected image properties
        selectedImageBase64String = null;
        selectedImageWidth = 0;
        selectedImageHeight = 0;

        // Validate file size and type
        if (!validate(file))
        {
            return;
        }

        // Update controls
        onBeforeLoad();

        // Load image
        var reader = new FileReader();
        reader.onload = function (readerEventArgs)
        {
            // Get base64 image string
            var url = readerEventArgs.target.result;

            // Show image to user and enable cropping
            var image = new Image();
            image.onload = function ()
            {
                // Save image properties
                selectedImageBase64String = url;
                selectedImageWidth = this.width;
                selectedImageHeight = this.height;

                // Update preview image with new one
                updatePreviewImage(url, this.width, this.height);

                // Update controls
                onAfterLoad();
            };
            image.src = url;
        };
        reader.readAsDataURL(file);
    }

    function validate(file)
    {
        if (file == null)
        {
            return false;
        }

        if (!file.type.match("image.*"))
        {
            onError(UIErrorMessages.UploadImage_WrongMimeType);
            return false;
        }

        if (file.size >= maxFileSize)
        {
            onError(UIErrorMessages.UploadImage_MaxFileSize);
            return false;
        }

        return true;
    }

    function updatePreviewImage(url, width, height)
    {
        // Define scale size
        var koef = width / height;

        // Calculate width and height to be less than 450px to fit the modal page
        var currentImageWidth = 0;
        var currentImageHeight = 0;

        if (width >= height)
        {
            currentImageWidth = maxImageWidth;
            currentImageHeight = Math.round(maxImageWidth / koef);
        }
        else
        {
            currentImageWidth = Math.round(maxImageHeight * koef);
            currentImageHeight = maxImageHeight;
        }

        // Set image and its width and height
        $(previewImageControl).attr("width", currentImageWidth);
        $(previewImageControl).attr("height", currentImageHeight);
        $(previewImageControl).attr("src", url);

        // Initialize crop feature only for the first time
        $(previewImageControl).Jcrop(
        {
            aspectRatio: defaultAspectRatio.w / defaultAspectRatio.h,
            allowSelect: false,
            bgOpacity: 0.5,
            boxWidth: currentImageWidth,
            boxHeight: currentImageHeight,
            trueSize: [currentImageWidth, currentImageHeight],
            minSize: getMinSelectionSize(currentImageWidth, currentImageHeight),
            maxSize: getMaxSelectionSize(currentImageWidth, currentImageHeight),
            borderOpacity: 0.3,
            handleOpacity: 0.4
        }, function ()
        {
            jcropApi = this;
            jcropApi.setSelect(getDefaultSelection(currentImageWidth, currentImageHeight));
            jcropApi.focus();
        });
    }

    function destroyCropping()
    {
        if (jcropApi != null)
        {
            jcropApi.destroy();
            jcropApi = null;
        }
    }

    function getMinSelectionSize(currentImageWidth, currentImageHeight)
    {
        var minWidth = Math.round(defaultAspectRatio.w / 10);
        var minHeight = Math.round(defaultAspectRatio.h / 10);

        var koef = minWidth / minHeight;

        if (minWidth > currentImageWidth)
        {
            minWidth = currentImageWidth;
            minHeight = minWidth / koef;
        }

        if (minHeight > currentImageHeight)
        {
            minHeight = currentImageHeight;
            minWidth = minHeight * koef;
        }

        return [minWidth, minHeight];
    }

    function getMaxSelectionSize(currentImageWidth, currentImageHeight)
    {
        return [currentImageWidth, currentImageHeight];
    }

    function getDefaultSelection(imageWidth, imageHeight)
    {
        var s = { x1: 0, x2: 0, y1: 0, y2: 0, width: 0, height: 0 };

        var thumbnailScale = defaultAspectRatio.w / defaultAspectRatio.h;
        var imageScale = imageWidth / imageHeight;
        var smallOffset = 2; // ensures that selection will be in image

        if (imageScale > thumbnailScale)
        {
            s.height = imageHeight - smallOffset;
            s.width = Math.round((imageHeight - smallOffset) * thumbnailScale);
        }
        else
        {
            s.height = Math.round((imageWidth - smallOffset) / thumbnailScale);
            s.width = imageWidth - smallOffset;
        }

        s.x1 = Math.round(imageWidth / 2 - s.width / 2);
        s.y1 = Math.round(imageHeight / 2 - s.height / 2);
        s.x2 = Math.round(imageWidth / 2 + s.width / 2);
        s.y2 = Math.round(imageHeight / 2 + s.height / 2);

        return [s.x1, s.y1, s.x2, s.y2];
    }

    function onBeforeLoad()
    {
        $(loadingBlockControl).show();
        $(errorBlockControl).hide();
        $(previewBlockControl).hide();
        $(actionsBlockControl).hide();
    }

    function onAfterLoad()
    {
        $(loadingBlockControl).hide();
        $(errorBlockControl).hide();
        $(actionsBlockControl).show();
        $(previewBlockControl).show();
    }

    function onError(message)
    {
        // Dispose current state
        dispose();

        // Show error message
        $(errorBlockControl).empty();
        $(errorBlockControl).append("<p class='text-center'>" + message + "</p>");
        $(errorBlockControl).show();
    }

    function dispose()
    {
        destroyCropping();

        $(loadingBlockControl).hide();
        $(errorBlockControl).hide();
        $(previewBlockControl).hide();
        $(actionsBlockControl).show();

        $(previewImageControl).removeAttr("width");
        $(previewImageControl).removeAttr("height");
        $(previewImageControl).attr("src", "");

        $(fileInputControl).val("");
    }

    function uploadImage()
    {
        if (isDefinedAndNotNull(selectedImageBase64String) && isDefinedAndNotNull(jcropApi))
        {
            var selection = jcropApi.tellSelect();

            var sw = selectedImageWidth / $(previewImageControl).attr("width");
            var sh = selectedImageHeight / $(previewImageControl).attr("height");
            var applyDecorator = $("input#Image_ApplyDecorator").val().toLowerCase() === "true";

            var dataToSend = JSON.stringify(
            {
                imageData: selectedImageBase64String,
                x1: Math.round(selection.x * sw),
                y1: Math.round(selection.y * sh),
                x2: Math.round(selection.x2 * sw),
                y2: Math.round(selection.y2 * sh),
                applyDecorator: applyDecorator,
            });

            apiProxy.uploadImage(dataToSend,
            {
                onSuccess: function (data)
                {
                    // Update page with new image
                    updateEditPagePreview(data.filePath);

                    // Show message for user
                    commonHelper.showMessage(UIMessages.UploadImage_SuccessfullyUploadedPleaseSave);
                }
            });
        }
    }

    function updateEditPagePreview(filePath)
    {
        if (!isDefinedAndNotNull(filePath))
        {
            throwJsException("updateEditPagePreview", "The 'filePath' parameter is missing.");
        }

        // Apply new image
        $(".view-image-block img").attr("src", filePath);
        $("input#Image_NewTempImageUrl").val(filePath);
        $("input#Image_ImageUrl").val(filePath);
    }
}

var imageChooser = null;

$().ready(function ()
{
    if (imageChooser === null)
    {
        imageChooser = new ImageChooser();
        imageChooser.init();
    }
});