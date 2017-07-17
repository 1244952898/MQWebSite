$(document).ready(function(){ 
  if($("#imgs").width() > 872){ 
    $("#imgs").attr("width", 872); 
  } 
  if($('.showt').find('img')){ 
    $('.showt').find('img').each(function(index, element){ 
      if($(this).width() > 872){ 
        $(this).attr("width", 872); 
      } 
    }); 
  } 
}); 