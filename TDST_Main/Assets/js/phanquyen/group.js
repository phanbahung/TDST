// --- Begin NewGroup=================        
function NewGroup() {
    var groupName = $("input#inputGroupName").val();
    //alert(id);
    var urlAdd;
    if (location.hostname == "localhost")
        urlAdd = "/QlUser/NewGroup";
    else
        urlAdd = "/nk/QlUser/NewGroup";

    //Send the JSON array to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: urlAdd,
        data: JSON.stringify({ "GroupName": groupName }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            // alert(" record(s) deleted.");
            if ($.isNumeric(r)) {
                $('#tblGroup').dataTable().fnAddData(
                            [r,
                             groupName,
                                "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' data-target='#modalUser' onclick='return ListUserByGroup("+r+")'> Chi tiết </button>",
                                "<button type='button' class='btn btn-success btn-sm' data-toggle='modal' data-target='#modalRole' onclick='return ListRoleByGroup("+r+")'> Chi tiết </button>"
                            ]);
                $('#modalNewGroup').modal('hide');
               
            }
            else alert(r);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    }); // end ajax

};
// ---- End NewGroup