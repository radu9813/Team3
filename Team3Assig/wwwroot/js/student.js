"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/messagehub").build();

connection.start()

connection.on("AddNewStudent", (id, name, birthdate, email) => {
    createNewStudentRow(id, name, birthdate, email);
});

connection.on("UpdateStudent", (id, name, birthdate, email) => {
    updateStudentRow(id, name, birthdate, email);
});

connection.on("RemoveStudent", (id) => {
    removeStudentRow(id);
});

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
