using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olives.Dtos
{
    public class AddUserInterestDto
    {
        public int UserId { get; set; }
        public string InterestName { get; set; }
    }
}