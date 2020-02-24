#ifndef _CPP_PRNG_PRNG_H_
#define _CPP_PRNG_PRNG_H_

#include "../export.h"
#include "prng.h"
#include <string.h>
#include "../shared.h"

DLLEXPORT int prng_prng_rand_t_new(prng_rand_t** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new prng_rand_t();
    memset(*returnValue, 0, sizeof(prng_rand_t));

    return error;
}

DLLEXPORT void prng_prng_rand_t_delete(prng_rand_t* state)
{
    if (state != nullptr) delete state;
}

DLLEXPORT int prng_prng_rand(prng_rand_t *state, uint64_t* returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = prng_rand(state);

    return error;
}

DLLEXPORT int prng_prng_srand(const uint64_t seed, prng_rand_t *state)
{
    int32_t error = ERR_OK;

    prng_srand(seed, state);

    return error;
}

#endif
