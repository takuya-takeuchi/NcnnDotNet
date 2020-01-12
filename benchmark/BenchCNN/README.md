# Bench CNN
  
This program is ported by C# from benchmark/benchncnn.cpp.

## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;BenchCNN_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***NcnnDotNetNative.dll*** to output directory; &lt;BenchCNN_dir&gt;\bin\Release\netcoreapp3.1. 
 
And extract them and copy to extracted files to &lt;BenchCNN_dir&gt;. 

## 2. Download demo data

Download test data from the local disk.

### for Linux

````bash
$ cd <BenchCNN_dir>
$ ./getParam.sh
````

### for Windows

````bash
$ cd <BenchCNN_dir>
$ getParam.bat
````
 
## 3. Run 
 
The following result is example. 
 
```` 
cd <BenchCNN_dir> 
dotnet run -c Release 4 2 0 0

[0 GeForce GTX 1080]  queueC=2[8]  queueG=0[16]  queueT=1[1]  buglssc=0
[0 GeForce GTX 1080]  fp16p=1  fp16s=1  fp16a=0  int8s=1  int8a=1
[1 Intel(R) UHD Graphics 630]  queueC=0[1]  queueG=0[1]  queueT=0[1]  buglssc=0
[1 Intel(R) UHD Graphics 630]  fp16p=1  fp16s=1  fp16a=1  int8s=1  int8a=1
loop_count = 4
num_threads = 2
powersave = 0
gpu_device = 0
          squeezenet  min =    1.86  max =    1.91  avg =    1.89
           mobilenet  min =    1.92  max =   13.60  avg =    5.01
        mobilenet_v2  min =    2.68  max =    2.72  avg =    2.71
        mobilenet_v3  min =    3.66  max =    3.69  avg =    3.68
          shufflenet  min =    2.23  max =    2.46  avg =    2.34
       shufflenet_v2  min =    2.67  max =    2.83  avg =    2.73
             mnasnet  min =    2.78  max =    3.22  avg =    2.90
     proxylessnasnet  min =    2.88  max =    2.91  avg =    2.89
           googlenet  min =    6.33  max =    6.50  avg =    6.40
            resnet18  min =    3.40  max =    3.43  avg =    3.42
             alexnet  min =    5.07  max =    5.14  avg =    5.11
               vgg16  min =   15.46  max =   16.15  avg =   15.64
            resnet50  min =    7.00  max =    7.04  avg =    7.03
      squeezenet_ssd  min =    7.44  max =    8.17  avg =    7.65
       mobilenet_ssd  min =    4.72  max =    4.75  avg =    4.74
      mobilenet_yolo  min =    5.20  max =    5.24  avg =    5.22
  mobilenetv2_yolov3  min =    4.83  max =    4.93  avg =    4.87
````