//Вывод текущей даты
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


//Вывод текущего года
$(document).ready(function () {
    var currentDate = new Date();
    var year = currentDate.getFullYear();
    $('#Year').val(year);
});


//фильтры
$(document).ready(function () {
    // Обработчик клика по кнопке "Применить фильтры"
    $('#applyFiltersBtn').click(applyFilters);

    // Обработчик нажатия клавиши Enter в полях фильтрации
    $('#yearSelect, #Stat, #Dep, #CourseBegin, #CourseEnd, #FullName, #RequestNumber').keypress(function (event) {
        if (event.which === 13) {
            applyFilters();
        }
    });

    function applyFilters() {
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
    }
});


