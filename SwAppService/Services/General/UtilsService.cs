using System.Text;
using AutoMapper;
using SwAppData.Entity;
using SwAppData.EntityFramework;
using SwAppViewModel.Panel;

namespace SwAppService.Services.General;

public interface IUtilsService
{
    string EncodePasswordToBase64(string password);
    string DecodePasswordToBase64(string data);
    bool Logging(LogVM data);
}

public class UtilsService : IUtilsService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext db;

    public UtilsService(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        this.db = db;
    }

    public string DecodePasswordToBase64(string data)
    {
        var encoder = new UTF8Encoding();
        var utf8Decode = encoder.GetDecoder();
        var todecode_byte = Convert.FromBase64String(data);
        var charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        var decoded_char = new char[charCount];
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        var result = new string(decoded_char);
        return result;
    }

    public string EncodePasswordToBase64(string password)
    {
        try
        {
            var encData_byte = new byte[password.Length];
            encData_byte = Encoding.UTF8.GetBytes(password);
            var encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }

    public bool Logging(LogVM data)
    {
        try
        {
            var log = _mapper.Map<Log>(data);
            db.Logs.Add(log);

            var x = db.SaveChanges();

            if (x > 0)
                return true;

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}