﻿
$(document).ready(function () {
    var currentDate = new Date();
    var day = currentDate.getDate();
    var month = currentDate.getMonth() + 1; // Месяцы в JavaScript начинаются с 0
    var year = currentDate.getFullYear();

    // Добавляем ведущий ноль для дней и месяцев, если они состоят из одной цифры
    if (day < 10) {
        day = '0' + day;
    }
    if (month < 10) {
        month = '0' + month;
    }

    var formattedDate = day + '.' + month + '.' + year;
    $('#CourseBeginning').val(formattedDate);
});



$(document).ready(function () {
    var currentDate = new Date();
    var year = currentDate.getFullYear();
    $('#Year').val(year);
});