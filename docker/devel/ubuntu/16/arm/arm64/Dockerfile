FROM ubuntu:16.04
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# install package to build
RUN apt-get update && apt-get install -y \
    build-essential \
    libx11-dev \
    libgtk2.0-dev \
    pkg-config \
    wget

# cmake
ENV CMAKE_VERSION 3.21.3
ENV ARCH linux-aarch64
RUN wget -q https://github.com/Kitware/CMake/releases/download/v${CMAKE_VERSION}/cmake-${CMAKE_VERSION}-${ARCH}.sh && chmod +x cmake-${CMAKE_VERSION}-${ARCH}.sh
RUN echo y | ./cmake-${CMAKE_VERSION}-${ARCH}.sh
RUN mv cmake-${CMAKE_VERSION}-${ARCH} /opt && echo y | ln -s /opt/cmake-${CMAKE_VERSION}-${ARCH}/bin/* /usr/bin

# install compiler for arm
# At fist, install binfmt-support before install qemu-user-static
RUN apt-get update && apt-get install -y \
    binfmt-support 
    
RUN apt-get update && apt-get install -y \
    qemu-user \
    qemu-user-static \
    g++-aarch64-linux-gnu \
    gcc-aarch64-linux-gnu 

# set compiler
ENV CMAKE_C_COMPILER=/usr/bin/aarch64-linux-gnu-gcc
ENV CMAKE_CXX_COMPILER=/usr/bin/aarch64-linux-gnu-g++

# set emulation config
ENV QEMU_LD_PREFIX=/usr/aarch64-linux-gnu

# Register Microsoft key and feed
RUN apt-get update && apt-get install -y \
    wget \
    apt-transport-https
RUN wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb && rm packages-microsoft-prod.deb
RUN apt-get update && apt-get install -y \
    powershell \
 && apt-get clean && rm -rf /var/lib/apt/lists/*