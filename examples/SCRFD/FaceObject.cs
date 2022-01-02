using NcnnDotNet.OpenCV;

namespace SCRFD
{

    internal sealed class FaceObject
    {

        #region Properties

        public Rect<float> Rect
        {
            get;
            set;
        }

        public Point<float>[] Landmark
        {
            get;
            set;
        }

        public float Prob
        {
            get;
            set;
        }

        #endregion

    }

}