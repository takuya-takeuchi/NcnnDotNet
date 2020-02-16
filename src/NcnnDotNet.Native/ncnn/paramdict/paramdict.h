#ifndef _CPP_PARAMDICT_PARAMDICT_H_
#define _CPP_PARAMDICT_PARAMDICT_H_

#include "../export.h"
#include <ncnn/paramdict.h>
#include "../shared.h"

DLLEXPORT int paramdict_ParamDict_new(ncnn::ParamDict** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::ParamDict();

    return error;
}

DLLEXPORT void paramdict_ParamDict_delete(ncnn::ParamDict* paramdict)
{
    if (paramdict != nullptr) delete paramdict;
}

DLLEXPORT void paramdict_ParamDict_set(ncnn::ParamDict* paramdict, const int32_t id, const int32_t i)
{
    paramdict->set(id, i);
}

DLLEXPORT void paramdict_ParamDict_set2(ncnn::ParamDict* paramdict, const int32_t id, const float f)
{
    paramdict->set(id, f);
}

#endif