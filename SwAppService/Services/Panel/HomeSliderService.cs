using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IHomeSliderService
{
    List<HomeSliderVM> HomeSliderGetAll();
    HomeSliderVM HomeSliderGetById(int id);
    bool HomeSliderAdd(HomeSliderVM data, string Username);
    bool HomeSliderDelete(int id, string Username);
    bool HomeSliderUpdate(HomeSliderVM data, string Username);
}

public class HomeSliderService : IHomeSliderService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public HomeSliderService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public bool HomeSliderAdd(HomeSliderVM data, string Username)
    {
        var dataModel = _mapper.Map<HomeSlider>(data);
        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM { Name = Username, Detail = Message.HomeSliderDataCreate, CreatedDate = DateTime.Now };
        var logData = _mapper.Map<Log>(log);

        db.HomeSliders.Add(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();
        if (x > 0)
            return true;
        return false;
    }

    public bool HomeSliderDelete(int id, string Username)
    {
        var data = db.HomeSliders.Where(x => x.Id == id).FirstOrDefault();
        if (data != null)
        {
            data.UpdatedDate = DateTime.Now;
            var log = new LogVM
            {
                Name = Username, Detail = Message.HomeSliderDataDelete, CreatedDate = DateTime.Now,
                NesneId = id.ToString()
            };
            var logData = _mapper.Map<Log>(log);

            db.Logs.Add(logData);
            data.Status = data.Status == Stat.Active ? Stat.Passive : Stat.Active;
            db.HomeSliders.Update(data);
            var x = db.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }

        return false;
    }

    public List<HomeSliderVM> HomeSliderGetAll()
    {
        var data = db.HomeSliders.ToList();
        if (data == null) return null;
        var dataModel = _mapper.Map<List<HomeSliderVM>>(data);
        return dataModel;
    }

    public HomeSliderVM HomeSliderGetById(int id)
    {
        var data = db.HomeSliders.Where(x => x.Id == id).FirstOrDefault();
        if (data == null) return null;
        var dataModel = _mapper.Map<HomeSliderVM>(data);
        return dataModel;
    }

    public bool HomeSliderUpdate(HomeSliderVM data, string Username)
    {
        var dataModel = _mapper.Map<HomeSlider>(data);
        dataModel.UpdatedDate = DateTime.Now;

        var log = new LogVM
        {
            Name = Username, Detail = Message.HomeSliderDataUpdate, CreatedDate = DateTime.Now,
            NesneId = data.Id.ToString()
        };
        var logData = _mapper.Map<Log>(log);


        db.HomeSliders.Update(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();

        if (x > 0)
            return true;
        return false;
    }
}