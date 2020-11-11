using _8gyak_QYE8OW.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8gyak_QYE8OW.entities
{
    public class PresentFactory : IToyFactory
    {
        public Color Ribbon { get; set; }
        public Color Box { get; set; }
        public Toy CreateNew()
        {
            return new Present(Ribbon, Box);
        }
    }
}
