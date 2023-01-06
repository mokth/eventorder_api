using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDemoAPI.Model
{
    public class GeneralResult<T>
    {
        public string ok { get; set; }
        public string error { get; set; }
        public T result { get; set; }
    }
}
