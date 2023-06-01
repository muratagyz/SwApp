using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IHomeProjectWorkService
{
    List<HomeProjectWorkVM> HomeProjectWorkGetAll();
    HomeProjectWorkVM HomeProjectWorkGetById(int id);
    bool HomeProjectWorkAdd(HomeProjectWorkVM data, string Username);
    bool HomeProjectWorkDelete(int id, string Username);
    bool HomeProjectWorkUpdate(HomeProjectWorkVM data, string Username);
}

public class HomeProjectWorkService : IHomeProjectWorkService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public HomeProjectWorkService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public bool HomeProjectWorkAdd(HomeProjectWorkVM data, string Username)
    {
        var dataModel = _mapper.Map<HomeProjectWork>(data);
        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM { Name = Username, Detail = Message.HomeProjectWorkDataCreate, CreatedDate = DateTime.Now };
        var logData = _mapper.Map<Log>(log);

        db.HomeProjectWorks.Add(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();
        if (x > 0)
            return true;
        return false;
    }

    public bool HomeProjectWorkDelete(int id, string Username)
    {
        var data = db.HomeProjectWorks.Where(x => x.Id == id).FirstOrDefault();
        if (data != null)
        {
            data.UpdatedDate = DateTime.Now;
            var log = new LogVM
            {
                Name = Username, Detail = Message.HomeProjectWorkDataDelete, CreatedDate = DateTime.Now,
                NesneId = id.ToString()
            };
            var logData = _mapper.Map<Log>(log);

            db.Logs.Add(logData);
            data.Status = data.Status == Stat.Active ? Stat.Passive : Stat.Active;
            db.HomeProjectWorks.Update(data);
            var x = db.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }

        return false;
    }

    public List<HomeProjectWorkVM> HomeProjectWorkGetAll()
    {
        var data = db.HomeProjectWorks.ToList();
        if (data == null) return null;
        var dataModel = _mapper.Map<List<HomeProjectWorkVM>>(data);
        return dataModel;
    }

    public HomeProjectWorkVM HomeProjectWorkGetById(int id)
    {
        var data = db.HomeProjectWorks.Where(x => x.Id == id).FirstOrDefault();
        if (data == null) return null;
        var dataModel = _mapper.Map<HomeProjectWorkVM>(data);
        return dataModel;
    }

    public bool HomeProjectWorkUpdate(HomeProjectWorkVM data, string Username)
    {
        var dataModel = _mapper.Map<HomeProjectWork>(data);
        dataModel.UpdatedDate = DateTime.Now;

        var log = new LogVM
        {
            Name = Username, Detail = Message.HomeProjectWorkDataUpdate, CreatedDate = DateTime.Now,
            NesneId = data.Id.ToString()
        };
        var logData = _mapper.Map<Log>(log);


        db.HomeProjectWorks.Update(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();

        if (x > 0)
            return true;
        return false;
    }
}