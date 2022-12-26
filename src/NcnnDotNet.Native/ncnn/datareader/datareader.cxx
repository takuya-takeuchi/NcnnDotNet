#include "datareader.h"

#include <cstring>

#pragma region DataReaderFromMemoryWrapper

DataReaderFromMemoryWrapper::DataReaderFromMemoryWrapper(const uint8_t* mem, const uint32_t length):
    m_mem(nullptr),
    m_mem_ref(nullptr),
    m_reader(nullptr)
{
    this->m_mem = (uint8_t*)std::malloc(length);
    std::memcpy(this->m_mem, mem, length);

    // m_mem will be modified in ncnn::DataReaderFromMemory
    // So store in m_mem_ref and it must be delete in destructor
    this->m_mem_ref = this->m_mem;
    this->m_reader = new ncnn::DataReaderFromMemory((const uint8_t* &)this->m_mem);
}

DataReaderFromMemoryWrapper::~DataReaderFromMemoryWrapper()
{
    if (this->m_reader)
    {
        delete m_reader;
        this->m_reader = nullptr;
    }

    if (this->m_mem_ref)
    {
        free(this->m_mem_ref);
        this->m_mem_ref = nullptr;
    }

    this->m_mem = nullptr;
}

int DataReaderFromMemoryWrapper::scan(const char* format, void* p) const
{
    return this->m_reader->scan(format, p);
}

size_t DataReaderFromMemoryWrapper::read(void* buf, size_t size) const
{
    return this->m_reader->read(buf, size);
}

#pragma endregion DataReaderFromMemoryWrapper