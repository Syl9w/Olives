using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Olives.Dtos;
using Olives.Models;

namespace Olives.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;
        public UserRepository(UserContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;


        }

        public List<GetInterestDto> AddUserInterest(AddUserInterestDto newUserInterest)
        {
            var user = _context.Users
                .Include(u => u.Interests)
                .FirstOrDefault(u => u.Id == newUserInterest.UserId);

            var interest = _context.Interets.FirstOrDefault(i => i.Name == newUserInterest.InterestName);
            if (interest == null)
            {
                interest = new Interest
                {
                    Name = newUserInterest.InterestName
                };
                _context.Interets.Add(interest);
                _context.SaveChanges();
            }
            interest = _context.Interets.FirstOrDefault(i => i.Name == newUserInterest.InterestName);
            user.Interests.Add(interest);
            _context.SaveChanges();
            var response = user.Interests.Select(i => _mapper.Map<GetInterestDto>(i)).ToList();
            return response;
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();

            return user;
        }

        public List<User> FindSuitableFriends(User user)
        {
            var users = _context.Users
                .Include(u => u.Interests)
                .Where(u => u.Id != user.Id).ToList();

            var res = new List<User>();
            var initialUserInterests = GetInterests(user);
            foreach(var pu in users){
                var ipu = pu.Interests.ToList().Where(interest => interest.Users.Contains(user)).Count();
                if(ipu>=3)
                    res.Add(pu);
            }
            return res.ToList();
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<GetInterestDto> GetInterests(User user)
        {
            var resUser = _context.Users
                .Include(u => u.Interests)
                .FirstOrDefault(u => u.Id == user.Id)
                .Interests.Select(i => _mapper.Map<GetInterestDto>(i))
                .ToList();
            return resUser;
        }

    }
}