
using System.Net;

namespace Public.Dtos.UserManagement;

public class ResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public TokenInfoDto TokenInfo { get; set; }
}