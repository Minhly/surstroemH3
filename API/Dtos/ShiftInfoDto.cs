using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ShiftInfoDto
    {
        public int Id { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }


        public ShiftInfoDto()
        {

        }

        public ShiftInfoDto(Shift shift)
        {
            Id = shift.Id;
            ShiftStart = shift.ShiftStart;
            ShiftEnd = shift.ShiftEnd;
        }
    }
}
