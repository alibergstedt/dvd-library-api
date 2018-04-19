$(document).ready(function () {

    loadDvdLibrary();

    $('#edit-button').on('click', function (event) {

        var haveValidationErrors = checkAndDisplayValidationErrors($('#edit-form').find('input'));

        if (haveValidationErrors) {
            return false;
        }

        if ($('#edit-dvd-title').val() == "") {

            $('#errorMessages')
               .append($('<li>')
               .attr({ class: 'list-group-item list-group-item-danger' })
               .text('Please enter a title for the DVD.'));

            var stop = true;

        }

        var year = $('#edit-realease-year').val();

        if (year.length != 4 || isNaN(year) == true) {

            $('#errorMessages')
               .append($('<li>')
               .attr({ class: 'list-group-item list-group-item-danger' })
               .text('Please enter a 4-digit year.'));

            var stop = true;
        }

        if (stop == true) { return false; }

        $.ajax({
            type: 'PUT',
            url: 'http://allisonbergstedt.com/dvd/' + $('#edit-dvd-id').val(),
            data: JSON.stringify({
                dvdId: $('#edit-dvd-id').val(),
                title: $('#edit-dvd-title').val(),
                realeaseYear: $('#edit-realease-year').val(),
                director: $('#edit-director').val(),
                rating: $('#edit-rating').val(),
                notes: $('#edit-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function (data) {
                $('#errorMessages').empty();
                hideEditForm();
                loadDvdLibrary();
            },
            complete: function (data) {
                $('#errorMessages').empty();
                hideEditForm();
                loadDvdLibrary();
            },
            error: function () {
                $('#errorMessages')
                    .append($('<li>')
                    .attr({ class: 'list-group-item  list-group-item-danger' })
                    .text('Error editing dvd. Please try again later.'));
            }
        })
    })

    $('#create-button').on('click', function (event) {
        showAddForm();
    })

    $("#add-button").on('click', function (event) {

        var haveValidationErrors = checkAndDisplayValidationErrors($('#add-form').find('input'));

        if (haveValidationErrors) {
            return false;
        }

        if ($('#add-dvd-title').val() == "") {

            $('#errorMessages')
               .append($('<li>')
               .attr({ class: 'list-group-item list-group-item-danger' })
               .text('Please enter a title for the DVD.'));

            var stop = true;

        }

        var year = $('#add-realease-year').val();

        if (year.length != 4 || isNaN(year) == true) {

            $('#errorMessages')
               .append($('<li>')
               .attr({ class: 'list-group-item list-group-item-danger' })
               .text('Please enter a 4-digit year.'));

            var stop = true;
        }

        if (stop == true) { return false; }

        $.ajax({
            type: 'POST',
            url: 'http://allisonbergstedt.com/dvd',
            data: JSON.stringify({
                title: $('#add-dvd-title').val(),
                realeaseYear: $('#add-realease-year').val(),
                director: $('#add-director').val(),
                rating: $('#add-rating').val(),
                notes: $('#add-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function (data, status) {
                $('#errorMessages').empty();
                $('#add-dvd-title').val('');
                $('#add-realase-year').val('');
                $('#add-director').val('');
                $('#add-rating').val('');
                $('#add-notes').val('');
                hideAddForm();
                loadDvdLibrary();
            },
            complete: function (data, status) {
                $('#errorMessages').empty();
                $('#add-dvd-title').val('');
                $('#add-realase-year').val('');
                $('#add-director').val('');
                $('#add-rating').val('');
                $('#add-notes').val('');
                hideAddForm();
                loadDvdLibrary();
            },
            error: function () {
                $('#errorMessages')
                    .append($('<li>')
                    .attr({ class: 'list-group-item  list-group-item-danger' })
                    .text('Error adding dvd. Please try again later.'));
            }
        })

    });

    $("#search-button").on('click', function (event) {
        clearDvdTable();
        var choice = $('#dvd-search-category').val();
        var term = $('#searchTerm').val();

        if (choice == "searchCategory") {
            var tempUrl = 'http://allisonbergstedt.com/dvds';
        }
        else if (choice == "searchTitle" && term != null) {
            var tempUrl = 'http://allisonbergstedt.com/dvds/title/' + term;
        }
        else if (choice == "searchRealeaseYear" && term != null && term.length == 4 && isNaN(term) == false) {
            var tempUrl = 'http://allisonbergstedt.com/dvds/year/' + term;
        }
        else if (choice == "searchDirector" && term != null) {
            var tempUrl = 'http://allisonbergstedt.com/dvds/director/' + term;
        }
        else if (choice == "searchRating" && term != null) {
            var tempUrl = 'http://allisonbergstedt.com/dvds/rating/' + term;
        }
        else {
            $('#errorMessages')
                .append($('<li>')
                .attr({ class: 'list-group-item  list-group-item-danger' })
                .text('Both Search Category and Search Term are required.'))
        }

        searchForm(tempUrl);
    })

});

function loadDvdLibrary() {
    clearDvdTable();
    var contentRows = $('#contentRows');

    $.ajax({
        type: 'GET',
        url: 'http://allisonbergstedt.com/dvds',
        success: function (dvdArray) {
            $.each(dvdArray, function (index, dvd) {
                var title = dvd.title;
                var realeaseYear = dvd.realeaseYear;
                var director = dvd.director;
                var rating = dvd.rating;
                var dvdId = dvd.dvdId;

                var row = '<tr>';
                row += '<td><a onclick="showDvdDetails(' + dvdId + ')">' + title + '</a></td>';
                row += '<td>' + realeaseYear + '</td>';
                row += '<td>' + director + '</td>';
                row += '<td>' + rating + '</td>';
                row += '<td><a onclick="showEditForm(' + dvdId + ')">Edit</a>' + " " + "|" + " " + '<a onclick="deleteDvd(' + dvdId + ')">Delete</a></td>';
                row += '</tr>';

                contentRows.append(row);
            });
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                .attr({ class: 'list-group-item  list-group-item-danger' })
                .text('Error loading dvds. Please try again later.'));
        }
    });
}

function searchForm(tempUrl) {
    clearDvdTable();
    $('#errorMessages').empty();
    var contentRows = $('#contentRows');

    $.ajax({
        type: 'GET',
        url: tempUrl,
        success: function (data, status) {
            $.each(data, function (index, dvd) {
                var title = dvd.title;
                var realeaseYear = dvd.realeaseYear;
                var director = dvd.director;
                var rating = dvd.rating;
                var dvdId = dvd.dvdId;

                var row = '<tr>';
                row += '<td><a onclick="showDvdDetails(' + dvdId + ')">' + title + '</a></td>';
                row += '<td>' + realeaseYear + '</td>';
                row += '<td>' + director + '</td>';
                row += '<td>' + rating + '</td>';
                row += '<td><a onclick="showEditForm(' + dvdId + ')">Edit</a>' + " " + "|" + " " + '<a onclick="deleteDvd(' + dvdId + ')">Delete</a></td>';
                row += '</tr>';

                contentRows.append(row);
            });
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                .attr({ class: 'list-group-item  list-group-item-danger' })
                .text('Both Search Category and Search Term are required.'));
        }
    });
}


function showEditForm(dvdId) {
    $('#errorMessages').empty();

    $.ajax({
        type: 'GET',
        url: 'http://allisonbergstedt.com/dvd/' + dvdId,
        success: function (data, status) {
            $('#edit-dvd-title').val(data.title);
            $('#edit-realease-year').val(data.realeaseYear);
            $('#edit-director').val(data.director);
            $('#edit-rating').val(data.rating);
            $('#edit-notes').val(data.notes);
            $('#edit-dvd-id').val(data.dvdId);

        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                .attr({ class: 'list-group-item  list-group-item-danger' })
                .text('Error calling web service. Please try again later.'));
        }
    })


    $('#dvdHeader').hide();
    $('#dvdTableDiv').hide();
    $('#editFormDiv').show();
}

function hideEditForm() {
    $('#errorMessages').empty();

    $('#edit-dvd-title').val('');
    $('#edit-director').val('');
    $('#edit-realease-year').val('');
    $('#edit-rating').val('');
    $('#edit-notes').val('');

    $('#editFormDiv').hide();
    $('#dvdHeader').show();
    $('#dvdTableDiv').show();
}

function showAddForm() {
    $('#errorMessages').empty();
    $('#dvdHeader').hide();
    $('#dvdTableDiv').hide();
    $('#addFormDiv').show();
}

function hideAddForm() {
    $('#errorMessages').empty();

    $('#add-dvd-title').val('');
    $('#add-director').val('');
    $('#add-realease-year').val('');
    $('#add-rating').val('');
    $('#add-notes').val('');

    $('#addFormDiv').hide();
    $('#dvdHeader').show();
    $('#dvdTableDiv').show();
}

function hideDetailsForm() {
    $('#errorMessages').empty();

    $('#add-dvd-title').val('');
    $('#add-director').val('');
    $('#add-realease-year').val('');
    $('#add-rating').val('');
    $('#add-notes').val('');

    $('#dvdDetailsDiv').hide();
    $('#dvdHeader').show();
    $('#dvdTableDiv').show();
}

function clearDvdTable() {
    $('#contentRows').empty();
}

function deleteDvd(dvdId) {
    if (confirm("Are you sure you want to delete this Dvd from your collection?")) {
        $.ajax({
            type: 'DELETE',
            url: 'http://allisonbergstedt.com/dvd/' + dvdId,
            success: function () {
                loadDvdLibrary();
            },
            error: function () {
                $('#errorMessages')
                    .append($('<li>')
                    .attr({ class: 'list-group-item  list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
            }
        })
    }
    return false;
}

function showDvdDetails(dvdId) {
    $('#errorMessages').empty();

    $.ajax({
        type: 'GET',
        url: 'http://allisonbergstedt.com/dvd/' + dvdId,
        success: function (data, status) {
            $('#display-dvd-title').text(data.title);
            $('#display-realease-year').text(data.realeaseYear);
            $('#display-director').text(data.director);
            $('#display-rating').text(data.rating);
            $('#display-notes').text(data.notes);

        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                .attr({ class: 'list-group-item  list-group-item-danger' })
                .text('Error calling web service. Please try again later.'));
        }
    });

    $('#dvdHeader').hide();
    $('#dvdTableDiv').hide();
    $('#editFormDiv').hide();
    $('#dvdDetailsDiv').show();
}

function checkAndDisplayValidationErrors(input) {
    $('#errorMessages').empty();

    var errorMessages = [];

    input.each(function () {
        if (!this.validity.valid) {
            var errorField = $('label[for=' + this.id + ']').text();
            errorMessages.push(errorField + ' ' + this.validationMessage);
        }
    });

    if (errorMessages.length > 0) {
        $.each(errorMessages, function (index, message) {
            $('#errorMessages').append($('<li>').attr({ class: 'list-group-item list-group-item-danger' }).text(message));
        });
        return true;
    } else {
        return false;
    }
}
