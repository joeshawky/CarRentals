using Core.Entities;
using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Customer : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;


        public int UserId { get; set; }

        //[ForeignKey(nameof(UserId))]
        //public virtual User User { get; set; }


    }
}
