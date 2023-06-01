using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IUserService
{
    List<UserVM> UserGetAll();
    bool UserDelete(int id, string Username);
    bool UserRoleUpdate(int id, string Username);
}

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public UserService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public bool UserDelete(int id, string Username)
    {
        var data = db.Users.Where(x => x.Id == id).FirstOrDefault();
        if (data != null)
        {
            data.UpdatedDate = DateTime.Now;
            var log = new LogVM
                { Name = Username, Detail = Message.UserDelete, CreatedDate = DateTime.Now, NesneId = id.ToString() };
            var logData = _mapper.Map<Log>(log);

            db.Logs.Add(logData);
            data.Status = data.Status == Stat.Active ? Stat.Passive : Stat.Active;
            db.Users.Update(data);
            var x = db.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }

        return false;
    }

    public List<UserVM> UserGetAll()
    {
        var data = db.Users.ToList();
        if (data == null) return null;
        var dataModel = _mapper.Map<List<UserVM>>(data);
        return dataModel;
    }

    public bool UserRoleUpdate(int id, string Username)
    {
        var data = db.Users.Where(x => x.Id == id).FirstOrDefault();
        if (data != null)
        {
            data.UpdatedDate = DateTime.Now;
            var log = new LogVM
            {
                Name = Username, Detail = Message.UserRoleDelete, CreatedDate = DateTime.Now, NesneId = id.ToString()
            };
            var logData = _mapper.Map<Log>(log);

            db.Logs.Add(logData);
            data.Role = data.Role == Role.Admin ? Role.Editor : Role.Admin;
            db.Users.Update(data);
            var x = db.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }

        return false;
    }
}