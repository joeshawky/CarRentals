using Core.Entities;
using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Car : IEntity
    {
        public int Id { get; set; }

        public int BrandId { get; set; }


        //[ForeignKey(nameof(BrandId))]
        //public virtual Brand Brand { get; set; }

        public int ColorId { get; set; }


        //[ForeignKey(nameof(ColorId))]
        //[NotMapped]
        //public virtual Color Color { get; set; }
        public int ModelYear { get; set; }

        public int DailyPrice{ get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
