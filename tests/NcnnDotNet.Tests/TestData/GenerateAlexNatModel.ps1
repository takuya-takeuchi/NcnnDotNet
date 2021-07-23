$current = $PSScriptRoot

# download model file and deploy.txt
$path = Join-Path $current "bvlc_alexnet.caffemodel"
if (!(Test-Path ${path}))
{
    Invoke-WebRequest "http://dl.caffe.berkeleyvision.org/bvlc_alexnet.caffemodel" -OutFile "${path}"
}
$path = Join-Path $current "deploy.prototxt"
if (!(Test-Path ${path}))
{
    Invoke-WebRequest "https://raw.githubusercontent.com/BVLC/caffe/master/models/bvlc_alexnet/deploy.prototxt" -OutFile "${path}"
}

# upgrade model and deploy.txt in container
Write-Host "upgrade_net_proto_binary" -ForegroundColor Green
$new_model = "bvlc_alexnet.model"
docker run -v "${current}:/workspace" -it bvlc/caffe:cpu /opt/caffe/build/tools/upgrade_net_proto_binary /workspace/bvlc_alexnet.caffemodel /workspace/${new_model}

Write-Host "upgrade_net_proto_text" -ForegroundColor Green
$new_prototxt = "alexnet.deploy.prototxt"
docker run -v "${current}:/workspace" -it bvlc/caffe:cpu /opt/caffe/build/tools/upgrade_net_proto_text /workspace/deploy.prototxt /workspace/${new_prototxt}

# convert
Write-Host "caffe2ncnn" -ForegroundColor Green
caffe2ncnn alexnet.deploy.prototxt bvlc_alexnet.model alexnet.param alexnet.bin
Write-Host "caffe2ncnn" -ForegroundColor Green
ncnn2mem alexnet.param alexnet.bin alexnet.id.h alexnet.mem.h