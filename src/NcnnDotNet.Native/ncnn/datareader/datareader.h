#ifndef _CPP_DATAREADER_DATAREADER_H_
#define _CPP_DATAREADER_DATAREADER_H_

#include "../export.h"
#include <datareader.h>
#include "../shared.h"

class DataReader : ncnn::DataReader
{
public:
    DataReader(void (*constructor_function)(DataReader*) = nullptr,
               void (*destructor_function)() = nullptr,
               int32_t (*scan_function)(const char*, void*) = nullptr,
               size_t (*read_function)(void*, size_t) = nullptr);
    virtual ~DataReader();

public:
    virtual int32_t scan(const char* format, void* p) const;
    virtual size_t read(void* buf, size_t size) const;

private:
    void (*m_constructor_function)(DataReader*);
    void (*m_destructor_function)();
    int32_t (*m_scan_function)(const char*, void*);
    size_t (*m_read_function)(void*, size_t);
};

DLLEXPORT DataReader* datareader_DataReader_new(void (*constructor_function)(DataReader*),
                                                void (*destructor_function)(),
                                                int32_t (*scan_function)(const char*, void*),
                                                size_t (*read_function)(void*, size_t))
{
    return new DataReader(constructor_function,
                          destructor_function,
                          scan_function,
                          read_function);
}

DLLEXPORT void datareader_DataReader_delete(DataReader* reader)
{
    if (reader != nullptr) delete reader;
}

#pragma region DataReaderFromMemory

class DataReaderFromMemoryWrapper
{
public:
    DataReaderFromMemoryWrapper(const uint8_t* mem, const uint32_t length);
    virtual ~DataReaderFromMemoryWrapper();

public:
    ncnn::DataReaderFromMemory* get() const;

private:
    ncnn::DataReaderFromMemory* m_reader;
    uint8_t* m_mem;
};

DLLEXPORT DataReaderFromMemoryWrapper* datareader_DataReaderFromMemory_new(const uint8_t*& mem, const uint32_t length)
{
    return new DataReaderFromMemoryWrapper(mem, length);
}

DLLEXPORT void datareader_DataReaderFromMemory_delete(DataReaderFromMemoryWrapper* reader)
{
    if (reader != nullptr) delete reader;
}

#pragma endregion DataReaderFromMemory

#endif