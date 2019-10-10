using System;
using System.Collections.Generic;
using System.Text;

namespace Goudkoorts.Models
{
    public enum Orientation
    {
        LEFT_RIGHT, // Horizontal
        TOP_BOTTOM, // Vertical

        BOTTOM_LEFT, // ┐
        BOTTOM_RIGHT, // ┌

        TOP_LEFT, // ┘
        TOP_RIGHT, // └
    }
}
