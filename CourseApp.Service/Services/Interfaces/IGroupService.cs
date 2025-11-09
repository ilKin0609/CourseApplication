using CourseApp.Domain.Models;

namespace CourseApp.Service.Services.Interfaces;

public interface IGroupService
{
    void Create(Group group);
    void Update(int id,Group group);
    void Delete(int id);
    Group GetById(int groupId);
    List<Group> GetAllByTeacher(string teacherName);
    List<Group> GetAllByRoom(int roomNumber);
    List<Group> GetAll();
}
