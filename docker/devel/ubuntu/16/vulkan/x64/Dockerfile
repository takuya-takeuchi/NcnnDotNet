FROM ubuntu:16.04
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# install package to build
RUN apt-get update && apt-get install -y \
    build-essential \
    libx11-dev \
    libgtk2.0-dev \
    pkg-config \
    apt-transport-https \
    wget

# cmake
ENV CMAKE_VERSION 3.21.3
ENV ARCH linux-x86_64
RUN wget -q https://github.com/Kitware/CMake/releases/download/v${CMAKE_VERSION}/cmake-${CMAKE_VERSION}-${ARCH}.sh && chmod +x cmake-${CMAKE_VERSION}-${ARCH}.sh
RUN echo y | ./cmake-${CMAKE_VERSION}-${ARCH}.sh
RUN mv cmake-${CMAKE_VERSION}-${ARCH} /opt && echo y | ln -s /opt/cmake-${CMAKE_VERSION}-${ARCH}/bin/* /usr/bin

# set compiler
ENV CMAKE_C_COMPILER=/usr/bin/gcc
ENV CMAKE_CXX_COMPILER=/usr/bin/g++

# Register Microsoft key and feed
RUN apt-get update && apt-get install -y \
    wget \
    curl \
    apt-transport-https
RUN wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb && rm packages-microsoft-prod.deb
RUN apt-get update && apt-get install -y \
    powershell \
 && apt-get clean && rm -rf /var/lib/apt/lists/*

# install vulkan sdk
ENV VULKAN_SDK_VERSION 1.2.189.0
WORKDIR /usr/share/vulkan
RUN curl --output vulkansdk-linux-x86_64-${VULKAN_SDK_VERSION}.tar.gz https://sdk.lunarg.com/sdk/download/${VULKAN_SDK_VERSION}/linux/vulkansdk-linux-x86_64-${VULKAN_SDK_VERSION}.tar.gz?Human=true
RUN tar -xf vulkansdk-linux-x86_64-${VULKAN_SDK_VERSION}.tar.gz
RUN rm vulkansdk-linux-x86_64-${VULKAN_SDK_VERSION}.tar.gz
ENV VULKAN_SDK /usr/share/vulkan/${VULKAN_SDK_VERSION}/x86_64

WORKDIR /
# old cmake does not have FindVulkan.cmake
RUN cp /usr/share/vulkan/${VULKAN_SDK_VERSION}/source/Vulkan-Tools/cmake/FindVulkan.cmake /opt/cmake-${CMAKE_VERSION}-${ARCH}/Modules