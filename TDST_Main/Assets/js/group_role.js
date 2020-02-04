
function ListRoleByGroup(Id)
{
    //alert("con cac");
    var myURL;
    if (location.hostname == "localhost")
        myURL = "/QLUser/ListRoleByGroup/";
    else
        myURL = "/kk/QLUser/ListRoleByGroup/";

    $('#inputNewRole').val("");

    $.ajax({
        url: myURL + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#IdGroup").text(Id);
            $('#example2').dataTable().fnClearTable();                       

            var i;
            for (i = 0; i < result.length; i++) {
                $('#example2').dataTable().fnAddData(                
                  [result[i].RoleName,
                  "<input type='button' id='" + result[i].RoleName + "' class='btn btn-danger' value='Xóa Role khỏi nhóm' onclick='RemoveRoleFromGroup(this);'>"
                  ]);
            }

          

        },
        error: function (errormessage) {
           alert(errormessage.responseText);
          }

    });
    return false;
}


// --- Begin Remove=================        
function RemoveRoleFromGroup(button) {
    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");
    //var id = row.children('td').eq(5).children('input').attr("id");
    var id = row.children('td').eq(0).html();
    //alert(id);
    if (confirm("Bạn đang chọn xóa 1 dòng?")) {
        var urlRemove;
        if (location.hostname == "localhost")
            urlRemove = "/QLUser/RemoveRoleFromGroup";
        else
            urlRemove = "/nk/QLUser/RemoveRoleFromGroup";

        //Send the JSON array to Controller using AJAX.
        $.ajax({
            type: "POST",
            url: urlRemove,
            data: JSON.stringify({ "id": id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                // alert(r + " record(s) deleted.");
                //row.remove();
                $('#example2').dataTable().fnDeleteRow(row[0]);
            }
        }); // end ajax

    }// end if
};

// ---- End Remove

// --- Begin AddRoleToGroup=================        
function AddRoleToGroup() {   
    var RoleName = $("input#inputNewRole").val();
    var group = $("span#IdGroup").text();
    //alert(id);
   
        var urlAdd;
        if (location.hostname == "localhost")
            urlAdd = "/QLUser/AddRoleToGroup";
        else
            urlAdd = "/nk/QLUser/AddRoleToGroup";

        //Send the JSON array to Controller using AJAX.
        $.ajax({
            type: "POST",
            url: urlAdd,
            data: JSON.stringify({ "RoleName": RoleName, "group": group }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {

                // alert(" record(s) deleted.");
                if (r == "0")
                    {
                        $('#example2').dataTable().fnAddData(
                                    [RoleName,
                                      "<input type='button' id='" + RoleName + "' class='btn btn-danger' value='Xóa Role khỏi nhóm' onclick='RemoveRoleFromGroup(this);'>"]);
                        $('#inputNewRole').val("");
                    }
                else   alert(r);
            }
        }); // end ajax

    
};

// ---- End AddRoleToGroup