// JScript File
 
    
function fnUpdateTableModel(maModel, model, maLoaiTB, soHieuHD,ngayHD) {                                                     
              var $td = $('#example td input#'+maModel).closest('tr').children('td');                                    
              $td.eq(0).text(model);       
              $td.eq(1).text(maLoaiTB);       
              $td.eq(2).text(soHieuHD);   
              $td.eq(3).text(ngayHD);              
            } 
            
function fnAddNewRowModel(maModel, model, maLoaiTB, soHieuHD) {
$('#example').dataTable().fnAddData( 
[   model,
    maLoaiTB,                
    soHieuHD,
    null,
    "<input id='" + maModel + "' type='button' class='xemModel' value='Chi tiết'/>",
    "<input id='edit" + maModel + "' type='button' class='suaModel' value='Sửa' />"]);
}      
    
    
   