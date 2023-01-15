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
    public class CarImage : IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        //[ForeignKey(nameof(CarId))]
        //public virtual Car Car { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
