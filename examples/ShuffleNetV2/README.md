# ShuffleNet V2
  
This program is ported by C# from examples/shufflenetv2.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;ShuffleNetV2_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** to output directory; &lt;ShuffleNetV2_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;ShuffleNetV2_dir&gt;. 

## 2. Download demo data

Download test data from the following urls.

- https://github.com/miaow1988/ShuffleNet_V2_pytorch_caffe/releases
  - shufflenet_v2_x0.5.caffemodel
  - shufflenet_v2_x0.5.prototxt

  And convert these files by **caffe2ncnn**

 ````
 $ caffe2ncnn shufflenet_v2_x0.5.prototxt shufflenet_v2_x0.5.caffemodel shufflenet_v2_x0.5.param shufflenet_v2_x0.5.bin
 ````
 After convert, these files will be generated.

 - shufflenet_v2_x0.5.caffemodel
 - shufflenet_v2_x0.5.prototxt

Then copy them to extracted files to &lt;ShuffleNetV2_dir&gt;.
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <ShuffleNetV2_dir> 
dotnet run --configuration Release -- goldfish.jpg

vkEnumerateInstanceExtensionProperties failed -6
397 = 0.24060433
723 = 0.114327215
818 = 0.07658924
````