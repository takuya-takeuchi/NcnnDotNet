#include "datareader.h"

#pragma region DataReader

DataReader::DataReader(void (*constructor_function)(DataReader*),
                       void (*destructor_function)(),
                       int32_t (*scan_function)(const char*, void*),
                       size_t (*read_function)(void*, size_t)):
    m_constructor_function(constructor_function),
    m_destructor_function(destructor_function),
    m_scan_function(scan_function),
    m_read_function(read_function)
{
    if (this->m_constructor_function)
        this->m_constructor_function(this);
}

DataReader::~DataReader()
{
    if (this->m_destructor_function)
        this->m_destructor_function();
}
int32_t DataReader::scan(const char* format, void* p) const
{
    if (this->m_scan_function)
        return this->m_scan_function(format, p);
    else
        return ncnn::DataReader::scan(format, p);
}

size_t DataReader::read(void* buf, size_t size) const
{
    if (this->m_read_function)
        return this->m_read_function(buf, size);
    else
        return ncnn::DataReader::read(buf, size);
}

#pragma endregion DataReader

#pragma region DataReaderFromMemoryWrapper

DataReaderFromMemoryWrapper::DataReaderFromMemoryWrapper(const uint8_t* mem, const uint32_t length):
    m_mem(nullptr),
    m_reader(nullptr)
{
    this->m_mem = (uint8_t*)std::malloc(length);
    std::memcpy(this->m_mem, mem, length);

    const uint8_t* &ref_mem = mem;
    this->m_reader = new ncnn::DataReaderFromMemory(ref_mem);
}

DataReaderFromMemoryWrapper::~DataReaderFromMemoryWrapper()
{    
    if (this->m_reader)
    {
        delete m_reader;
        this->m_reader = nullptr;
    }

    if (this->m_mem)
    {
        free(this->m_mem);
        this->m_mem = nullptr;
    }
}

ncnn::DataReaderFromMemory* DataReaderFromMemoryWrapper::get() const
{
    return this->m_reader;
}

#pragma endregion DataReaderFromMemoryWrapper