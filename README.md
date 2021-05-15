# ![Alt text](nuget/nn48.png "NcnnDotNet") NcnnDotNet [![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)]()

ncnn wrapper written in C++ and C# for Windows, MacOS and Linux

|Package|OS|x86|x64|ARM|ARM64|Nuget|
|---|---|---|---|---|---|---|
|NcnnDotNet (CPU)|Windows|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.svg)](https://www.nuget.org/packages/NcnnDotNet)|
||Linux|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.svg)](https://www.nuget.org/packages/NcnnDotNet)|
||OSX|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.svg)](https://www.nuget.org/packages/NcnnDotNet)|
|NcnnDotNet (GPU)|Windows|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.GPU.svg)](https://www.nuget.org/packages/NcnnDotNet.GPU)|
||Linux|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.GPU.svg)](https://www.nuget.org/packages/NcnnDotNet.GPU)|
||OSX|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.GPU.svg)](https://www.nuget.org/packages/NcnnDotNet.GPU)|
|NcnnDotNet (Xamarin)|UWP|-|-|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.Xamarin.svg)](https://www.nuget.org/packages/NcnnDotNet.Xamarin)|
||Android|✓|✓|✓|✓|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.Xamarin.svg)](https://www.nuget.org/packages/NcnnDotNet.Xamarin)|
||iOS|-|-|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.Xamarin.svg)](https://www.nuget.org/packages/NcnnDotNet.Xamarin)|

## Demo

#### Xamarin.Android

<img src="examples\Xamarin\YoloV3\images\android.webp" />

## Related Projects

- [UltraFaceDotNet](https://github.com/takuya-takeuchi/UltraFaceDotNet)
  - Face detection .NET library uses NcnnDotNet
- [CenterFaceDotNet](https://github.com/takuya-takeuchi/CenterFaceDotNet)
  - Face detection .NET library uses NcnnDotNet
 
## Dependencies Libraries and Products

#### [ncnn](https://github.com/Tencent/ncnn/)

> **License:** The BSD 3-Clause License
>
> **Author:** THL A29. Limited, a Tencent company
> 
> **Principal Use:** A high-performance neural network inference framework optimized for the mobile platform in C++. Main goal of NcnnDotNet is what wraps ncnn by C#.

#### [OpenCV](https://opencv.org/)

> **License:** The BSD 3-Clause License
>
> **Author:** Intel Corporation, Willow Garage, Itseez
> 
> **Principal Use:** Uses to read and show image data.