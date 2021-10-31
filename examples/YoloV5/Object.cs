using NcnnDotNet.OpenCV;

namespace YoloV5
{

    internal sealed class Object
    {

        #region Constructors

        public Object()
        {
            this.Rect = new Rect<float>();
        }

        #endregion

        #region Properties

        public Rect<float> Rect
        {
            get;
            set;
        }

        public int Label
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