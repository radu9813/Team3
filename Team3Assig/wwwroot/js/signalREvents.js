"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/messagehub").build();

connection.start()

connection.on("AddNewStudent", createNewStudentRow);
connection.on("UpdateStudent", updateStudentRow);
connection.on("RemoveStudent", removeStudentRow);

connection.on("AddNewDiploma", createNewDiplomaRow);
connection.on("UpdateDiploma", updateDiplomaRow);
connection.on("RemoveDiploma", removeDiplomaRow);

 function createNewStudentRow(id, name, birthdate, email){

     $("#student_table tbody").append("<tr>" +
         `<td>${id}</td>` +
         `<td>${name}</td>` +
         `<td>${birthdate}</td>` +
         `<td>${email}</td>` +
         `<td> 
            <a href = "/Students/Edit/${id}" > Edit</a > |
            <a href="/Students/Details/${id}">Details</a> |
            <a href="/Students/Delete/${id}">Delete</a></td>` +
         "</tr>");
}

function updateStudentRow(id, name, birthdate, email) {
    $(`tr[data-id="${id}"] td[data-field-name]`).text(name)
    $(`tr[data-id="${id}"] td[data-field-birthdate]`).text(birthdate)
    $(`tr[data-id="${id}"] td[data-field-email]`).text(email)
}

function removeStudentRow(id) {
    $(`tr[data-id="${id}"]`).remove()
}


function createNewDiplomaRow(id, thesis, abstract, completeness, supervisor, studentName) {
    const checkbox = completeness == true ? 'checked = "checked"' : ''

    $("#diploma_table tbody").append(
        `<tr data-id="${id}">` +
        `<td data-field-thesis="${thesis}">${thesis}</td>` +
        `<td data-field-abstract="${abstract}">${abstract}</td>` +
        `<td data-field-completeness="${id}"><input ${checkbox} class="check-box" disabled="disabled" type="checkbox"></td>` +
        `<td data-field-supervizor="${supervisor}">${supervisor}</td>` +
        `<td data-field-studentName="${studentName}">${studentName}</td>` +
        `<td> 
            <a href = "/Diplomata/Edit/${id}" >Edit</a > |
            <a href="/Diplomata/Details/${id}">Details</a> |
            <a href="/Diplomata/Delete/${id}">Delete</a></td>` +
        "</tr>");
}

function updateDiplomaRow(id, thesis, abstract, completeness, supervisor) {
    const checkbox = completeness == true ? 'checked = "checked"' : ''
    const check = `<td data-field-completeness="${id}"><input ${checkbox} class="check-box" disabled="disabled" type="checkbox"></td>`

    $(`tr[data-id="${id}"] td[data-field-thesis]`).text(thesis)
    $(`tr[data-id="${id}"] td[data-field-abstract]`).text(abstract)
    $(`tr[data-id="${id}"] td[data-field-completeness]`).replaceWith(check)
    $(`tr[data-id="${id}"] td[data-field-supervizor]`).text(supervisor)
}

function removeDiplomaRow(id) {
    $(`tr[data-id="${id}"]`).remove()
}