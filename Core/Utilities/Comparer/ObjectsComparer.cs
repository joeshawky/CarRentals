using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Comparer
{
    public static class ObjectsComparer
    {
        public static bool CompareByValues(object objOne, object objTwo)
        {
            string objectOne = JsonConvert.SerializeObject(objOne);
            string objectTwo = JsonConvert.SerializeObject(objTwo);

            return objectOne == objectTwo;
        }

    }
}
