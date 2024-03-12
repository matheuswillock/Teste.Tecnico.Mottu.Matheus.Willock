using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal RaceValue { get; set; }
        public bool OrderIsDelivered { get; set; } = false;

        public Order()
        {

        }

        public Order(decimal raceValue)
        {
            RaceValue = raceValue;
        }
    }
}
