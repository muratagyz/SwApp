using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IContactInfoService
{
    List<ContactInfoVM> ContactInfoWorkGetAll();
    ContactInfoVM ContactInfoGetById(int id);
    bool ContactInfoAdd(ContactInfoVM data, string Username);
    bool ContactInfoDelete(int id, string Username);
    bool ContactInfoUpdate(ContactInfoVM data, string Username);
}

public class ContactInfoService : IContactInfoService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public ContactInfoService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public bool ContactInfoAdd(ContactInfoVM data, string Username)
    {
        var dataModel = _mapper.Map<ContactInfo>(data);
        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM { Name = Username, Detail = Message.ContactInfoDataCreate, CreatedDate = DateTime.Now };
        var logData = _mapper.Map<Log>(log);

        db.ContactInfos.Add(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();
        if (x > 0)
            return true;
        return false;
    }

    public bool ContactInfoDelete(int id, string Username)
    {
        var data = db.ContactInfos.Where(x => x.Id == id).FirstOrDefault();
        if (data != null)
        {
            data.UpdatedDate = DateTime.Now;
            var log = new LogVM
            {
                Name = Username, Detail = Message.ContactInfoDataDelete, CreatedDate = DateTime.Now,
                NesneId = id.ToString()
            };
            var logData = _mapper.Map<Log>(log);

            db.Logs.Add(logData);
            data.Status = data.Status == Stat.Active ? Stat.Passive : Stat.Active;
            db.ContactInfos.Update(data);
            var x = db.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }

        return false;
    }

    public ContactInfoVM ContactInfoGetById(int id)
    {
        var data = db.ContactInfos.Where(x => x.Id == id).FirstOrDefault();
        if (data == null) return null;
        var dataModel = _mapper.Map<ContactInfoVM>(data);
        return dataModel;
    }

    public bool ContactInfoUpdate(ContactInfoVM data, string Username)
    {
        var dataModel = _mapper.Map<ContactInfo>(data);
        dataModel.UpdatedDate = DateTime.Now;

        var log = new LogVM
        {
            Name = Username, Detail = Message.ContactInfoDataUpdate, CreatedDate = DateTime.Now,
            NesneId = data.Id.ToString()
        };
        var logData = _mapper.Map<Log>(log);


        db.ContactInfos.Update(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();

        if (x > 0)
            return true;
        return false;
    }

    public List<ContactInfoVM> ContactInfoWorkGetAll()
    {
        var data = db.ContactInfos.ToList();
        if (data == null) return null;
        var dataModel = _mapper.Map<List<ContactInfoVM>>(data);
        return dataModel;
    }
}