/*placeholder支持多个密码版本，密码的样式需将login-input换成自己的输入框的样式，保证统一
在做登录的时候我们都会用到文本框中显示要输入的默认提示，以前都是用js判断的比较麻烦，还有一个就是password是通过两个input框切换实现的，还好html5提供了一个属性placeholder，在input类型的框上可以起到占位符的效果，但现在还不是所有的浏览器都支持很html5，下面就通过jquery、html5来实现可以兼容多种浏览器的placeholder效果。
大致思路：
	1.判断浏览器是否支持html5的placeholder，支持就直接使用该属性。
	2.不支持就通过jquery来添加blur focus事件
	3.对password框的特使处理
*/
function isPlaceholder(){  
    var input = document.createElement('input');  
    return 'placeholder' in input;  
}  

if (!isPlaceholder()) {//不支持placeholder 用jquery来完成  
    $(document).ready(function() {  
        if(!isPlaceholder()){  
            $("input").not("input[type='password']").each(//把input绑定事件 排除password框  
                function(){  
                    if($(this).val()=="" && $(this).attr("placeholder")!=""){  
                        $(this).val($(this).attr("placeholder"));  
                        $(this).focus(function(){  
                            if($(this).val()==$(this).attr("placeholder")) $(this).val("");  
                        });  
                        $(this).blur(function(){  
                            if($(this).val()=="") $(this).val($(this).attr("placeholder"));  
                        });  
                    }  
            });  
            //对password框的特殊处理1.创建一个text框 2获取焦点和失去焦点的时候切换 
            $("input[type='password']").each(
            	function() {
            		var pwdField    = $(this);  
            		var pwdVal      = pwdField.attr('placeholder');  
            		pwdField.after('<input  class="login-input" type="text" value='+pwdVal+' autocomplete="off" />');  
            		var pwdPlaceholder = $(this).siblings('.login-input');  
            		pwdPlaceholder.show();  
            		pwdField.hide();  
            		  
            		pwdPlaceholder.focus(function(){  
            		    pwdPlaceholder.hide();  
            		    pwdField.show();  
            		    pwdField.focus();  
            		});   
            		  
            		pwdField.blur(function(){  
            		    if(pwdField.val() == '') {  
            		        pwdPlaceholder.show();  
            		        pwdField.hide();  
            		    }  
            		}); 
            	}
            )
              
        }  
    });  
      
}  