FROM centos:7
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# install package to build
RUN yum update -y && yum install -y \
    ca-certificates
RUN yum install -y https://dl.fedoraproject.org/pub/epel/epel-release-latest-7.noarch.rpm
RUN yum update -y && yum install -y \
    libX11-devel \
    cmake3 \
    gtk2-devel \
    pkg-config
RUN yum groupinstall -y "Development Tools"

# install compiler for arm
RUN yum update -y && yum install -y \
    qemu-user \
    qemu-user-static \
    gcc-c++-aarch64-linux-gnu \
    gcc-aarch64-linux-gnu 

# set compiler
ENV CMAKE_C_COMPILER=/usr/bin/aarch64-linux-gnu-gcc
ENV CMAKE_CXX_COMPILER=/usr/bin/aarch64-linux-gnu-g++

# set emulation config
ENV QEMU_LD_PREFIX=/usr/aarch64-linux-gnu

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