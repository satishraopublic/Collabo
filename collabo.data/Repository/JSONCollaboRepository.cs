using System;
using System.Collections.Generic;
using System.Linq;

public class JSONCollaboRepository : ICollaboRepository
{

    CollaboDB _db;
    JSONDataFileService<CollaboDB> _dataFileService;

    public JSONCollaboRepository(JSONDataFileService<CollaboDB> dataFileService)
    {
        _dataFileService = dataFileService;
        _db = _dataFileService.GetDB();
    }
    public UserViewDTO Register(string firstName, string lastName, string userName, string password, string confirmPassword, string emailId, string question, string answer)
    {
        Guid newUserId;
        User newUser;
        if(!IsUserAlreadyExists(emailId)){
            newUserId = Guid.NewGuid();
            newUser = new User(){
                 ID = newUserId,
                 FirstName = firstName,
                 LastName = lastName,
                 Password = password,
                 EmailId = emailId,
                 SecretQuestion = question,
                 SecretAnswer = answer
            };
            _db.Users.Add(newUser);    
            _dataFileService.SaveDB(_db);
            return new UserViewDTO(newUser);
        }       
        return null;
    }

    public Guid CreateGroup(GroupCreateDTO group)
    {
        Guid newGuid = Guid.NewGuid();
        DateTime currentTime = DateTime.UtcNow;
        _db.Groups.Add(new Group(){
            ID = newGuid, 
            Name = group.Name,
            CreatedOn = currentTime,
            ModifiedOn = currentTime
        });
        _dataFileService.SaveDB(_db);
        return newGuid;
    }
    public List<GroupViewDTO> ShowAllGroups()
    {
        List<GroupViewDTO> groups = new List<GroupViewDTO>();
        if(_db?.Groups?.Any()==true){
            foreach(var group in _db?.Groups){
                groups.Add(new GroupViewDTO(){
                     Name = group.Name,
                     CreatedOn = group.CreatedOn,
                     ModifiedOn = group.ModifiedOn
                });
            }
        }
        return groups;
    }

    public List<Message> GetAllMessages(Guid session)
    {
        throw new NotImplementedException();
    }

    public Session Login(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public bool Logout(Guid session)
    {
        throw new NotImplementedException();
    }


    public Guid SendMessage(Guid session, Guid sendTo, MessageCreateDTO message)
    {
        throw new NotImplementedException();
    }

    private bool IsUserAlreadyExists(string emailId){
        return (_db.Users.Any(c=>string.Equals(c.EmailId, emailId, StringComparison.InvariantCultureIgnoreCase)) == true);
    }

}