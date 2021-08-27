using System;

namespace Team3Assig.Services
{
    public interface IBroadcastService
    {
        void AddNewStudent(int id, string name, DateTime birthday, string email);
    }
}