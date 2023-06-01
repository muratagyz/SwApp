using AutoMapper;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppService.Services.General;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IProfileService
{
    UserVM GetUser(string userName);
}

public class ProfileService : IProfileService
{
    private readonly IMapper _mapper;
    private readonly IUtilsService utilsService;
    private readonly AppDbContext db;

    public ProfileService(IMapper mapper, AppDbContext db, IUtilsService utilsService)
    {
        _mapper = mapper;
        this.db = db;
        this.utilsService = utilsService;
    }

    public UserVM GetUser(string userName)
    {
        var user = db.Users.Where(x => x.UserName == userName && x.Status == Stat.Active).FirstOrDefault();
        var dataModel = _mapper.Map<UserVM>(user);
        return dataModel;
    }
}