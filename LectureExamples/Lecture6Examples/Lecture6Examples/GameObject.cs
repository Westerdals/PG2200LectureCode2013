using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Lecture6Examples
{
    public class GameObject
    {
        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual string GetState()
        {
            return "Im all good!";
        }
    }
}
