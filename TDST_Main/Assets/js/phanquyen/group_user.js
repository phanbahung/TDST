
function ListUserByGroup(nhom)
{
    //alert("con cac");
    var myURL;
    if (location.hostname == "localhost")
        myURL = "/QlUser/ListUserByGroup/";
    else
        myURL = "/kk/QlUser/ListUserByGroup/";

    $('#inputNewUser').val("");

    $.ajax({
        url: myURL + nhom,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#IdGroup").text(nhom);
            $('#example1').dataTable().fnClearTable();                       

            var i;
            for (i = 0; i < result.length; i++) {
                $('#example1').dataTable().fnAddData(                
                  [result[i].UserName,
                  "<input type='button' id='" + result[i].UserName + "' class='btn btn-danger' value='Xóa user khỏi nhóm' onclick='RemoveUserFromGroup(this);'>"
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
function RemoveUserFromGroup(button) {
    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");
    //var id = row.children('td').eq(5).children('input').attr("id");
    var userName = row.children('td').eq(0).html();
    var group = $("span#IdGroup").text();

    //alert(id);
    if (confirm("Bạn đang chọn xóa 1 dòng?")) {
        var urlRemove;
        if (location.hostname == "localhost")
            urlRemove = "/QlUser/RemoveUserFromGroup";
        else
            urlRemove = "/nk/QlUser/RemoveUserFromGroup";

        //Send the JSON array to Controller using AJAX.
        $.ajax({
            type: "POST",
            url: urlRemove,
            data: JSON.stringify({ "userName": userName, "group": group }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                // alert(r + " record(s) deleted.");
                //row.remove();
                $('#example1').dataTable().fnDeleteRow(row[0]);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        }); // end ajax

    }// end if
};

// ---- End Remove

// --- Begin AddUserToGroup=================        
function AddUserToGroup() {
    var userName = $("input#inputNewUser").val();
    var group = $("span#IdGroup").text();
    //alert(id);

    var urlAdd;
    if (location.hostname == "localhost")
        urlAdd = "/QlUser/AddUserToGroup";
    else
        urlAdd = "/nk/QlUser/AddUserToGroup";

    //Send the JSON array to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: urlAdd,
        data: JSON.stringify({ "userName": userName, "group": group }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {

            // alert(" record(s) deleted.");
            if ($.isNumeric(r)) {
                $('#example1').dataTable().fnAddData(
                            [userName,
                              "<input type='button' id='" + userName + "' class='btn btn-danger' value='Xóa user khỏi nhóm' onclick='RemoveUserFromGroup(this);'>"]);
                $('#inputNewUser').val("");
            }
            else alert(r);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    }); // end ajax

};
// ---- End AddUserToGroup

