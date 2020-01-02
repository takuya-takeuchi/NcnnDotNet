FROM ubuntu:16.04
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# install vulkan sdk
RUN apt-get update && apt install -y wget apt-transport-https

ENV VULKAN_SDK_VERSION 1.1.92.1
WORKDIR /usr/share/vulkan
RUN wget https://sdk.lunarg.com/sdk/download/${VULKAN_SDK_VERSION}/linux/vulkansdk-linux-x86_64-${VULKAN_SDK_VERSION}.tar.gz?Human=true -O vulkansdk-linux-x86_64-${VULKAN_SDK_VERSION}.tar.gz
RUN tar -xf vulkansdk-linux-x86_64-${VULKAN_SDK_VERSION}.tar.gz
RUN rm vulkansdk-linux-x86_64-${VULKAN_SDK_VERSION}.tar.gz
ENV VULKAN_SDK ${PWD}/${VULKAN_SDK_VERSION}

# install package to build
RUN apt-get update && apt-get install -y \
    libx11-6 \
    && apt-get clean && rm -rf /var/lib/apt/lists/*