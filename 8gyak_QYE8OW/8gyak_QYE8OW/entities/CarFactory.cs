using _8gyak_QYE8OW.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8gyak_QYE8OW.entities
{
    public class CarFactory: IToyFactory
    {  
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}
