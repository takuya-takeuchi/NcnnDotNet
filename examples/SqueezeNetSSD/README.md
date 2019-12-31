# SqueezeNet SSD
  
This program is ported by C# from examples/squeezenetssd.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;SqueezeNetSSD_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** to output directory; &lt;SqueezeNetSSD_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;SqueezeNetSSD_dir&gt;. 

## 2. Download demo data

Download test data from the following urls.

- https://github.com/nihui/ncnn-assets/tree/master/models
  - squeezenet_ssd_voc.bin
  - squeezenet_ssd_voc.param

And extract them and copy to extracted files to &lt;SqueezeNetSSD_dir&gt;.
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <SqueezeNetSSD_dir> 
dotnet run --configuration Release -- dog.jpg

vkEnumerateInstanceExtensionProperties failed -6
3 = 0.99954 at 141.22 97.22 470.17 345.85
````

![SqueezeNetSSD](images/image.png "SqueezeNetSSD")