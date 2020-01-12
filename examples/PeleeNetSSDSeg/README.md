# PeleeNet SSD Seg
  
This program is ported by C# from examples/peleenetssd_seg.cpp.

## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;PeleeNetSSDSeg_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** to output directory; &lt;PeleeNetSSDSeg_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;PeleeNetSSDSeg_dir&gt;. 

## 2. Download demo data

Download test data from the following urls.

- https://github.com/nihui/ncnn-assets/tree/master/models
  - pelee.bin
  - pelee.param

And extract them and copy to extracted files to &lt;PeleeNetSSDSeg_dir&gt;.
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <PeleeNetSSDSeg_dir> 
dotnet run --configuration Release -- car.jpg

[0 GeForce GTX 1080]  queueC=2[8]  queueG=0[16]  queueT=1[1]  buglssc=0
[0 GeForce GTX 1080]  fp16p=1  fp16s=1  fp16a=0  int8s=1  int8a=1
[1 Intel(R) UHD Graphics 630]  queueC=0[1]  queueG=0[1]  queueT=0[1]  buglssc=0
[1 Intel(R) UHD Graphics 630]  fp16p=1  fp16s=1  fp16a=1  int8s=1  int8a=1
3 = 0.99860 at 85.66 117.31 183.82 119.78
````

![MobileNetSSD](images/image.png "MobileNetSSD")