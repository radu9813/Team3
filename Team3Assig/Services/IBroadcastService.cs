using System;
using Team3Assig.Models;

namespace Team3Assig.Services
{
    public interface IBroadcastService
    {
        void AddNewStudent(Student student);

        void UpdateStudent(Student student);

        void RemoveStudent(Student student);
        
        void AddNewDiploma(Diploma diploma);

        void UpdateDiploma(Diploma diploma);

        void RemoveDiploma(Diploma diploma);
    }
}