using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team3Assig.Models;

namespace Team3Assig.Services
{
    public class BroadcastService : IBroadcastService
    {
        private readonly IHubContext<MessageHub> messageHub;

        public BroadcastService(IHubContext<MessageHub> messageHub)
        {
            this.messageHub = messageHub;
        }

        public void AddNewStudent(Student student)
        {
            messageHub.Clients.All.SendAsync("AddNewStudent", student.StudentId, student.Name, student.Birthdate.ToString(), student.EmailAddress);
        }

        public void UpdateStudent(Student student)
        {
            messageHub.Clients.All.SendAsync("UpdateStudent", student.StudentId, student.Name, student.Birthdate.ToString(), student.EmailAddress);
        }

        public void RemoveStudent(Student student)
        {
            messageHub.Clients.All.SendAsync("RemoveStudent", student.StudentId);
        }

        public void AddNewDiploma(Diploma diploma)
        {
            messageHub.Clients.All.SendAsync("AddNewDiploma", diploma.DiplomaId, diploma.Thesis, diploma.Abstract, diploma.Completeness, diploma.Supervisor, diploma.Student.Name);
        }

        public void UpdateDiploma(Diploma diploma)
        {
            messageHub.Clients.All.SendAsync("UpdateDiploma", diploma.DiplomaId, diploma.Thesis, diploma.Abstract, diploma.Completeness, diploma.Supervisor);
        }

        public void RemoveDiploma(Diploma diploma)
        {
            messageHub.Clients.All.SendAsync("RemoveDiploma", diploma.DiplomaId);
        }
    }
}
