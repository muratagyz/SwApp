using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IAboutSubTableService
{
    List<AboutSubTableVM> AboutSubTableGetAll();
    AboutSubTableVM AboutSubTableGetById(int id);
    bool AboutSubTableAdd(AboutSubTableVM data, string Username);
    bool AboutSubTableDelete(int id, string Username);
    bool AboutSubTableUpdate(AboutSubTableVM data, string Username);
}

public class AboutSubTableService : IAboutSubTableService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public AboutSubTableService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public bool AboutSubTableAdd(AboutSubTableVM data, string Username)
    {
        var dataModel = _mapper.Map<AboutSubTable>(data);
        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM { Name = Username, Detail = Message.AboutSubTableDataCreate, CreatedDate = DateTime.Now };
        var logData = _mapper.Map<Log>(log);

        db.AboutSubTables.Add(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();
        if (x > 0)
            return true;
        return false;
    }

    public bool AboutSubTableDelete(int id, string Username)
    {
        var data = db.AboutSubTables.Where(x => x.Id == id).FirstOrDefault();
        if (data != null)
        {
            data.UpdatedDate = DateTime.Now;
            var log = new LogVM
            {
                Name = Username, Detail = Message.AboutSubTableDataDelete, CreatedDate = DateTime.Now,
                NesneId = id.ToString()
            };
            var logData = _mapper.Map<Log>(log);

            db.Logs.Add(logData);
            data.Status = data.Status == Stat.Active ? Stat.Passive : Stat.Active;
            db.AboutSubTables.Update(data);
            var x = db.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }

        return false;
    }

    public List<AboutSubTableVM> AboutSubTableGetAll()
    {
        var data = db.AboutSubTables.ToList();
        if (data == null) return null;
        var dataModel = _mapper.Map<List<AboutSubTableVM>>(data);
        return dataModel;
    }

    public AboutSubTableVM AboutSubTableGetById(int id)
    {
        var data = db.AboutSubTables.Where(x => x.Id == id).FirstOrDefault();
        if (data == null) return null;
        var dataModel = _mapper.Map<AboutSubTableVM>(data);
        return dataModel;
    }

    public bool AboutSubTableUpdate(AboutSubTableVM data, string Username)
    {
        var dataModel = _mapper.Map<AboutSubTable>(data);
        dataModel.UpdatedDate = DateTime.Now;

        var log = new LogVM
        {
            Name = Username, Detail = Message.AboutSubTableDataUpdate, CreatedDate = DateTime.Now,
            NesneId = data.Id.ToString()
        };
        var logData = _mapper.Map<Log>(log);


        db.AboutSubTables.Update(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();

        if (x > 0)
            return true;
        return false;
    }
}