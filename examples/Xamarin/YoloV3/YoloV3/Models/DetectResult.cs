using System.Collections.Generic;

namespace YoloV3.Models
{

    public sealed class DetectResult
    {

        #region Constructors

        public DetectResult(int width, int height, IEnumerable<Object> boxes)
        {
            this.Width = width;
            this.Height = height;
            this.Boxes = new List<Object>(boxes);
        }

        #endregion

        #region Properties

        public IReadOnlyCollection<Object> Boxes
        {
            get;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        #endregion

    }

}