using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IAboutService
{
    AboutVM AboutGet();
    bool AboutUpdate(AboutVM data, string Username);
    bool AboutAdd(AboutVM data, string Username);
}

public class AboutService : IAboutService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public AboutService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public bool AboutAdd(AboutVM data, string Username)
    {
        var dataModel = _mapper.Map<About>(data);
        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM { Name = Username, Detail = Message.AboutOurWhoCreate, CreatedDate = DateTime.Now };
        var logData = _mapper.Map<Log>(log);

        db.Abouts.Add(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();
        if (x > 0)
            return true;
        return false;
    }

    public AboutVM AboutGet()
    {
        var data = db.Abouts.FirstOrDefault();
        if (data == null) return null;
        var dataModel = _mapper.Map<AboutVM>(data);
        return dataModel;
    }

    public bool AboutUpdate(AboutVM data, string Username)
    {
        var dataModel = _mapper.Map<About>(data);
        dataModel.UpdatedDate = DateTime.Now;

        var log = new LogVM
        {
            Name = Username, Detail = Message.AboutDataUpdate, CreatedDate = DateTime.Now, NesneId = data.Id.ToString()
        };
        var logData = _mapper.Map<Log>(log);


        db.Abouts.Update(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();

        if (x > 0)
            return true;
        return false;
    }
}