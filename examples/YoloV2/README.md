# YOLO V2
  
This program is ported by C# from examples/yolov2.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;YoloV2_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** and ***NcnnDotNetNativeDnn.dll*** to output directory; &lt;YoloV2_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;YoloV2_dir&gt;. 

## 2. Download demo data

Download test data from the following urls.

- https://github.com/nihui/ncnn-assets/tree/master/models
  - mobilenet_yolo.bin
  - mobilenet_yolo.param

And extract them and copy to extracted files to &lt;YoloV2_dir&gt;.
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <YoloV2_dir> 
dotnet run --configuration Release -- dog.jpg

vkEnumerateInstanceExtensionProperties failed -6
7 = 0.92694 at 460.66 76.49 239.46 94.77
2 = 0.92689 at 111.79 159.01 453.06 267.01
12 = 0.84539 at 134.51 213.33 185.91 307.00
````

![YOLOv2](images/image.png "YOLOv2")