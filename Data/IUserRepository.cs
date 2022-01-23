using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Olives.Dtos;
using Olives.Models;

namespace Olives.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByEmail(string email);
        User GetById(int id);
        List<GetInterestDto> AddUserInterest(AddUserInterestDto newUserInterest);
        List<GetInterestDto> GetInterests(User user);
        List<User> FindSuitableFriends(User user);
    }
}