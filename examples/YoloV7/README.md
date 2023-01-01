# YOLO V7
  
This program is ported by C# from examples/yolov7.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;YoloV7_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** to output directory; &lt;YoloV7_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;YoloV7_dir&gt;. 

## 2. Download demo data

Download test data from the following urls.

- https://github.com/nihui/ncnn-assets/tree/master/models
  - yolov7-tiny.param
  - yolov7-tiny.bin

And extract them and copy to extracted files to &lt;YoloV7_dir&gt;.
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <YoloV7_dir> 
dotnet run --configuration Release -- dog.jpg

[0 GeForce GTX 1080]  queueC=2[8]  queueG=0[16]  queueT=1[1]  buglssc=0
[0 GeForce GTX 1080]  fp16p=1  fp16s=1  fp16a=0  int8s=1  int8a=1
[1 Intel(R) UHD Graphics 630]  queueC=0[1]  queueG=0[1]  queueT=0[1]  buglssc=0
[1 Intel(R) UHD Graphics 630]  fp16p=1  fp16s=1  fp16a=1  int8s=1  int8a=1
16 = 0.90858 at 132.30 219.57 178.94 321.64
1 = 0.78291 at 123.57 120.72 440.54 293.66
7 = 0.72089 at 466.41 75.30 218.18 96.89
````

![YOLOv7](images/image.png "YOLOv7")