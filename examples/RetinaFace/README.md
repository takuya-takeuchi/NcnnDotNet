# Retina Face
  
This program is ported by C# from examples/retinaface.cpp.

## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;RetinaFace_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** to output directory; &lt;RetinaFace_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;RetinaFace_dir&gt;. 

## 2. Download demo data

Download test data from the following urls.

- https://github.com/nihui/ncnn-assets/tree/master/models
  - mnet.25-opt.bin
  - mnet.25-opt.param

And extract them and copy to extracted files to &lt;RetinaFace_dir&gt;.
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <RetinaFace_dir> 
dotnet run --configuration Release -- lenna.jpg

[0 GeForce GTX 1080]  queueC=2[8]  queueG=0[16]  queueT=1[1]  buglssc=0
[0 GeForce GTX 1080]  fp16p=1  fp16s=1  fp16a=0  int8s=1  int8a=1
[1 Intel(R) UHD Graphics 630]  queueC=0[1]  queueG=0[1]  queueT=0[1]  buglssc=0
[1 Intel(R) UHD Graphics 630]  fp16p=1  fp16s=1  fp16a=1  int8s=1  int8a=1
0.98291 at 214.15 174.04 164.64 x 228.71
````

![MobileNetSSD](images/image.png "MobileNetSSD")