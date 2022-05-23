using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public UserDto User { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public AddressDto PayingAddress { get; set; }
        public DeliveryStateDto DeliveryState { get; set; }
        public DeliveryTypeDto DeliveryType { get; set; }

        public OrderDto()
        {

        }

        public OrderDto(Order order)
        {
            Id = order.Id;
            User = new UserDto(order.User);
            ShippingAddress = new AddressDto(order.ShippingAddress);
            PayingAddress = new AddressDto(order.PayingAddress);
            DeliveryState = new DeliveryStateDto(order.DeliveryState);
            DeliveryType = new DeliveryTypeDto(order.DeliveryType);
        }
    }
}
