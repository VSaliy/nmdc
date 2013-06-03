#include <stdlib.h>
/* Taken from Dolda Connect -- Licensed under the GPL */

static int reservedchar(unsigned char c)
{
    return((c == 0) || (c == 5) || (c == 124) || (c == 96) || (c == 126) || (c == 36));
}

static char *dcmakekey(char *lock)
{
    int i, len, offset;
    char *buf, *key;
    char save;
    
    /* Step 1: Compute key */
    buf = malloc(strlen(lock));
    save = 5;
    len = 0;
    for(i = 0; lock[i]; i++) {
        buf[i] = lock[i] ^ save;
        buf[i] = ((buf[i] & 0x0F) << 4) | ((buf[i] & 0xF0) >> 4);
        save = lock[i];
        if((i != 0) && reservedchar(buf[i]))
            len += 10;
        else
            len++;
    }
    buf[0] ^= buf[i - 1];
    if(reservedchar(buf[0]))
        len += 10;
    else
        len++;
    
    /* Step 2: Quote reserved characters */
    key = malloc(len + 1);
    offset = 0;
    for(i = 0; lock[i] != 0; i++) {
        if(reservedchar(buf[i]))
            offset += sprintf(key + offset, "/%%DCN%03i%%/", buf[i]);
        else
            key[offset++] = buf[i];
    }
    key[offset] = 0;
    free(buf);
    
    /* Observe: The caller will have to free the memory */
    return(key);
}