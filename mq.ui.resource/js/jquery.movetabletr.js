/**
Jquery扩展方法:用于表格行上移/下移.

参数注释:
    aCurrent:当前a标签;
    aUpIndex:用于操作上移的a标签在当前td的索引(从0开始);
    aDownIndex:用于操作下移的a标签在当前td的索引(从0开始);
    maxRowCount:显示最大行数(不包括表头);
    animateTime:动画时间;
    callback:回调方法;
@author:fanxiaojia
@version:20161124
*/

$.extend({
    MoveTableTrUp: function (aCurrent, aUpIndex, aDownIndex, maxRowCount, animateTime, callback) {
        if (!maxRowCount) {
            alert("maxRowCount参数不能为空!");
            return false;
        }
        if (rowCount > maxRowCount) {
            alert("实际行数不能大于maxRowCount的值");
            return false;
        }
        var time = arguments[4] ? arguments[4] : 800;
        var trCurrent = $(aCurrent).parents("tr");
        var trPrev = $(trCurrent).prev();
        var rowCount = $(trCurrent).parents("table").find("tr").length - 1;
        var rowIndex = $(trCurrent).index();

        if (rowIndex > 1) {
            if (rowIndex <= 2) {
                //本行处理 上移不可用
                $(aCurrent).removeAttr("onclick").attr({ "style": "color:gray;cursor:not-allowed" });
                //前一行处理 上移可用
                var aPrevUp = trPrev.children("td").last().children("a").eq(aUpIndex);
                aPrevUp.attr({ "onclick": "MoveUp(this)", "style": "cursor:hand" });
            }
            if (rowIndex >= rowCount || rowIndex >= maxRowCount) {
                //本行处理 下移可用
                $(aCurrent).next().attr({ "onclick": "MoveDown(this)", "style": "cursor:hand" });
                //前一行处理 下移不可用
                var aNextDown = trPrev.children("td").last().children("a").eq(aDownIndex);
                aNextDown.removeAttr("onclick").attr({ "style": "color:gray;cursor:not-allowed" });
            }
            $(trCurrent).fadeOut(time).fadeIn(time).prev().before($(trCurrent));//上移
            $(trCurrent).children("td").first().html($(trCurrent).index());
            trPrev.children("td").first().html(trPrev.index());
            if (callback) {
                callback();
            }
        }
        else {
            alert("首行不可上移!");
            return false;
        }
    },
    MoveTableTrDown: function (aCurrent, aUpIndex, aDownIndex, maxRowCount, animateTime, callback) {
        if (!maxRowCount) {
            alert("maxRowCount参数不能为空!");
            return false;
        }
        if (rowCount > maxRowCount) {
            alert("实际行数不能大于maxRowCount的值");
            return false;
        }
        var time = arguments[4] ? arguments[4] : 800;
        var trCurrent = $(aCurrent).parents("tr");
        var nextTr = $(trCurrent).next();
        var rowIndex = $(trCurrent).index();
        var rowCount = $(trCurrent).parents("table").find("tr").length - 1;

        if (rowIndex < maxRowCount && rowIndex < rowCount) {
            if (rowIndex <= 1) {
                //本行处理 上移可用
                $(aCurrent).prev().attr({ "onclick": "MoveUp(this)", "style": "cursor:hand" });
                //后一行处理(第二行) 上移不可用
                var aPreDown = nextTr.children("td").last().children("a").eq(aUpIndex);
                aPreDown.removeAttr("onclick").attr({ "style": "color:gray;cursor:not-allowed" });
            }
            if (rowIndex >= maxRowCount - 1 || rowIndex >= rowCount - 1) {
                //本行处理 下移不可用
                $(aCurrent).removeAttr("onclick").attr({ "style": "color:gray;cursor:not-allowed" });
                //后一行处理 下移可用 
                var aNextUp = nextTr.children("td").last().children("a").eq(aDownIndex);
                aNextUp.attr({ "onclick": "MoveDown(this)", "style": "cursor:hand" });
            }
            $(trCurrent).fadeOut(time).fadeIn(time).next().after($(trCurrent));//下移
            $(trCurrent).children("td").first().html($(trCurrent).index());
            nextTr.children("td").first().html(nextTr.index());
            if (callback) {
                callback();
            }
        }
        else {
            alert("末行不可下移!");
            return false;
        }
    }
});
