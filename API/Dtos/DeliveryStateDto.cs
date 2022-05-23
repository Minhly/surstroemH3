using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class DeliveryStateDto
    {
        public int Id { get; set; }
        public string State { get; set; }

        public DeliveryStateDto()
        { 
        
        }

        public DeliveryStateDto(DeliveryState deliveryState)
        {
            Id = deliveryState.Id;
            State = deliveryState.State;
        }
    }
}
