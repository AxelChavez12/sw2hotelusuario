$(document).on('ready',function(){
  $('#foto1').on('click',function(){
  var src= $('#foto1 img').attr('src');
  $('#foto img').attr('src',src)
  });

  $('#foto2').on('click',function(){
  var src= $('#foto2 img').attr('src');
  $('#foto img').attr('src',src)
  });

  $('#foto3').on('click',function(){
  var src= $('#foto3 img').attr('src');
  $('#foto img').attr('src',src)
  });
  $('#foto4').on('click',function(){
  var src= $('#foto4 img').attr('src');
  $('#foto img').attr('src',src)
  });



});


$(document).ready(function(){
    $("select").change(function(){
        $(this).find("option:selected").each(function(){
            var optionValue = $(this).attr("value");
            if(optionValue){
                $(".box").not("." + optionValue).hide();
                $("." + optionValue).show();
            } else{
                $(".box").hide();
            }
        });
    }).change();
});

$(document).ready(function(){
    $('input[type="radio"]').click(function(){
        var inputValue = $(this).attr("value");
        var targetBox = $("." + inputValue);
        $(".box").not(targetBox).hide();
        $(targetBox).show();
    });
});