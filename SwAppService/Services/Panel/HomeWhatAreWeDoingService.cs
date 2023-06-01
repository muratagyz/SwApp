using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IHomeWhatAreWeDoingService
{
    HomeWhatAreWeDoingVM HomeWhatAreWeGet();
    bool HomeWhatAreWeUpdate(HomeWhatAreWeDoingVM data, string Username);
    bool HomeWhatAreWeAdd(HomeWhatAreWeDoingVM data, string Username);
}

public class HomeWhatAreWeDoingService : IHomeWhatAreWeDoingService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public HomeWhatAreWeDoingService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public HomeWhatAreWeDoingVM HomeWhatAreWeGet()
    {
        var data = db.HomeWhatAreWeDoings.FirstOrDefault();
        if (data == null) return null;
        var dataModel = _mapper.Map<HomeWhatAreWeDoingVM>(data);
        return dataModel;
    }

    public bool HomeWhatAreWeAdd(HomeWhatAreWeDoingVM data, string Username)
    {
        var dataModel = _mapper.Map<HomeWhatAreWeDoing>(data);
        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM { Name = Username, Detail = Message.HomeWhatAreWeDoingCreate, CreatedDate = DateTime.Now };
        var logData = _mapper.Map<Log>(log);

        db.HomeWhatAreWeDoings.Add(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();
        if (x > 0)
            return true;
        return false;
    }

    public bool HomeWhatAreWeUpdate(HomeWhatAreWeDoingVM data, string Username)
    {
        var dataModel = _mapper.Map<HomeWhatAreWeDoing>(data);
        dataModel.UpdatedDate = DateTime.Now;

        var log = new LogVM
        {
            Name = Username, Detail = Message.HomeWhatAreWeDoingUpdate, CreatedDate = DateTime.Now,
            NesneId = data.Id.ToString()
        };
        var logData = _mapper.Map<Log>(log);


        db.HomeWhatAreWeDoings.Update(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();

        if (x > 0)
            return true;
        return false;
    }
}