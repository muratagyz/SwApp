using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppData.Messages;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IPersonalService
{
    List<PersonalVM> PersonalGetAll();
    PersonalVM PersonalGetById(int id);
    bool PersonalAdd(PersonalVM data, string Username);
    bool PersonalDelete(int id, string Username);
    bool PersonalUpdate(PersonalVM data, string Username);
}

public class PersonalService : IPersonalService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public PersonalService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public bool PersonalAdd(PersonalVM data, string Username)
    {
        var dataModel = _mapper.Map<Personal>(data);
        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM { Name = Username, Detail = Message.PersonelDataCreate, CreatedDate = DateTime.Now };
        var logData = _mapper.Map<Log>(log);

        db.Personals.Add(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();
        if (x > 0)
            return true;
        return false;
    }

    public bool PersonalDelete(int id, string Username)
    {
        var data = db.Personals.Where(x => x.Id == id).FirstOrDefault();
        if (data != null)
        {
            data.UpdatedDate = DateTime.Now;
            var log = new LogVM
            {
                Name = Username, Detail = Message.PersonelDataDelete, CreatedDate = DateTime.Now,
                NesneId = id.ToString()
            };
            var logData = _mapper.Map<Log>(log);

            db.Logs.Add(logData);
            data.Status = data.Status == Stat.Active ? Stat.Passive : Stat.Active;
            db.Personals.Update(data);
            var x = db.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }

        return false;
    }

    public List<PersonalVM> PersonalGetAll()
    {
        var data = db.Personals.ToList();
        if (data == null) return null;
        var dataModel = _mapper.Map<List<PersonalVM>>(data);
        return dataModel;
    }

    public PersonalVM PersonalGetById(int id)
    {
        var data = db.Personals.Where(x => x.Id == id).FirstOrDefault();
        if (data == null) return null;
        var dataModel = _mapper.Map<PersonalVM>(data);
        return dataModel;
    }

    public bool PersonalUpdate(PersonalVM data, string Username)
    {
        var dataModel = _mapper.Map<Personal>(data);
        dataModel.UpdatedDate = DateTime.Now;

        var log = new LogVM
        {
            Name = Username, Detail = Message.PersonelDataUpdate, CreatedDate = DateTime.Now,
            NesneId = data.Id.ToString()
        };
        var logData = _mapper.Map<Log>(log);


        db.Personals.Update(dataModel);
        db.Logs.Add(logData);

        var x = db.SaveChanges();

        if (x > 0)
            return true;
        return false;
    }
}