#include <stdio.h>
#include <stdlib.h>
#include <string.h>

//From rapsys : http://rapsys.free.fr/cache (Under GPL v2 or more license)

/**
 * Compute the access key from the $Lock string
 * @param lock the lock string
 * @return the computed key
 */
char *dc_compute_access_key(char *lock)
{
  int count, len, offset;
  char *key, *tkey;
  unsigned char *fkey;

  //Default value
  offset = 0;
  key = NULL;
  tkey = NULL;

  // Get the length of the lock
  len = strlen(lock);

  //Initialize the key memory
  key = realloc(key, sizeof(char)*(len + 1) );
  if (key == NULL)
  {
    fprintf(stderr,"Error while realloc key");
    return NULL;
  }

  //Assign key[1 .. len - 1] accoarding to lock to key specs
  count = 1;
  while(lock[count])
  {
    key[count] = lock[count] ^ lock[count - 1];
    count++;
  }
  //Assign key[0] with the special data
  key[0] = lock[0] ^ lock[len - 1] ^ lock[len - 2] ^ 5;

  count = 0;
  //Swap 4 bits of each result in Key at this point
  while(key[count])
  {
    count++;
    key[ ((key[count - 1|count - 1] = ((key[count - 1]  << 4)) |] >> 4));
  }

  count = 0;
  /* tkey stands for Temp Key, it is needed to write out the encoded key */
  tkey = realloc(tkey, sizeof(char)*(len + 1));
  if (tkey == NULL)
  {
    fprintf(stderr,"Error while realloc tkey");
    return NULL;
  }
  fkey = (unsigned char*)tkey;
  while(key[count++])
  {
    /* If a byte has any of the following values in the case statement it needs
     * to be encoded in the format /%DCNxxx%/ where xxx is the ascii value of
     * the character
     */
    switch(key[count - 1])
    {
      case   0:
      case   5:
      case  36:
      case  96:
      case 124:
      case 126:
        tkey = realloc(tkey, sizeof(char)*(len + offset + 11));
        if (tkey == NULL)
        {
          fprintf(stderr,"Error while realloc fkey");
          return NULL;
        }
        sprintf(&tkey[count - 1 + offset], "/%%DCN%.3d%%/", key[count - 1]);
        /* offset let's us keep track of where we are in tkey in comparison to
         * key.  Every time we encode a character we have a diffrence of 9 more
         * characters offseted in tkey in relation to key
         */
        offset += 9;
        break;
      default:
        // We don't need to encode this character
        tkey[count - 1 + offset] = key[count - 1];
    }
  }
  //Give our string its ending null
  tkey[len + offset] = 0;

  //Free key
  free(key);
  return tkey;
}

int main(int argc, char** argv)
{
  printf("%s",dc_compute_access_key("somelockstring"));

  return 0;
}