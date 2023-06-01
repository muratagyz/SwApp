using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IProjectService
{
    List<ProjectVM> ProjectGetAll();
    ProjectVM ProjectGetById(int id);
    bool ProjectAdd(ProjectVM data, string Username);
    bool ProjectDelete(int id, string Username);
    bool ProjectUpdate(ProjectVM data, string Username);
}

public class ProjectService : IProjectService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public ProjectService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public bool ProjectAdd(ProjectVM data, string Username)
    {
        var dataModel = _mapper.Map<Project>(data);
        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM { Name = Username, Detail = Message.ProjectDataCreate, CreatedDate = DateTime.Now };
        var logData = _mapper.Map<Log>(log);

        db.Projects.Add(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();
        if (x > 0)
            return true;
        return false;
    }

    public bool ProjectDelete(int id, string Username)
    {
        var data = db.Projects.Where(x => x.Id == id).FirstOrDefault();
        if (data != null)
        {
            data.UpdatedDate = DateTime.Now;
            var log = new LogVM
            {
                Name = Username, Detail = Message.ProjectDataDelete, CreatedDate = DateTime.Now, NesneId = id.ToString()
            };
            var logData = _mapper.Map<Log>(log);

            db.Logs.Add(logData);
            data.Status = data.Status == Stat.Active ? Stat.Passive : Stat.Active;
            db.Projects.Update(data);
            var x = db.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }

        return false;
    }

    public List<ProjectVM> ProjectGetAll()
    {
        var data = db.Projects.ToList();
        if (data == null) return null;
        var dataModel = _mapper.Map<List<ProjectVM>>(data);
        return dataModel;
    }

    public ProjectVM ProjectGetById(int id)
    {
        var data = db.Projects.Where(x => x.Id == id).FirstOrDefault();
        if (data == null) return null;
        var dataModel = _mapper.Map<ProjectVM>(data);
        return dataModel;
    }

    public bool ProjectUpdate(ProjectVM data, string Username)
    {
        var dataModel = _mapper.Map<Project>(data);
        dataModel.UpdatedDate = DateTime.Now;

        var log = new LogVM
        {
            Name = Username, Detail = Message.ProjectDataUpdate, CreatedDate = DateTime.Now,
            NesneId = data.Id.ToString()
        };
        var logData = _mapper.Map<Log>(log);


        db.Projects.Update(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();

        if (x > 0)
            return true;
        return false;
    }
}