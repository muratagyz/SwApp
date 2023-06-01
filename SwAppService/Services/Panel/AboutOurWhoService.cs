using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IAboutOurWhoService
{
    AboutOurWhoVM AboutOurGet();
    bool AboutOurUpdate(AboutOurWhoVM data, string Username);
    bool AboutOurAdd(AboutOurWhoVM data, string Username);
}

public class AboutOurWhoService : IAboutOurWhoService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public AboutOurWhoService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public bool AboutOurAdd(AboutOurWhoVM data, string Username)
    {
        var dataModel = _mapper.Map<AboutOurWho>(data);
        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM { Name = Username, Detail = Message.AboutOurWhoCreate, CreatedDate = DateTime.Now };
        var logData = _mapper.Map<Log>(log);

        db.AboutOurWhos.Add(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();
        if (x > 0)
            return true;
        return false;
    }

    public AboutOurWhoVM AboutOurGet()
    {
        var data = db.AboutOurWhos.FirstOrDefault();
        if (data == null) return null;
        var dataModel = _mapper.Map<AboutOurWhoVM>(data);
        return dataModel;
    }

    public bool AboutOurUpdate(AboutOurWhoVM data, string Username)
    {
        var dataModel = _mapper.Map<AboutOurWho>(data);
        dataModel.UpdatedDate = DateTime.Now;

        var log = new LogVM
        {
            Name = Username, Detail = Message.AboutOurWhoUpdate, CreatedDate = DateTime.Now,
            NesneId = data.Id.ToString()
        };
        var logData = _mapper.Map<Log>(log);


        db.AboutOurWhos.Update(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();

        if (x > 0)
            return true;
        return false;
    }
}