# SqueezeNet
  
This program is ported by C# from examples/squeezenet.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;SqueezeNet_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** to output directory; &lt;SqueezeNet_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;SqueezeNet_dir&gt;. 

## 2. Download demo data

Download test data from the following urls.

- https://github.com/nihui/ncnn-assets/tree/master/models
  - squeezenet_v1.1.bin
  - squeezenet_v1.1.param

And extract them and copy to extracted files to &lt;SqueezeNet_dir&gt;.
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <SqueezeNet_dir> 
dotnet run --configuration Release -- goldfish.jpg

[0 GeForce GTX 1080]  queueC=2[8]  queueG=0[16]  queueT=1[1]  buglssc=0
[0 GeForce GTX 1080]  fp16p=1  fp16s=1  fp16a=0  int8s=1  int8a=1
[1 Intel(R) UHD Graphics 630]  queueC=0[1]  queueG=0[1]  queueT=0[1]  buglssc=0
[1 Intel(R) UHD Graphics 630]  fp16p=1  fp16s=1  fp16a=1  int8s=1  int8a=1
1 = 0.9998976
107 = 3.3139066E-05
950 = 1.42037325E-05
````