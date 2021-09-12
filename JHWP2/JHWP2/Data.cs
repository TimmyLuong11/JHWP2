using System;
using System.Collections.Generic;
using System.Text;

namespace JHWP2
{
    class Data
    {
        public string State { get; set; }
        public string Gender { get; set; }
        public double Mean { get; set; }
        public int N { get; set; }

        /// <summary>
        /// Default constructor setting each value to an empty string or 0
        /// </summary>
        public Data()
        {
            State = string.Empty;
            Gender = string.Empty;
            Mean = 0;
            N = 0;
        }
    }
}
