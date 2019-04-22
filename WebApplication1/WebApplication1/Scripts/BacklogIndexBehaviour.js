function SaveTask(rowId, backlogPosition) {

    let backlogID = backlog_left_items[backlogPosition].getAttribute("id");
    let rowInputs = document.getElementById(rowId).getElementsByTagName("input");

    var jsonObject = {
        Description: rowInputs[0].value,
        HoursDone: rowInputs[1].value,
        HoursEstimated: rowInputs[2].value,
        Backlog: backlogID
    }

    var xmlhttp = new XMLHttpRequest();   // new HttpRequest instance 
    xmlhttp.open("POST", "/BacklogTasks/Create");
    xmlhttp.setRequestHeader("Content-Type", "application/json");
    //xmlhttp.setRequestHeader('__RequestVerificationToken', $('input[name="__RequestVerificationToken"]').val());
    xmlhttp.send(JSON.stringify(jsonObject));

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.status != 200) {
            // обработать ошибку
            alert(xmlhttp.status + ': ' + xmlhttp.statusText); // пример вывода: 404: Not Found
        } else {
            // вывести результат
            alert('хуй блять'); // responseText -- текст ответа.
        }
    }

    //$.ajax({
    //    type: 'POST',
    //    url: '/BacklogTasks/Create',
    //    beforeSend: function (xhr) {
    //        xhr.setRequestHeader("XSRF-TOKEN",
    //            $('input:hidden[name="__RequestVerificationToken"]').val());
    //    },
    //    contentType: "application/json; charset=utf-8",
    //    data: JSON.stringify(jsonObject),
    //    success: function () { alert("success") },
    //    error: function () { alert("fail") },
    //    dataType: "json"
    //});

}

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
    cell.innerHTML = "<input type = 'button' class = 'btn-add-pk' value = '+New' onclick='addNewTaskLine(" + i + ");' />";
}

function addNewTaskLine(tableNumber) {

    const backlogPosition = tableNumber; // because count of taskTables == countBacklogs
    let currentTableRows = task_tables[tableNumber].getElementsByTagName("tr");
    var currentRow = currentTableRows[currentTableRows.length - 1];
    currentRow.setAttribute("id", "" + Math.floor(Math.random() * (300 - 1) + 1) + "");

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
    div_5.innerHTML = "<input type = 'button' class = 'btn btn-success' value = 'Save' onClick = 'SaveTask(" + currentRow.getAttribute("id") + ", " + backlogPosition + ");' /> <input type = 'button' class = 'btn btn-danger' value = 'Cancel' onClick = 'CancelTask();' />";

    cell_2.appendChild(div_5);

}