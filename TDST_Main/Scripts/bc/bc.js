function Set_SelectedOption(select_id, option_val) {
    // Remove old selected
    $('select#' + select_id + ' option:selected').removeAttr('selected');
    // Set new seleted 
    $('select#' + select_id + ' option[value=' + option_val + ']').attr('selected', 'selected');
}

function GetbyId(idCTBC)
{
   
    $.ajax({
        url: "/BaoCao/GetbyId",        
        data:{"idCTBC":idCTBC},
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {           

            $('input#IdCTBC').val(result[0].IdCTBC);
            $('input#TenChiTieu').val(result[0].TenChiTieu);
            $('input#STT').val(result[0].STT);
            $('input#DisplayOnReport').val(result[0].DisplayOnReport);
            $('input#NhomCH').val(result[0].NhomCH);
            $('input#NhomTM').val(result[0].NhomTM);

            Set_SelectedOption('NhomChuong', result[0].NhomCH);
            Set_SelectedOption('NhomMTM', result[0].NhomTM);

            $('#myModal').modal('show');  
            $('#btnUpdate').show();
            $('#btnAdd').hide();

        },
        error: function (errormessage) {
           alert(errormessage.responseText);
          }

    });
    return false;
}