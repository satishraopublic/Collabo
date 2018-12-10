using System;
using System.Collections.Generic;

public interface  ICollaboRepository{
//GROUP
    Guid CreateGroup(GroupCreateDTO group);
    List<GroupViewDTO> ShowAllGroups();

//USER
    UserViewDTO Register(string firstName, string lastName, string userName, string password, string confirmPassword, string emailId, string question, string answer);

//SESSION
    Session Login(string userName, string password);
    bool Logout(Guid session);

//Message
    List<Message> GetAllMessages(Guid session);
    Guid SendMessage(Guid session, Guid sendTo, MessageCreateDTO message);

}