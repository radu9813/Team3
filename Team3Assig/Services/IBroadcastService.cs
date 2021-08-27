using System;
using Team3Assig.Models;

namespace Team3Assig.Services
{
    public interface IBroadcastService
    {
        void AddNewStudent(int id, string name, DateTime birthday, string email);

        void UpdateStudent(int id, string name, DateTime birthday, string email);

        void RemoveStudent(int id);
        
        void AddNewDiploma(Diploma diploma);

        void UpdateDiploma(Diploma diploma);

        void RemoveDiploma(Diploma diploma);
    }
}