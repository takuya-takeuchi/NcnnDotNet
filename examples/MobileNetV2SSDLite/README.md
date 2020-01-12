# MobileNet V2 SSD Lite
  
This program is ported by C# from examples/mobilenetv2ssdlite.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;MobileNetV2SSDLite_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** to output directory; &lt;MobileNetV2SSDLite_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;MobileNetV2SSDLite_dir&gt;. 

## 2. Download demo data

Download test data from the following urls.

- https://github.com/nihui/ncnn-assets/tree/master/models
  - mobilenetv2_ssdlite_voc.bin
  - mobilenetv2_ssdlite_voc.param

And extract them and copy to extracted files to &lt;MobileNetV2SSDLite_dir&gt;.
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <MobileNetV2SSDLite_dir> 
dotnet run --configuration Release -- dog.jpg

[0 GeForce GTX 1080]  queueC=2[8]  queueG=0[16]  queueT=1[1]  buglssc=0
[0 GeForce GTX 1080]  fp16p=1  fp16s=1  fp16a=0  int8s=1  int8a=1
[1 Intel(R) UHD Graphics 630]  queueC=0[1]  queueG=0[1]  queueT=0[1]  buglssc=0
[1 Intel(R) UHD Graphics 630]  fp16p=1  fp16s=1  fp16a=1  int8s=1  int8a=1
2 = 0.96775 at 130.60 118.44 436.14 315.11
7 = 0.83982 at 465.92 83.44 235.06 94.72
12 = 0.83866 at 128.00 212.55 185.20 328.66
8 = 0.47114 at 128.00 212.55 185.20 328.66
````

![MobileNetV2SSDLite](images/image.png "MobileNetV2SSDLite")