let backlog_left_items = document.getElementsByClassName("backlog-left-item-pk");
let backlog_right_items = document.getElementsByClassName("backlog-right-item-pk");
let task_tables = document.getElementsByClassName("main-backlog-div-pk");

for (let i = 0; i < backlog_left_items.length; i++) {

    switch (backlog_left_items[i].getAttribute("btype")) {
        case "Backlog":
            backlog_left_items[i].setAttribute("style", "background-color:rgba(38,148,5,0.5)")
            break;
        case "Defect":
            backlog_left_items[i].setAttribute("style", "background-color:rgba(207,7,0,0.5)")
            break;
        case "Sprint Backlog":
            backlog_left_items[i].setAttribute("style", "background-color:#e07d0b")
            break;
        default: backlog_left_items[i].setAttribute("background-color", "background-color:rgba(65, 206, 44,0.4)");
            break;
    }

    switch (backlog_right_items[i].getAttribute("bstate")) {
        case "In Progress":
            backlog_right_items[i].setAttribute("style", "color:orange");
            break;
        default: backlog_right_items[i].setAttribute("style", "color:green");
    }
}

for (let i = 0; i < task_tables.length; i++) {

    if (task_tables[i].getElementsByClassName("task-container-pk").length >= 3) {
        continue;
    } 

    var lineToAddTask = document.createElement("div"); lineToAddTask.setAttribute("class", "task-container-pk"); lineToAddTask.setAttribute("style", "margin-top:.5em;margin-bottom:.5em;");
    lineToAddTask.innerHTML = "<input type = 'button' class = 'btn-add-pk' value = '+' onclick='addNewTaskLine(" + i + ");' />";
    task_tables[i].appendChild(lineToAddTask);

}

function addNewTaskLine(tableNumber) {

    const backlogPosition = tableNumber; // because count of taskTables == countBacklogs
    let currentRow = task_tables[tableNumber].lastChild;
    let newId = Math.floor(Math.random() * (300 - 1) + 1);
    currentRow.setAttribute("id", "" + newId + "");

    while (currentRow.firstChild) {
        currentRow.removeChild(currentRow.firstChild);
    }

    var div_1 = document.createElement("div"); div_1.setAttribute("class", "flex-item-big-abs-pk"); /*div_1.setAttribute("style", "padding-left:.5em;");*/
    var div_2 = document.createElement("div"); div_2.setAttribute("class", "flex-item-small-pk div-text-center-pk");
    var div_3 = document.createElement("div"); div_3.setAttribute("class", "flex-item-small-pk div-text-center-pk"); /*div_3.setAttribute("style", "margin-bottom:.5em;");*/
    var div_4 = document.createElement("div"); div_4.setAttribute("class", "flex-item-big-pk div-text-center-pk"); 
    var inputTaskDescription = document.createElement("input"); inputTaskDescription.setAttribute("type", "text"); inputTaskDescription.setAttribute("placeholder", "Task Description");
    var inputTaskEstimate = document.createElement("input"); inputTaskEstimate.setAttribute("type", "number"); inputTaskEstimate.setAttribute("placeholder", "Task Estimate"); inputTaskEstimate.setAttribute("min", "0"); inputTaskEstimate.setAttribute("style", "margin-bottom:.4em;"); /*inputTaskDescription.setAttribute("style", "margin-right:2em;");*/
    var inputTaskDone = document.createElement("input"); inputTaskDone.setAttribute("type", "number"); inputTaskDone.setAttribute("placeholder", "Task Done"); inputTaskDone.setAttribute("min", "0");

    div_1.appendChild(inputTaskDescription);

    div_2.appendChild(inputTaskDone);
    div_3.appendChild(inputTaskEstimate);

    div_3.innerHTML = "<input type = 'number' height = '80%' placeholder = 'Task Estimate' min = '0'  /> <div class = 'div-text-center-pk'> <input type = 'button' width='50%' class = 'btn btn-sm btn-success' style = 'margin-top:.5em' value = 'Save' onClick = 'SaveTask(" + backlogPosition + ");' /> <input type = 'button' style = 'margin-top:.5em' class = 'btn btn-sm btn-danger' value = 'Cancel' onClick = 'CancelTask(" + backlogPosition + "," + newId + ");' /></div>";

    currentRow.appendChild(div_1);
    currentRow.appendChild(div_2);
    currentRow.appendChild(div_3);
    //currentRow.appendChild(div_4);

}

function SaveTask(backlogPosition) {

    let backlogID = backlog_left_items[backlogPosition].getAttribute("id");
    let rowInputs = task_tables[backlogPosition].lastChild.getElementsByTagName("input");

    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    $.ajax({
        type: 'POST',
        url: '/BacklogTasks/Create',
        data: {
            __RequestVerificationToken: token,
            Description: rowInputs[0].value,
            HoursDone: rowInputs[1].value,
            HoursEstimated: rowInputs[2].value,
            Backlog: backlogID
        },
        success: function (data) {

            currentRow = task_tables[backlogPosition].lastChild;

            while (currentRow.firstChild) {
                currentRow.removeChild(currentRow.firstChild);
            }

            let doneFor = Math.floor(data.HoursDone / data.HoursEstimated) * 100;

            var div_1 = document.createElement("div"); div_1.setAttribute("class", "flex-item-big-abs-pk hover-element-pk"); div_1.setAttribute("style", "padding-left:.5em;");
            div_1.innerHTML = "<a href = '/BacklogTasks/Details/" + data.TaskId + "' >" + data.Description + "</a>";

            var div_2 = document.createElement("div"); div_2.setAttribute("class", "flex-item-middle-pk div-text-center-pk hover-element-pk progress");
            div_2.innerHTML = "<div class='progress-bar progress-bar-striped bg-info text-center' role='progressbar' style='width: " + doneFor + "%' aria-valuenow=" + doneFor + " aria-valuemin='0' aria-valuemax='100'>" + data.HoursDone + "/" + data.HoursEstimated + "</div>";

            var div_3 = document.createElement("div"); div_3.setAttribute("class", "flex-item-small-pk div-text-center-pk hover-element-pk");
            div_3.innerHTML = "<a href = '/Reports/Create/" + data.TaskId + "' >Report</a>";

            var div_4 = document.createElement("div"); div_4.setAttribute("class", "hover-element-pk div-text-center-pk flex-item-small-pk");
            div_4.innerHTML = data.CreatedBy;

            currentRow.appendChild(div_1);
            currentRow.appendChild(div_2);
            currentRow.appendChild(div_3);
            currentRow.appendChild(div_4);
        },
        error: function () { alert("fail") }
    });
}

function CancelTask(backlogPosition, divId) {

    currentRow = document.getElementById(divId);

    while (currentRow.firstChild) {
        currentRow.removeChild(currentRow.firstChild);
    }

    currentRow.innerHTML = "<input type = 'button' class = 'btn-add-pk' value = '+' onclick='addNewTaskLine(" + backlogPosition + ");' />";

}