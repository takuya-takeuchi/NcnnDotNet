# Faster RCNN
  
This program is ported by C# from examples/fasterrcnn.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;FasterRCNN_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** to output directory; &lt;FasterRCNN_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;FasterRCNN_dir&gt;. 

## 2. Download demo data

Download test data from the following urls.

- https://github.com/nihui/ncnn-assets/tree/master/models
  - ZF_faster_rcnn_final.bin.z01
  - ZF_faster_rcnn_final.bin.z02
  - ZF_faster_rcnn_final.bin.zip
  - ZF_faster_rcnn_final.param

And extract them and copy to extracted files to &lt;FasterRCNN_dir&gt;.
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <FasterRCNN_dir> 
dotnet run --configuration Release -- dog.jpg

vkEnumerateInstanceExtensionProperties failed -6
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
7 = 0.94920 at 447.28 72.53 251.93 96.47
12 = 0.93823 at 140.84 184.89 172.13 347.51
````

![MobileNetV3SSDLite](images/image.png "MobileNetV3SSDLite")