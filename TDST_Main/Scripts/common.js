   
function Empty_Text_Input()
    {
       $("input.control").val("");        
       //$(".control").text("");     
       $(".control").html("");  
    }


   
function Set_SelectedOption(select_id, option_val) {
        // Remove old selected
        $('select#'+select_id+' option:selected').removeAttr('selected');
        // Set new seleted 
        $('select#'+select_id+' option[value='+option_val+']').attr('selected','selected');       
 }