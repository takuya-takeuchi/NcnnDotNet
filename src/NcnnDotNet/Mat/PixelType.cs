// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public enum PixelType : uint
    {

        ConvertShift = 16,

        FormatMask = 0x0000ffff,

        ConvertMask = 0xffff0000,

        Rgb = 1,
        Bgr = 2,
        Gray = 3,
        Rgba = 4,

        Rgb2Bgr  = Rgb | (Bgr  << (int)ConvertShift),
        Rgb2Gray = Rgb | (Gray << (int)ConvertShift),
        Rgb2Rgba = Rgb | (Rgba << (int)ConvertShift),

        Bgr2Rgb =  Bgr | (Rgb  << (int)ConvertShift),
        Bgr2Gray = Bgr | (Gray << (int)ConvertShift),
        Bgr2Rgba = Bgr | (Rgba << (int)ConvertShift),

        Gray2Rgb  = Gray | (Rgb  << (int)ConvertShift),
        Gray2Bgr  = Gray | (Bgr  << (int)ConvertShift),
        Gray2Rgba = Gray | (Rgba << (int)ConvertShift),

        Rgba2Rgb  = Rgba | (Rgb  << (int)ConvertShift),
        Rgba2Bgr  = Rgba | (Bgr  << (int)ConvertShift),
        Rgba2Gray = Rgba | (Gray << (int)ConvertShift),

    }

}