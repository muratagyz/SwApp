using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IContactInformationService
{
    List<ContactInformationVM> ContactInformationGetAll();
    ContactInformationVM ContactInformationGetById(int id);
    bool ContactInformationAdd(ContactInformationVM data, string Username);
    bool ContactInformationDelete(int id, string Username);
    bool ContactInformationUpdate(ContactInformationVM data, string Username);
}

public class ContactInformationService : IContactInformationService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public ContactInformationService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public bool ContactInformationAdd(ContactInformationVM data, string Username)
    {
        var dataModel = _mapper.Map<ContactInformation>(data);
        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM
            { Name = Username, Detail = Message.ContactInformationDataCreate, CreatedDate = DateTime.Now };
        var logData = _mapper.Map<Log>(log);

        db.ContactInformations.Add(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();
        if (x > 0)
            return true;
        return false;
    }

    public bool ContactInformationDelete(int id, string Username)
    {
        var data = db.ContactInformations.Where(x => x.Id == id).FirstOrDefault();
        if (data != null)
        {
            data.UpdatedDate = DateTime.Now;
            var log = new LogVM
            {
                Name = Username, Detail = Message.ContactInformationlDataUpdate, CreatedDate = DateTime.Now,
                NesneId = id.ToString()
            };
            var logData = _mapper.Map<Log>(log);

            db.Logs.Add(logData);
            data.Status = data.Status == Stat.Active ? Stat.Passive : Stat.Active;
            db.ContactInformations.Update(data);
            var x = db.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }

        return false;
    }

    public List<ContactInformationVM> ContactInformationGetAll()
    {
        var data = db.ContactInformations.ToList();
        if (data == null) return null;
        var dataModel = _mapper.Map<List<ContactInformationVM>>(data);
        return dataModel;
    }

    public ContactInformationVM ContactInformationGetById(int id)
    {
        var data = db.ContactInformations.Where(x => x.Id == id).FirstOrDefault();
        if (data == null) return null;
        var dataModel = _mapper.Map<ContactInformationVM>(data);
        return dataModel;
    }

    public bool ContactInformationUpdate(ContactInformationVM data, string Username)
    {
        var dataModel = _mapper.Map<ContactInformation>(data);
        dataModel.UpdatedDate = DateTime.Now;

        var log = new LogVM
        {
            Name = Username, Detail = Message.ContactInformationlDataUpdate, CreatedDate = DateTime.Now,
            NesneId = data.Id.ToString()
        };
        var logData = _mapper.Map<Log>(log);


        db.ContactInformations.Update(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();

        if (x > 0)
            return true;
        return false;
    }
}