$(document).ready(function () {
    FetchRole()
});



$("#btn").click(function(){
    $("#roleModal").modal('show');
});

$("#closemodal").click(function(){
    $("#roleModal").modal('hide');
});


// add role
$("#roleform").submit(function(e){
    e.preventDefault();
    var obj = $(this).serialize();
    $.ajax({
        url: '/Role/Create',
        type: 'POST',
        data: obj,
        dataType: 'json',

        success: function(response){
            alert(response.message);

            $("#roleModal").modal('hide');
            $("#roleform")[0].reset();
            FetchRole();
        },
        error: function(xhr){
            alert("Error");
        }
    });
});


// fetch all role
function FetchRole(){
    $.ajax({
        url: '/Role/GetAll',
        type: 'GET',
        dataType: 'json',

        success: function(result,status){
            var obj = '';
            $.each(result,function(index,item){
                obj+='<tr>';
                obj+= '<td>'+item.roleId+'</td>'
                obj+= '<td>'+item.roleName +'</td>'
                obj+= '<td>'+item.description +'</td>'
                obj+= '<td>'+(item.isActive == 1 ? 'Active' : 'Inactive') +'</td>'

                obj += '<td>';
                obj += '<button class="btn btn-sm btn-primary" onclick="EditRole(' + item.roleId + ')">Edit</button> ';
                obj += '<button class="btn btn-sm btn-danger" onclick="DeleteRole(' + item.roleId + ')">Delete</button>';
                obj += '</td>';
                obj+='</tr>';
            });
            $("#roledata").html(obj);
        },
        error: function () {
            alert("error")
        }
    })
}




function EditRole(id) {

    $.ajax({
        url: '/Role/GetById?id=' + id,
        type: 'GET',
        dataType: 'json',

        success: function (result) {

            $("#UpdateRoleId").val(result.roleId);

            $("#UpdateRoleName").val(result.roleName);

            $("#UpdateDescription").val(result.description);

            $("#UpdateIsActive").val(result.isActive);

            $("#updateRoleModal").modal('show');
        },

        error: function () {
            alert("Error");
        }
    });
}

$("#closeUpdateModal").click(function () {

    $("#updateRoleModal").modal('hide');

});

$("#updateRoleForm").submit(function (e) {

    e.preventDefault();

    var obj = $(this).serialize();

    $.ajax({

        url: '/Role/Update',
        type: 'POST',
        data: obj,
        dataType: 'json',

        success: function (response) {

            if (response.success) {

                alert(response.message);

                $("#updateRoleModal").modal('hide');

                $("#updateRoleForm")[0].reset();

                FetchRole();
            }
            else {

                alert(response.message);
            }
        },

        error: function () {

            alert("Error");
        }
    });

});

function DeleteRole(id) {

    if (!confirm("Are you sure you want to delete this role?")) {
        return;
    }

    $.ajax({
        url: '/Role/Delete?id=' + id,
        type: 'POST',
        dataType: 'json',

        success: function(response) {

            if (response.success) {

                alert(response.message);

                FetchRole();
            }
            else {
                alert(response.message);
            }
        },

        error: function() {
            alert("Error");
        }
    });
}