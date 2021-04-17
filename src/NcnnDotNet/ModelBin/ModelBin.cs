// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public abstract class ModelBin : NcnnObject
    {

        #region Methods

        public virtual Mat Load(int w, int type)
        {
            return null;
        }

        public virtual Mat Load(int w, int h, int type)
        {
            return null;
        }

        public virtual Mat Load(int w, int h, int c, int type)
        {
            return null;
        }

        #endregion

    }

}