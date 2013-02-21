using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lecture6Examples
{
    public class Bonfire : GameObject, ICatchFire
    {
        public bool IsOnFire { get { return _isOnFire; } }

        private bool _isOnFire;

        public void LightOnFire()
        {
            _isOnFire = true;
        }

        public void Extinguish()
        {
            _isOnFire = false;
        }

        public override string GetState()
        {
            if (_isOnFire)
                return "AAAH FIRE!!";
            return base.GetState();
        }
    }
}
