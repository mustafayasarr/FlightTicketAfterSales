using FlightTicket.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicket.Test.MockData
{
    public static class PassengerMockData
    {
        public static PassengerEntity Passenger()
        {
            return new PassengerEntity
            {
                Id = Guid.Parse("4e450608-ed61-4725-a192-84e0edd8b0b5"),
                BirthDate = new DateTime(1993, 11, 17),
                FirstName = "Behçet",
                LastName = "Necatigil",
                CreationDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };
        }
        public static PassengerEntity VoidPassenger()
        {
            return new PassengerEntity
            {
                Id = Guid.Parse("3c16c2da-d21a-44b0-ad75-b40a13dff7cd"),
                BirthDate = new DateTime(1996, 11, 17),
                FirstName = "Ali",
                LastName = "Lorem",
                CreationDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };
        }
        public static PassengerEntity ReissuePassenger()
        {
            return new PassengerEntity
            {
                Id = Guid.Parse("170d1532-26e1-4a1f-911c-6edfaf2b7a50"),
                BirthDate = new DateTime(1994, 05, 17),
                FirstName = "Ayşe",
                LastName = "Ipsum",
                CreationDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };
        }
    }
}
