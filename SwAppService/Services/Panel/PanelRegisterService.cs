using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Messages;
using SwAppService.Services.General;
using SwAppViewModel.General;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IPanelRegisterService
{
    PanelRegisterOpResult PanelRegister(PanelRegisterVM data);
}

public class PanelRegisterService : IPanelRegisterService
{
    private readonly IMapper _mapper;
    private readonly IUtilsService utilsService;
    private readonly AppDbContext db;

    public PanelRegisterService(IMapper mapper, AppDbContext db, IUtilsService utilsService)
    {
        _mapper = mapper;
        this.db = db;
        this.utilsService = utilsService;
    }

    public PanelRegisterOpResult PanelRegister(PanelRegisterVM data)
    {
        if (data.Password != null)
            data.Password = utilsService.EncodePasswordToBase64(data.Password);

        if (db.Users.Where(x => x.UserName == data.UserName).FirstOrDefault() != null)
            return new PanelRegisterOpResult { IsSuccess = false, OpDescription = Message.UserNameControl };

        var dataModel = _mapper.Map<User>(data);

        dataModel.CreatedDate = DateTime.Now;

        var log = new LogVM { Name = data.UserName, Detail = Message.UserCreate, CreatedDate = DateTime.Now };
        utilsService.Logging(log);

        db.Users.Add(dataModel);
        var x = db.SaveChanges();

        if (x > 0)
            return new PanelRegisterOpResult { IsSuccess = true, OpDescription = Message.Success };

        return new PanelRegisterOpResult { IsSuccess = false, OpDescription = Message.Error };
    }
}