/*Вывод текущей даты*/
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


/*Вывод текущего года*/
$(document).ready(function () {
    var currentDate = new Date();
    var year = currentDate.getFullYear();
    $('#Year').val(year);
});


/*фильтр по дате*/
/*$(function () {
    $('#yearSelect').change(function () {
        var year = $(this).val();

        // Отправка запроса на сервер
        $.get('/Request/FilteredRequests', { year: year }, function (data) {
            $('#requestTableBody').html(data);
        });
    });
});*/


//$(document).ready(function () {
//    $('#yearSelect').change(function () {
//        var year = $(this).val();
//        var userName = 'Ads'/*$('#userName').data('username')*/; // Получаем имя пользователя из data атрибута
//        $.ajax({
//            url: '/Request/FilteredRequests',
//            type: 'POST',
//            data: { year: year, userName: userName }, // Передаем выбранный год и имя пользователя
//            success: function (data) {
//                $('#requestTableBody').html(data);
//            }
//        });
//    });
//});

$(document).ready(function () {
    // Обработчик клика по кнопке "Применить фильтры"
    $('#applyFiltersBtn').click(function () {
        // Собираем значения фильтров
        var year = $('#yearSelect').val();
        var status = $('#Stat').val();
        var department = $('#Dep').val();
        var courseBegin = $('#CourseBegin').val();
        var courseEnd = $('#CourseEnd').val();
        var fullName = $('#FullName').val();
        var requestNumber = $('#RequestNumber').val();

        // Отправляем значения фильтров на сервер
        $.ajax({
            url: '/Request/FilteredRequests',
            type: 'POST',
            data: {
                year: year,
                status: status,
                department: department,
                courseBegin: courseBegin,
                courseEnd: courseEnd,
                fullName: fullName,
                requestNumber: requestNumber
            },
            success: function (data) {
                $('#requestTableBody').html(data);
            }
        });
    });
});
