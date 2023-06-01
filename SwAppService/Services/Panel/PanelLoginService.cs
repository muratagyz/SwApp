using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppData.Messages;
using SwAppService.Services.General;
using SwAppViewModel.General;
using SwAppViewModel.Panel;

namespace SwAppService.Services.Panel;

public interface IPanelLoginService
{
    PanelLoginResult PanelLogin(PanelLoginVM data);
    public string PanelLoginGetFullName(string userName);
}

public class PanelLoginService : IPanelLoginService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IUtilsService utilsService;
    private readonly AppDbContext db;

    public PanelLoginService(IMapper mapper, AppDbContext db, IConfiguration configuration, IUtilsService utilsService)
    {
        _mapper = mapper;
        this.db = db;
        _configuration = configuration;
        this.utilsService = utilsService;
    }

    public PanelLoginResult PanelLogin(PanelLoginVM data)
    {
        var user = db.Users.Where(x =>
            x.UserName == data.UserName && x.Password == utilsService.EncodePasswordToBase64(data.Password) &&
            x.Status == Stat.Active).FirstOrDefault();
        if (user != null)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Role, user.Role.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = GetToken(authClaims);

            var dataModel = _mapper.Map<User>(user);
            dataModel.Token = token;

            return new PanelLoginOpSuccessResult
            {
                FirstName = dataModel.FirstName, LastName = dataModel.LastName, UserName = dataModel.UserName,
                Token = dataModel.Token, IsSuccess = true, OpDescription = Message.Success, Role = dataModel.Role
            };
        }

        return new PanelLoginOpErrorResult { IsSuccess = false, OpDescription = Message.Error };
    }

    public string PanelLoginGetFullName(string userName)
    {
        var user = db.Users.Where(x => x.UserName == userName && x.Status == Stat.Active).FirstOrDefault();
        return user.FirstName + " " + user.LastName;
    }

    private string GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            _configuration["JWT:ValidIssuer"],
            _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}