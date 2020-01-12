#include "datareader.h"

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
        this->m_read_function(buf, size);
    else
        return ncnn::DataReader::read(buf, size);
}