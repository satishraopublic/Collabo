using System;
using Collabo.Common;

namespace Collabo.API.Services{
public interface ILoginService{
    void AddUserContext(Session uinfo);
    Session ValidateUserContext(Guid token);
    void RemoveUserContext(Session uinfo);
}
}