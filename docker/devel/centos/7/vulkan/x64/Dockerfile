FROM centos:7
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

RUN yum update -y && yum install -y \
    ca-certificates \
    wget

# install package to build
RUN yum install -y https://dl.fedoraproject.org/pub/epel/epel-release-latest-7.noarch.rpm
RUN yum update -y && yum install -y \
    libX11-devel \
    cmake3 \
    gtk2-devel \
    pkg-config
RUN yum groupinstall -y "Development Tools"

# set compiler
ENV CMAKE_C_COMPILER=/usr/bin/gcc
ENV CMAKE_CXX_COMPILER=/usr/bin/g++

# Register Microsoft key and feed
RUN yum update -y && yum install -y \
    curl
RUN curl https://packages.microsoft.com/config/rhel/7/prod.repo > /etc/yum.repos.d/microsoft.repo
RUN yum update -y && yum install -y \
    powershell \
 && yum clean all

# user cmake 3 instead of cmake 2
RUN alternatives --install /usr/local/bin/cmake cmake /usr/bin/cmake 10 \
                 --slave /usr/local/bin/ctest ctest /usr/bin/ctest \
                 --slave /usr/local/bin/cpack cpack /usr/bin/cpack \
                 --slave /usr/local/bin/ccmake ccmake /usr/bin/ccmake \
                 --family cmake
RUN alternatives --install /usr/local/bin/cmake cmake /usr/bin/cmake3 20 \
                 --slave /usr/local/bin/ctest ctest /usr/bin/ctest3 \
                 --slave /usr/local/bin/cpack cpack /usr/bin/cpack3 \
                 --slave /usr/local/bin/ccmake ccmake /usr/bin/ccmake3 \
                 --family cmake

# install vulkan sdk
ENV VULKAN_SDK_VERSION 1.2.189.0
WORKDIR /usr/share/vulkan
RUN curl --output vulkansdk-linux-x86_64-${VULKAN_SDK_VERSION}.tar.gz https://sdk.lunarg.com/sdk/download/${VULKAN_SDK_VERSION}/linux/vulkansdk-linux-x86_64-${VULKAN_SDK_VERSION}.tar.gz?Human=true
RUN tar -xf vulkansdk-linux-x86_64-${VULKAN_SDK_VERSION}.tar.gz
RUN rm vulkansdk-linux-x86_64-${VULKAN_SDK_VERSION}.tar.gz
ENV VULKAN_SDK /usr/share/vulkan/${VULKAN_SDK_VERSION}/x86_64

WORKDIR /