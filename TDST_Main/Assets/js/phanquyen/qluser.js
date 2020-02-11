function Set_SelectedOption(select_id, option_val) {
    // Remove old selected
    $('select#' + select_id + ' option:selected').removeAttr('selected');
    // Set new seleted 
    $('select#' + select_id + ' option[value=' + option_val + ']').attr('selected', 'selected');
}

function fnUpdateTableModel(idUser, fullName, status ) {
    var $td = $('#example tr[id=' + idUser + ']').children('td');   
    $td.eq(2).text(fullName);
    $td.eq(3).text(status);
}

function DetailUser(idUser)
{
    //alert("con cac");
    var myURL;
    if (location.hostname == "localhost")
        myURL = "/QLUser/DetailUser/";
    else
        myURL = "/kk/QLUser/DetailUser/";
    
    
    $.ajax({
        url: myURL + idUser,
        //data: JSON.stringify({ "idUser": idUser }),
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {           
            $('#inputIdUser').val(result[0].IdUser);
            $('#inputUserName').val(result[0].UserName);
            $('#inputFullName').val(result[0].FullName);
            Set_SelectedOption('selStatus', result[0].Status);           
        },
        error: function (errormessage) {
           alert(errormessage.responseText);
          }
    });
    return false;
}


function UpdateUser() {   
    var myURL;
    if (location.hostname == "localhost")
        myURL = "/QLUser/UpdateUser";
    else
        myURL = "/kk/QLUser/UpdateUser";
   
    var customer = {};   
    customer.IdUser = $("#inputIdUser").val();
    customer.UserName = $("#inputUserName").val();    
    customer.FullName = $("#inputFullName").val();    
    customer.Status = $("select#selStatus").val();   
    //alert("con cac");
    $.ajax({
        url: myURL,
        data: JSON.stringify(customer),        
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            //alert(result);           
            $('#modalUser').modal('hide');
            fnUpdateTableModel(customer.IdUser, customer.FullName, customer.Status);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    }); 
    return false;
}

function ListGroupByUser(userName) {
    //alert("con cac");
    var myURL;
    if (location.hostname == "localhost")
        myURL = "/QlUser/ListGroupByUser/";
    else
        myURL = "/kk/QlUser/ListGroupByUser/";

    $('#inputNewGroup').val("");

    $.ajax({
        url: myURL + userName,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#hiddenUserName").text(userName);
            $('#example1').dataTable().fnClearTable();

            var i;
            for (i = 0; i < result.length; i++) {
                $('#example1').dataTable().fnAddData(
                  [result[i].IdGroup,
                   result[i].GroupName,
                  "<input type='button' id='" + result[i].IdGroup + "' class='btn btn-danger' value='Xóa nhóm này' onclick='RemoveGroupFromUser(this);'>"
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
function RemoveGroupFromUser(button) {
    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");
    //var id = row.children('td').eq(5).children('input').attr("id");
    var group = row.children('td').eq(0).html();
    var userName = $("span#hiddenUserName").text();

    //alert(id);
    if (confirm("Bạn đang chọn xóa 1 dòng?")) {
        var urlRemove;
        if (location.hostname == "localhost")
            urlRemove = "/QlUser/RemoveGroupFromUser";
        else
            urlRemove = "/nk/QlUser/RemoveGroupFromUser";

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


// --- Begin AddUserToGroup=================        
function AddGroupToUser() {    
    var group = $("select#selGroup").val();
    var userName = $("span#hiddenUserName").text();
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
                            [group,
                             group,
                              "<input type='button' id='" + userName + "' class='btn btn-danger' value='Xóa nhóm này' onclick='RemoveUserFromGroup(this);'>"]);
                $('#inputNewUser').val("");
            } else alert(r);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    }); // end ajax


};

// ---- End AddUserToGroup

// --- Begin NewUser=================        
function NewUser() {
    var userName = $("input#inputNewUserName").val();
    var fullName = $("input#inputNewFullName").val();
    //alert(userName);
    var urlAdd;
    if (location.hostname == "localhost")
        urlAdd = "/QlUser/NewUser";
    else
        urlAdd = "/nk/QlUser/NewUser";

    //Send the JSON array to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: urlAdd,
        data: JSON.stringify({ "UserName": userName,"FullName": fullName }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            // alert(" record(s) deleted.");
            if ($.isNumeric(r)) {
                $('#tblUser').dataTable().fnAddData(
                            [r,
                             userName,
                             fullName,
                             "True",
                              "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' data-target='#modalResetPass' onclick='return ResetPass("+ userName +")'> Reset pass " + userName+" </button>",
                              "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' data-target='#modalGroup' onclick='return ListGroupByUser(" + userName + ")'> Thuộc nhóm </button>",
                              "<button type='button' class='btn btn-primary btn-sm' data-toggle='modal' data-target='#modalUser' onclick='return DetailUser("+r+")'> Chi tiết </button>"
                            ]);
                $('#modalNewUser').modal('hide');               

            }
            else alert(r);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    }); // end ajax

};
// ---- End NewGroup