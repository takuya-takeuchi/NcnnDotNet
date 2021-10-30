#ifndef _CPP_DATAREADER_DATAREADER_H_
#define _CPP_DATAREADER_DATAREADER_H_

#include "../export.h"
#include <datareader.h>
#include "../shared.h"

#pragma region DataReaderFromMemory

class DataReaderFromMemoryWrapper : public ncnn::DataReader
{
public:
    DataReaderFromMemoryWrapper(const uint8_t* mem, const uint32_t length);
    virtual ~DataReaderFromMemoryWrapper();

public:
    virtual int scan(const char* format, void* p) const;
    virtual size_t read(void* buf, size_t size) const;

private:
    ncnn::DataReaderFromMemory* m_reader;
    uint8_t* m_mem;
    uint8_t* m_mem_ref;
};

DLLEXPORT DataReaderFromMemoryWrapper* datareader_DataReaderFromMemory_new(const uint8_t* mem, const uint32_t length)
{
    return new DataReaderFromMemoryWrapper(mem, length);
}

DLLEXPORT void datareader_DataReaderFromMemory_delete(DataReaderFromMemoryWrapper* reader)
{
    if (reader != nullptr) delete reader;
}

#pragma endregion DataReaderFromMemory

#endif