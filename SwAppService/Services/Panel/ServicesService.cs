using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IServicesService
{
    List<ServiceVM> ServiceGetAll();
    ServiceVM ServiceGetById(int id);
    bool ServiceAdd(ServiceVM data, string Username);
    bool ServiceDelete(int id, string Username);
    bool ServiceUpdate(ServiceVM data, string Username);
}

public class ServicesService : IServicesService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public ServicesService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public bool ServiceAdd(ServiceVM data, string Username)
    {
        var dataModel = _mapper.Map<Service>(data);
        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM { Name = Username, Detail = Message.ServiceDataCreate, CreatedDate = DateTime.Now };
        var logData = _mapper.Map<Log>(log);

        db.Services.Add(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();
        if (x > 0)
            return true;
        return false;
    }

    public bool ServiceDelete(int id, string Username)
    {
        var data = db.Services.Where(x => x.Id == id).FirstOrDefault();
        if (data != null)
        {
            data.UpdatedDate = DateTime.Now;
            var log = new LogVM
            {
                Name = Username, Detail = Message.ServiceDataDelete, CreatedDate = DateTime.Now, NesneId = id.ToString()
            };
            var logData = _mapper.Map<Log>(log);

            db.Logs.Add(logData);
            data.Status = data.Status == Stat.Active ? Stat.Passive : Stat.Active;
            db.Services.Update(data);
            var x = db.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }

        return false;
    }

    public List<ServiceVM> ServiceGetAll()
    {
        var data = db.Services.ToList();
        if (data == null) return null;
        var dataModel = _mapper.Map<List<ServiceVM>>(data);
        return dataModel;
    }

    public ServiceVM ServiceGetById(int id)
    {
        var data = db.Services.Where(x => x.Id == id).FirstOrDefault();
        if (data == null) return null;
        var dataModel = _mapper.Map<ServiceVM>(data);
        return dataModel;
    }

    public bool ServiceUpdate(ServiceVM data, string Username)
    {
        var dataModel = _mapper.Map<Service>(data);
        dataModel.UpdatedDate = DateTime.Now;

        var log = new LogVM
        {
            Name = Username, Detail = Message.ServiceDataUpdate, CreatedDate = DateTime.Now,
            NesneId = data.Id.ToString()
        };
        var logData = _mapper.Map<Log>(log);


        db.Services.Update(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();

        if (x > 0)
            return true;
        return false;
    }
}