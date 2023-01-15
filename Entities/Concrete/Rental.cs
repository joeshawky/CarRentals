using Core.Entities;
using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Rental : IEntity
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        //[ForeignKey(nameof(CarId))]
        //public virtual Car Car { get; set; }

        public int CustomerId { get; set; }
        
        //[ForeignKey(nameof(CarId))]
        //public virtual Customer Customer { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; } = null;
    }
}
