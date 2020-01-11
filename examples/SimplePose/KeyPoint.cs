using NcnnDotNet.OpenCV;

namespace SimplePose
{

    internal sealed class KeyPoint
    {

        #region Constructors

        public KeyPoint()
        {
            this.P = new Point<float>();
        }

        #endregion

        #region Properties

        public Point<float> P
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