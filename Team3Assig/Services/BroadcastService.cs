using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    }
}
