using System.Linq;
using NcnnDotNet.OpenCV;

namespace RetinaFace
{

    internal sealed class FaceObject
    {

        #region Constructors

        public FaceObject()
        {
            this.Rect = new Rect<float>();
            this.Landmark = Enumerable.Range(0, 5).Select(_ => new Point<float>()).ToArray();
        }

        #endregion

        #region Properties

        public Rect<float> Rect
        {
            get;
            set;
        }

        public Point<float>[] Landmark
        {
            get;
        }

        public float Prob
        {
            get;
            set;
        }

        #endregion

    }

}