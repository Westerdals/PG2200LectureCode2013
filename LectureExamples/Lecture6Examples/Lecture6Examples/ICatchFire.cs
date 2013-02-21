using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lecture6Examples
{
    interface ICatchFire
    {
        void LightOnFire();
        void Extinguish();
        bool IsOnFire { get; }
    }
}
