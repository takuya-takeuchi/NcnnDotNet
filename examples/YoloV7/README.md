# YOLO V5
  
This program is ported by C# from examples/yolov5.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;YoloV5_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** to output directory; &lt;YoloV5_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;YoloV5_dir&gt;. 

## 2. Download demo data

Download test data from the following urls.

- https://github.com/nihui/ncnn-assets/tree/master/models
  - yolov5s_6.0.param
  - yolov5s_6.0.bin
  - yolov5s.param
  - yolov5s.bin

And extract them and copy to extracted files to &lt;YoloV5_dir&gt;.
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <YoloV5_dir> 
dotnet run --configuration Release -- dog.jpg

[0 GeForce GTX 1080]  queueC=2[8]  queueG=0[16]  queueT=1[1]  buglssc=0
[0 GeForce GTX 1080]  fp16p=1  fp16s=1  fp16a=0  int8s=1  int8a=1
[1 Intel(R) UHD Graphics 630]  queueC=0[1]  queueG=0[1]  queueT=0[1]  buglssc=0
[1 Intel(R) UHD Graphics 630]  fp16p=1  fp16s=1  fp16a=1  int8s=1  int8a=1
7 = 0.98014 at 458.34 82.56 224.54 86.26
12 = 0.97378 at 127.00 230.88 196.63 294.23
2 = 0.48931 at 153.65 110.83 423.08 351.21
````

![YOLOv3](images/image.png "YOLOv3")