let backlog_left_items = document.getElementsByClassName("backlog-left-item-pk");
let backlog_right_items = document.getElementsByClassName("backlog-right-item-pk");
let task_tables = document.getElementsByClassName("task-table-pk");

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

    var rowsCount = task_tables[i].getElementsByTagName("tr").length;

    var row = task_tables[i].insertRow(rowsCount);
    var cell = row.insertCell(0);
    cell.setAttribute("colspan", "2"); cell.setAttribute("class", "div-text-center-pk hover-element-pk");
    cell.innerHTML = "<input type = 'button' class = 'btn-add-pk' value = '+New' onclick='addNewTaskLine(row);' />";
}

function addNewTaskLine(currentRow) {

    while (currentRow.firstChild) {
        currentRow.removeChild(currentRow.firstChild);
    }

    var cell = currentRow.insertCell(0);
    cell.setAttribute("width", "85%"); cell.setAttribute("height", "50%");

    var div_1 = document.createElement("div"); div_1.setAttribute("class", "task-container-pk"); div_1.setAttribute("style", "margin:.2em;");
    var div_2 = document.createElement("div"); div_2.setAttribute("class", "flex-item-big-pk hover-element-pk"); div_2.setAttribute("style", "padding-left:.5em");
    var div_3 = document.createElement("div"); div_3.setAttribute("class", "flex-item-middle-pk div-text-center-pk");
    var div_4 = document.createElement("div"); div_3.setAttribute("class", "flex-item-small-pk div-text-center-pk");
    var inputTaskDescription = document.createElement("input"); inputTaskDescription.setAttribute("type", "text"); inputTaskDescription.setAttribute("placeholder", "Task Description"); /*inputTaskDescription.setAttribute("style", "margin-right:2em;");*/
    var inputTaskEstimate = document.createElement("input"); inputTaskEstimate.setAttribute("type", "number"); inputTaskEstimate.setAttribute("placeholder", "Task Estimate"); inputTaskEstimate.setAttribute("min", "0"); /*inputTaskDescription.setAttribute("style", "margin-right:2em;");*/
    var inputTaskDone = document.createElement("input"); inputTaskDone.setAttribute("type", "number"); inputTaskDone.setAttribute("placeholder", "Task Done"); inputTaskEstimate.setAttribute("min", "0");

    div_2.appendChild(inputTaskDescription);
    div_3.appendChild(inputTaskEstimate);
    div_4.appendChild(inputTaskDone);

    div_1.appendChild(div_2);
    div_1.appendChild(div_4);
    div_1.appendChild(div_3);
    cell.appendChild(div_1);

    var cell_2 = currentRow.insertCell(1);
    cell_2.setAttribute("width", "15%");

    var div_5 = document.createElement("div");
    div_5.innerHTML = "<input type = 'button' class = 'btn btn-success' value = 'Save' onClick = 'SaveTask();' style='float:left;' /> <input type = 'button' class = 'btn btn-danger' value = 'Cancel' onClick = 'CancelTask();' style='float:right;' />";

    cell_2.appendChild(div_5);

}