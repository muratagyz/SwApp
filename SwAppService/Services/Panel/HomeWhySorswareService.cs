using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IHomeWhySorswareService
{
    List<HomeWhySorswareVM> HomeWhySorswareGetAll();
    HomeWhySorswareVM HomeWhySorswareGetById(int id);
    bool HomeWhySorswareAdd(HomeWhySorswareVM data, string Username);
    bool HomeWhySorswareDelete(int id, string Username);
    bool HomeWhySorswareUpdate(HomeWhySorswareVM data, string Username);
}

public class HomeWhySorswareService : IHomeWhySorswareService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public HomeWhySorswareService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public bool HomeWhySorswareAdd(HomeWhySorswareVM data, string Username)
    {
        var dataModel = _mapper.Map<HomeWhySorsware>(data);
        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM { Name = Username, Detail = Message.HomeWhySorswareDataCreate, CreatedDate = DateTime.Now };
        var logData = _mapper.Map<Log>(log);

        db.HomeWhySorswares.Add(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();
        if (x > 0)
            return true;
        return false;
    }

    public bool HomeWhySorswareDelete(int id, string Username)
    {
        var data = db.HomeWhySorswares.Where(x => x.Id == id).FirstOrDefault();
        if (data != null)
        {
            data.UpdatedDate = DateTime.Now;
            var log = new LogVM
            {
                Name = Username, Detail = Message.HomeWhySorswareDataDelete, CreatedDate = DateTime.Now,
                NesneId = id.ToString()
            };
            var logData = _mapper.Map<Log>(log);

            db.Logs.Add(logData);
            data.Status = data.Status == Stat.Active ? Stat.Passive : Stat.Active;
            db.HomeWhySorswares.Update(data);
            var x = db.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }

        return false;
    }

    public List<HomeWhySorswareVM> HomeWhySorswareGetAll()
    {
        var data = db.HomeWhySorswares.ToList();
        if (data == null) return null;
        var dataModel = _mapper.Map<List<HomeWhySorswareVM>>(data);
        return dataModel;
    }

    public HomeWhySorswareVM HomeWhySorswareGetById(int id)
    {
        var data = db.HomeWhySorswares.Where(x => x.Id == id).FirstOrDefault();
        if (data == null) return null;
        var dataModel = _mapper.Map<HomeWhySorswareVM>(data);
        return dataModel;
    }

    public bool HomeWhySorswareUpdate(HomeWhySorswareVM data, string Username)
    {
        var dataModel = _mapper.Map<HomeWhySorsware>(data);
        dataModel.UpdatedDate = DateTime.Now;

        var log = new LogVM
        {
            Name = Username, Detail = Message.HomeWhySorswareDataUpdate, CreatedDate = DateTime.Now,
            NesneId = data.Id.ToString()
        };
        var logData = _mapper.Map<Log>(log);


        db.HomeWhySorswares.Update(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();

        if (x > 0)
            return true;
        return false;
    }
}