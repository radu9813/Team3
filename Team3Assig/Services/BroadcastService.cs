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

        public void AddNewStudent(int id, string name, DateTime birthday, string email)
        {
            messageHub.Clients.All.SendAsync("AddNewStudent", id, name, birthday.ToString(), email);
        }

        public void UpdateStudent(int id, string name, DateTime birthday, string email)
        {
            messageHub.Clients.All.SendAsync("UpdateStudent", id, name, birthday.ToString(), email);
        }

        public void RemoveStudent(int id)
        {
            messageHub.Clients.All.SendAsync("RemoveStudent", id);
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
