using System.Data;
using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LeaveTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LeaveTypeReadOnlyVM>> GetAllAsync()      
        {
            var data = await _context.LeaveTypes.ToListAsync();
            var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
            return viewData;
        }

        public async Task<T?> GetAsync<T>(int id) where T : class
        {   
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(l => l.Id == id);
            if(data is null)
            {   
                return null;    
            }
            var dataView = _mapper.Map<T>(data);
            return dataView;
        }

        public async Task Remove(int id)
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(l => l.Id == id);
            if (data != null)
            {
                _context.LeaveTypes.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Edit(LeaveTypeEditVM leaveTypeEdit)
        {
            var leaveType = _mapper.Map<LeaveType>(leaveTypeEdit);
            _context.Update(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task Create(LeaveTypeCreateVM leaveTypeCreate)
        {
            var leaveType = _mapper.Map<LeaveType>(leaveTypeCreate);
            _context.Add(leaveType);
            await _context.SaveChangesAsync();
        }

        public bool LeaveTypeExists(int id)
        {
            return _context.LeaveTypes.Any(e => e.Id == id);
        }

        public async Task<bool> CheckIfLeaveTypeNameExists(string name)    
        {
            var lowercaseName = name.ToLower();
            return await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName));
        }

        public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit)
        {
            var lowercaseName = leaveTypeEdit.Name.ToLower();
            return await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName) && q.Id != leaveTypeEdit.Id);
        }
    }
}
