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
    $('#Course_Start').val(formattedDate);
});


//Вывод текущего года
//$(document).ready(function () {
//    var currentDate = new Date();
//    var year = currentDate.getFullYear();
//    $('#Year').val(year);
//});


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



/*$(document).ready(function () {

    // Выпадающий список
    $("#Full_Name").on("input", function () {
        var input = $(this).val();
        if (input.length >= 1) {
            $.ajax({
                url: "/Home/GetSimilarNames",
                data: { name: input },
                type: "GET",
                dataType: "json",
                success: function (data) {
                    var listGroup = $("#similarNamesList");
                    listGroup.empty();
                    if (data && data.length > 0) {
                        data.forEach(function (item) {
                            console.log(item);
                            var listItem = $("<a href='#' class='list-group-item list-group-item-action'></a>").text(item.name);
                            listItem.click(function () {
                                console.log("Click event fired!");
                                $("#Full_Name").val(item.name);
                                listGroup.hide();
                            });
                            listGroup.append(listItem);
                        });
                        listGroup.show();
                    } else {
                        listGroup.hide();
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            });
        } else {
            $("#similarNamesList").hide();
        }
    });

    // Отображение выпадающего списка при фокусе на поле ввода
    $("#Full_Name").on("focus", function () {
        $("#similarNamesList").show();
    });

    // Скрытие выпадающего списка при потере фокуса с поля ввода
    $("#Full_Name").on("blur", function () {
        $("#similarNamesList").hide();
    });

    // При наведении на элементы списка меняем цвет фона
    $("#similarNamesList").on("mouseenter", "a.list-group-item", function () {
        $(this).css("background-color", "#e5e5e5");
    });

    // При уходе курсора с элементов списка возвращаем обычный цвет фона
    $("#similarNamesList").on("mouseleave", "a.list-group-item", function () {
        $(this).css("background-color", "white");
    });

    // Закрытие списка предложений при клике вне списка и поля ввода
    $(document).on("click", function (e) {
        if (!$("#similarNamesList").is(e.target) && !$("#Full_Name").is(e.target) && $("#similarNamesList").has(e.target).length === 0 && $("#Full_Name").has(e.target).length === 0) {
            $("#similarNamesList").hide();
        }
    });

});*/


$(document).ready(function () {

    // Выпадающий список
    $("#Full_Name").on("input", function () {
        var input = $(this).val();
        if (input.length >= 1) {
            $.ajax({
                url: "/Home/GetSimilarNames",
                data: { name: input },
                type: "GET",
                dataType: "json",
                success: function (data) {
                    var listGroup = $("#similarNamesList");
                    listGroup.empty();
                    if (data && data.length > 0) {
                        data.forEach(function (item) {
                            var listItem = $("<a href='#' class='list-group-item list-group-item-action'></a>").text(item.userName);
                            console.log(item);
                            listItem.click(function () {
                                $("#Full_Name").val(item.userName);
                                $("#Department").val(item.deptName);
                                $("#Position").val(item.position);
                                listGroup.hide();
                            });

                            /*$("#similarNamesList").on("click", "a.list-group-item", function () {
                                console.log("Click event on list-group-item fired!");
                                var name = $(this).text(); // Получаем текст элемента, на который нажали
                                $("#Full_Name").val(item.name); // Вставляем текст в поле ФИО обучающегося
                                $("#similarNamesList").hide(); // Скрываем выпадающий список
                            });*/

                            listGroup.append(listItem);
                        });
                        listGroup.show();
                    } else {
                        listGroup.hide();
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            });
        } else {
            $("#similarNamesList").hide();
        }
    });

    // Отображение выпадающего списка при фокусе на поле ввода
    $("#Full_Name").on("focus", function () {
        $("#similarNamesList").show();
    });

    // Скрытие выпадающего списка при потере фокуса с поля ввода
    /*$("#Full_Name").on("blur", function () {
        $("#similarNamesList").hide();
    });*/

    // При наведении на элементы списка меняем цвет фона
    $("#similarNamesList").on("mouseenter", "a.list-group-item", function () {
        $(this).css("background-color", "#e5e5e5");
    });

    // При уходе курсора с элементов списка возвращаем обычный цвет фона
    $("#similarNamesList").on("mouseleave", "a.list-group-item", function () {
        $(this).css("background-color", "white");
    });

    // Закрытие списка предложений при клике вне списка и поля ввода
    $(document).on("click", function (e) {
        if (!$("#similarNamesList").is(e.target) && !$("#Full_Name").is(e.target) && $("#similarNamesList").has(e.target).length === 0 && $("#Full_Name").has(e.target).length === 0) {
            $("#similarNamesList").hide();
        }
    });


});