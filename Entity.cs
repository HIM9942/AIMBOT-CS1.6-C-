using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AIM_CS1._60
{
    public class Entity
    {
        public IntPtr Address { get; set; }
        public Vector3 Position { get; set; }

        public float Health { get; set; }
        public int Team { get; set; }
        public float Distance { get; set; }

    }
}
