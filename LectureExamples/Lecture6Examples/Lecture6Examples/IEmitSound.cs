using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Lecture6Examples
{
    interface IEmitSound
    {
        AudioEmitter Emitter { get; }
        void PlaySounds(AudioListener listener);

    }
}
