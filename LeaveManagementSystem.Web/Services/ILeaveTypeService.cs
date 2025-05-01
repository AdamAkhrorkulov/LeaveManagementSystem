using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.Services;

public interface ILeaveTypeService
{
    Task<List<LeaveTypeReadOnlyVM>> GetAllAsync();
    Task<T?> GetAsync<T>(int id) where T : class;
    Task Remove(int id);
    Task Edit(LeaveTypeEditVM leaveTypeEdit);
    Task Create(LeaveTypeCreateVM leaveTypeCreate);
    bool LeaveTypeExists(int id);
    Task<bool> CheckIfLeaveTypeNameExists(string name);
    Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit);
}