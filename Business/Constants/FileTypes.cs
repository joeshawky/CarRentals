using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class FileTypes
    {
        public static Dictionary<string, string> Types = new Dictionary<string, string>
        {
            { "image/png", "png" },
            { "image/jpeg", "jpg" },
            { "image/jpg", "jpg" }
        };
    }
}
