using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class DeliveryTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public DeliveryTypeDto()
        {

        }

        public DeliveryTypeDto(DeliveryType deliveryType)
        {
            Id = deliveryType.Id;
            Type = deliveryType.Type;
        }
    }
}
