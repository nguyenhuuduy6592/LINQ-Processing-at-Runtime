window.getFieldList = function () {
    var dataModelCount = window.dataModel.length;
    var col = []; // define an empty array
    if (dataModelCount > 0) {
        // retrieve column header
        for (var i = 0; i < dataModelCount; i++) {
            for (var key in window.dataModel[i]) {
                if (col.indexOf(key) === -1) {
                    col.push(key);
                    $('#Fields').append(`<option value="${key}"> 
                            ${key} 
                         </option>`);
                    $('#Values').append(`<option value="${key}"> 
                            ${key} 
                         </option>`); 
                }
            }
        }
    }
    window.fieldList = col;
};

window.generateDynamicTable = function () {
    var dataModelCount = window.dataModel.length;
    if (dataModelCount > 0) {
        // CREATE DYNAMIC TABLE.
        var table = document.createElement("table");
        table.style.width = '50%';
        table.setAttribute('border', '1');
        table.setAttribute('cellspacing', '0');
        table.setAttribute('cellpadding', '5');

        // retrieve column header
        var col = window.fieldList;

        // CREATE TABLE HEAD .
        var tHead = document.createElement("thead");

        // CREATE ROW FOR TABLE HEAD .
        var hRow = document.createElement("tr");

        // ADD COLUMN HEADER TO ROW OF TABLE HEAD.
        for (var i = 0; i < col.length; i++) {
            var th = document.createElement("th");
            th.innerHTML = col[i].split(/(?=[A-Z])/).join(" ");
            hRow.appendChild(th);
        }
        tHead.appendChild(hRow);
        table.appendChild(tHead);

        // CREATE TABLE BODY .
        var tBody = document.createElement("tbody");

        // ADD COLUMN HEADER TO ROW OF TABLE HEAD.
        for (var i = 0; i < dataModelCount; i++) {

            var bRow = document.createElement("tr"); // CREATE ROW FOR EACH RECORD .


            for (var j = 0; j < col.length; j++) {
                var td = document.createElement("td");
                td.innerHTML = window.dataModel[i][col[j]];
                bRow.appendChild(td);
            }
            tBody.appendChild(bRow)

        }
        table.appendChild(tBody);

        // FINALLY ADD THE NEWLY CREATED TABLE WITH JSON DATA TO A CONTAINER.
        var divContainer = document.getElementById("dataModel");
        divContainer.innerHTML = "";
        divContainer.appendChild(table);
    }
};

window.generateChart = function () {
    var dataModelCount = window.dataModel.length;
    if (dataModelCount > 0) {
        var ctx = document.getElementById('myChart').getContext('2d');
        var field = $('#Fields').val();
        var value = $('#Values').val();
        var fieldList = $.map(window.dataModel, function (val, index) {
            return val[field];
        });
        var valueList = $.map(window.dataModel, function (val, index) {
            return val[value];
        });

        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: fieldList,
                datasets: [{
                    label: '# of Votes',
                    data: valueList,
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    }
};

$("#viewGrid").on("click", function () {
    $("#dataModel").show();
    $("#myChart").hide();
    generateDynamicTable();
});

$("#viewChart").on("click", function () {
    $("#dataModel").hide();
    $("#myChart").show();
    generateChart();
});