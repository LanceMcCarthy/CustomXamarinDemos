using System;

namespace RenderImage.Portable.Controls.HingeService
{
    public class HingeEventArgs : EventArgs
    {
        public HingeEventArgs(int angle)
            : base()
        {
            Angle = angle;
        }

        public int Angle { get; private set; }
    }
}