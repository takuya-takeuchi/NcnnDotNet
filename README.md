![NcnnDotNet](https://socialify.git.ci/takuya-takeuchi/NcnnDotNet/image?description=1&font=Raleway&language=1&logo=https%3A%2F%2Fgithub.com%2Ftakuya-takeuchi%2FNcnnDotNet%2Fraw%2Fmaster%2Fnuget%2Fnn128.png&owner=1&pattern=Circuit%20Board&theme=Light)

[![GitHub Stars](https://img.shields.io/github/stars/takuya-takeuchi/ncnndotnet?label=STARS&color=%23DFB317&style=for-the-badge)]() [![GitHub Forks](https://img.shields.io/github/forks/takuya-takeuchi/ncnndotnet?label=FORKS&color=%236ECA00&style=for-the-badge)]() [![GitHub Issues](https://img.shields.io/github/issues/takuya-takeuchi/ncnndotnet?label=ISSUES&color=%23007EC6&style=for-the-badge)]() [![GitHub Lisence](https://img.shields.io/github/license/takuya-takeuchi/ncnndotnet?label=LISENCE&color=%239109BD&style=for-the-badge)]()

|Package|OS|x86|x64|ARM|ARM64|Nuget|
|---|---|---|---|---|---|---|
|NcnnDotNet (CPU)|Windows|✓|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.svg)](https://www.nuget.org/packages/NcnnDotNet)|
||Linux|-|✓|-|✓|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.svg)](https://www.nuget.org/packages/NcnnDotNet)|
||OSX|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.svg)](https://www.nuget.org/packages/NcnnDotNet)|
|NcnnDotNet (GPU)|Windows|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.GPU.svg)](https://www.nuget.org/packages/NcnnDotNet.GPU)|
||Linux|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.GPU.svg)](https://www.nuget.org/packages/NcnnDotNet.GPU)|
||OSX|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.GPU.svg)](https://www.nuget.org/packages/NcnnDotNet.GPU)|
|NcnnDotNet (Xamarin)|UWP|✓|✓|✓|✓|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.Xamarin.svg)](https://www.nuget.org/packages/NcnnDotNet.Xamarin)|
||Android|✓|✓|✓|✓|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.Xamarin.svg)](https://www.nuget.org/packages/NcnnDotNet.Xamarin)|
||iOS|-|✓|-|✓|[![NuGet version](https://img.shields.io/nuget/v/NcnnDotNet.Xamarin.svg)](https://www.nuget.org/packages/NcnnDotNet.Xamarin)|

## Demo

#### YoloV3 on Xamarin.Android, iOS and UWP

<img src="examples\Xamarin\YoloV3\images\android.webp" width="150" /> <img src="examples\Xamarin\YoloV3\images\ios.webp" width="150" /> <img src="examples\Xamarin\YoloV3\images\uwp.webp" width="300" />

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