using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Persistence.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation.Domain.Entities.Reservation>, IReservationRepository
    {
        public ReservationRepository(ReservationDbContext context) : base(context)
        {

        }
    }
}
