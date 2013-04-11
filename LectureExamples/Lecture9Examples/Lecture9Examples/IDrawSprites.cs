using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LectureExamples5;

namespace Lecture7Examples
{
    public interface IDrawSprites
    {
        void AddDrawable(DrawData drawable);
        void RemoveDrawable(DrawData drawable);
    }
}
