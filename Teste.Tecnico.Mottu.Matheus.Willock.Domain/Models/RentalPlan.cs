using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models
{
    public class RentalPlan
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MotorcycleId { get; set; }
        public decimal RentValue { get; set; }
        public PlansDay Days { get; private set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }

        public RentalPlan()
        {

        }

        public RentalPlan(PlansDay days, DateTime startDay, DateTime endDay, decimal rentValue)
        {
            StartDay = startDay;
            EndDay = endDay;
            Days = days;
            RentValue = rentValue;
        }
    }
}
