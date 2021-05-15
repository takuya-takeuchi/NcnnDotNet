#ifndef _CPP_LAYER_LAYERS_PROPOSAL_H_
#define _CPP_LAYER_LAYERS_PROPOSAL_H_

#include "../../export.h"
#include <layer.h>
#include <layer/proposal.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Proposal_new(ncnn::Proposal** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Proposal();

    return error;
}

DLLEXPORT void layer_layers_Proposal_delete(ncnn::Proposal* layer)
{
    if (layer != nullptr) delete layer;
}

#endif