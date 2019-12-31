# SqueezeNet
  
This program is ported by C# from examples/squeezenet.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;SqueezeNet_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** and ***NcnnDotNetNativeDnn.dll*** to output directory; &lt;SqueezeNet_dir&gt;\bin\Release\netcoreapp3.1. 
 
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

vkEnumerateInstanceExtensionProperties failed -6
1 = 0.9998976
107 = 3.3139066E-05
950 = 1.42037325E-05
````