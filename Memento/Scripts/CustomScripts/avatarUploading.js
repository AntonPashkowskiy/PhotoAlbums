AvatarUploading = (function ($) {

    var analyseData = function(data) {
        if (!data) {
            return;
        }
        $('#user-avatar').attr('src', data);
    };

    var onFailure = function() {
        alert("Failed avatar upload.");
    };

    return {
        analyseData: analyseData,
        onError: onFailure
    };
})(jQuery);