# MobileNet SSD
  
This program is ported by C# from examples/mobilenetssd.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;MobileNetSSD_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** to output directory; &lt;MobileNetSSD_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;MobileNetSSD_dir&gt;. 

## 2. Download demo data

Download test data from the following urls.

- https://github.com/nihui/ncnn-assets/tree/master/models
  - mobilenet_ssd_voc_ncnn.bin
  - mobilenet_ssd_voc_ncnn.param

And extract them and copy to extracted files to &lt;MobileNetSSD_dir&gt;.
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <MobileNetSSD_dir> 
dotnet run --configuration Release -- dog.jpg

[0 GeForce GTX 1080]  queueC=2[8]  queueG=0[16]  queueT=1[1]  buglssc=0
[0 GeForce GTX 1080]  fp16p=1  fp16s=1  fp16a=0  int8s=1  int8a=1
[1 Intel(R) UHD Graphics 630]  queueC=0[1]  queueG=0[1]  queueT=0[1]  buglssc=0
[1 Intel(R) UHD Graphics 630]  fp16p=1  fp16s=1  fp16a=1  int8s=1  int8a=1
12 = 0.99775 at 137.99 209.13 186.70 332.18
7 = 0.99636 at 465.98 72.85 222.67 98.30
2 = 0.99473 at 106.52 141.06 467.29 273.91
````

![MobileNetSSD](images/image.png "MobileNetSSD")