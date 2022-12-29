# YOLOX
  
This program is ported by C# from examples/yolox.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;YoloX_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** to output directory; &lt;YoloX_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;YoloX_dir&gt;. 

## 2. Download demo data

Download test data from the following urls.

- https://github.com/nihui/ncnn-assets/tree/master/models
  - yolox.param
  - yolox.bin

And extract them and copy to extracted files to &lt;YoloX_dir&gt;.
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <YoloX_dir> 
dotnet run --configuration Release -- dog.jpg

[0 GeForce GTX 1080]  queueC=2[8]  queueG=0[16]  queueT=1[1]  buglssc=0
[0 GeForce GTX 1080]  fp16p=1  fp16s=1  fp16a=0  int8s=1  int8a=1
[1 Intel(R) UHD Graphics 630]  queueC=0[1]  queueG=0[1]  queueT=0[1]  buglssc=0
[1 Intel(R) UHD Graphics 630]  fp16p=1  fp16s=1  fp16a=1  int8s=1  int8a=1
1 = 0.95030 at 124.93 119.17 435.19 302.63
16 = 0.91241 at 134.08 222.57 176.22 325.38
7 = 0.61818 at 463.03 76.70 231.18 95.06
58 = 0.41572 at 684.37 110.83 31.97 43.11
````

![YOLOv7](images/image.png "YOLOv7")