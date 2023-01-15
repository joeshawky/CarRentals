using Core.Entities;
using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos
{
    public class CarDto : IDto
    {
        public int CarId { get; set; }
        public string CarName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public string ColorName { get; set; } = string.Empty;
        public int DailyPrice { get; set; }
        public List<string>? ImagesUrls { get; set; }

    }

}
