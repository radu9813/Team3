"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/messagehub").build();

connection.start()

connection.on("AddNewStudent", (id, name, birthdate, email) => {
    createNewStudentRow(id, name, birthdate, email);
});

 function createNewStudentRow(id, name, birthdate, email){
     const links = ` < a href = "/Students/Edit/${id}" > Edit</a > |
          <a href="/Students/Details/${id}">Details</a> |
          <a href="/Students/Delete/${id}">Delete</a>`

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