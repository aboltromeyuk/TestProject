$(document).ready(function () {
    $('#tag').autocomplete({
        source: "/Home/Autocomplete"
    });
    
    $("#tabs").tabs({

        ajaxOptions: {
            error: function (xhr, status, index, anchor) {
                $(anchor.hash).html("Содержимое не найдено");
            }
        }
    });

    $(".btn").click(function () {
        var index = $(this).attr('data-index');
        Content.openWindowRight(index);
    });

    $('#fond').click(function () {
        Content.closeWindowRight();
    });

    
    $('#addBtn').click(function(){
        Content.addingTagToArt();
        Content.reloudeListTags();
        
    });
});

Content = {
    openWindowRight: function (id) {
        $.ajax({
            type: 'GET',
            url: '/Home/AddedTags',
            data: { idArt: id },
            async: true,
            success: function (html) {
                $('#listTags').html(html);
                $('#tags').show('slide', { direction: 'right' }, 500);
                $('#fond').show('fade', 500);
            },
            error: function () { }
        });
    },

    closeWindowRight: function () {

        $('#tags').hide('slide', { direction: 'right' }, 500);
        $('#fond').hide('fade', 500);
    },

    addingTagToArt: function () {
        var idArt = $('#Id').val(),
            nameTag = $('#tag').val();

        $.ajax({
            type: 'POST',
            url: '/Home/AddTagToArt',
            asynch: true,
            data: { idArt: idArt, nameTag: nameTag },
            success: function () { },
            error: function() { }
        });
    },

    reloudeListTags: function () {
        var idArt = $('#Id').val();

        $.ajax({
            type: 'GET',
            url: '/Home/AddedTags',
            data: { idArt: idArt },
            async: true,
            success: function (html) {
                $('#listTags').html(html);
                
            },
            error: function () { }
        });
}
}

