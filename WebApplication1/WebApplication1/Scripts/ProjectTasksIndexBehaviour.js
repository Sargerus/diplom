function RemoveDoneTask(taskid,projectid,divid) {

    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    $.post(
        "/ProjectTasks/SetInvisible",
        {
            __RequestVerificationToken: token,
            taskid: taskid,
            projectid: projectid
        },
        function (data) {  },
        "json"
        )
        .fail(function () { alert("fail"); })
        .done(function () { var elem = document.getElementById(divid); elem.remove(); });


}