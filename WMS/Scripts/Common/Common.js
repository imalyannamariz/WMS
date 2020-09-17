fnShowLoading: function () {
    //if ($("body").find("#modalLoading").length !== 0) {

    //} else {
    //    var modal = '<div id="modalLoading" class="uk-modal">' +
    //                    '<div class="uk-modal-dialog" style="height:80px;">' +
    //                        '<a href="" class="uk-modal-close uk-close"></a>' +
    //                        '<div class="uk-modal-spinner uk-icon-spin"></div>' +
    //                    '</div>' +
    //                '</div>';

    //    $("body").append($.parseHTML(modal));
    //}
    //var modalLoading = UIkit.modal("#modalLoading", { bgclose: false, modal: false });
    //modalLoading.show();
    if (Common.modalLoading == "" || Common.modalLoading == null) {
        Common.modalLoading = UIkit.modal.blockUI(
            '<label>' +
            '<span class="uk-icon-spinner uk-icon-spin"></span>' +
            ' Please Wait...' +
            '</label>'
        );

        Common.modalLoading
            .find('.uk-modal-dialog')
            .prepend('<a class="uk-modal-close uk-close"></a>');
    }
},
fnHideLoading: function () {
    //setTimeout(function () {
    //    var x = $("body").find("div#modalLoading");
    //    var modalLoading = UIkit.modal(x, { bgclose: false, modal: false });
    //    modalLoading.hide();
    //}, 300);
    //2018 06 27
    //if (Common.modalLoading != "") { 
    //    setTimeout(function () {
    //        Common.modalLoading.hide();
    //        Common.modalLoading = "";
    //    }, 300);
    //}

    if (Common.modalLoading == null) { } else {
        if (Common.modalLoading != "") {
            setTimeout(function () {

                //Common.modalLoading.find('.uk-close').click();
                try {
                    Common.modalLoading.hide();
                } catch (err) {

                }

                Common.modalLoading = "";
            }, 300);
        }
    }
},