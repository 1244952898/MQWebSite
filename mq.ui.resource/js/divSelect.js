// JavaScript Document
jQuery.divselect = function (textword, droplist, changetext, callback) {
    textword.click(function (e) {
        if ($(this).siblings(droplist).css("display") == "none") {
            $(this).siblings(droplist).slideDown("fast");
        } else {
            $(this).siblings(droplist).slideUp("fast");
        }
        e.stopPropagation();
    });
    changetext.click(function () {
        var txt = $(this).text();
        $(this).parent().siblings(textword).html(txt);
        if (undefined!= callback) {
            callback();//选择下拉框时所作操作
        }
        droplist.hide();
    });
    $(document).click(function () {
        droplist.hide();
    });
};
//调用方式
/*<script>
$(function(){
    $.divselect(textword,droplist,changetext);
});
</script>*/
