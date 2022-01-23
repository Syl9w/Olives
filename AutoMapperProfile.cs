using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Olives.Dtos;
using Olives.Models;

namespace Olives
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Interest, GetInterestDto>();
            CreateMap<User, GetFriendsDto>();
        }
    }
}