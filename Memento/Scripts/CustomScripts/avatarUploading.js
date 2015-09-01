AvatarUploading = (function ($) {

    var onSuccess = function (data) {
        $('#user-avatar').attr('src', data);
    }

    var onFailure = function (message) {
        alert("Failed avatar upload.");
    }

    return {
        onSuccess: onSuccess,
        onError: onFailure
    };
})(jQuery);