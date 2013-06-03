#include <string.h>
#include <stdlib.h>

/* erealloc done by James N. Hart
   Abridged version.
   INPUT:
     void **mem
       - should be a pointer to the pointer that you want to allocate or
       reallocate memory to.  If *mem == NULL (0) then data will be allocated
       from scratch.  If != NULL then data will be realloc'ed.
     int size
       - should be the size of the memory you want allocated. *note this
       revision doesn't do any checking for size 0.  Results are undefined if
       size == 0..

    OUTPUT:
      void **mem
        - assigned with allocated memory.  Memory is initialized with all
        bits 0.

    PURPOSE:
      To give one interface for malloc and realloc, to initialized malloc'ed
      data, and to do error checking and handling.

    ABRIDGED NOTE:
      There are comments where you should add your own error handling code.

    EXAMPLE:
      Create a character array of 35 characters, then resize it to 25.
      char *string = 0;  erealloc((void **)&string, sizeof(char)*35);
      erealloc((void **)&string, sizeof(char)*25);
*/
void erealloc(void **mem, size_t size)
{
   int err = 0;
   if(!*mem)
     {
        if(!(*mem = malloc(size)))
          {
            /*You should handle a memory allocation error here */
            exit(1);
          }
        memset(*mem, 0, size);
        return;
     }
   if(! (*mem = realloc(*mem, size) ) )
     {
        /*You should handle a memory allocation error here */
        exit(1);
     }

}

/* generateKey done by James N. Hart
   INPUT:
     unsigned char *lock
       - A null terminated character array that contains the lock posted with
       $Lock (excluding $Lock= and ' PK=*')
     unsigned char **fkey
       - a pointer to a previous allocated char *array or a pointer to a
       char *array that has been initialized wiht NULL (0).

    OUTPUT:
      unsigned char **fkey 
        - assigned a allocated char *array, null terminated that contains
        the key.

    PURPOSE:
      To generate the key from given lock for as specified for the
      DC Protocol.

    BUGS:
      If you want to use this in a real program, you may want to add error
      handling to check that the lock is at least 2 characters long. (if not
      this code should crash)

    EXAMPLE:
      char *lock = "IEatHamsters", *key = 0;
      generateKey(lock, &key);
*/
void generateKey(unsigned char *lock, unsigned char **fkey)
{
   int count = 0, len = 0, offset = 0;
   char *key = 0, *tkey= 0;
   /* Get the length of the lock */
   len = strlen(lock);
   /* Initialize the key memory */
   erealloc((void **)&key, sizeof(char)*(len + 1) );

   /* assign key[1 .. len - 1] accoarding to lock to key specs */
   while(lock[++count])
        key[count] = lock[count] ^ lock[count - 1];
   /* assign key[0] with the special data */
   key[0] = lock[0] ^ lock[len - 1] ^ lock[len - 2] ^ 5;

   count = 0;
   /* Swap 4 bits of each result in Key at this point */
   while(key[count++])
     key[count - 1] = ((key[count - 1]  << 4)) | ((key[count - 1] >> 4));

   count = 0;
   /* tkey stands for Temp Key, it is needed to write out the encoded key */
   erealloc((void **)fkey, sizeof(char)*(len + 1));
   tkey = *fkey;
   while(key[count++])
     {
        /* If a byte has any of the following values in the case statement it needs
           to be encoded in the format /%DCNxxx%/ where xxx is the ascii value of
           the character */
        switch(key[count - 1])
          {
           case 0:
           case 5:
           case  36:
           case 96:
           case 124:
           case 126:
             erealloc((void **)&tkey, sizeof(char)*(len + offset + 11));
             sprintf(&tkey[count - 1 + offset], "/%%DCN%.3d%%/", key[count - 1]);
             /* offset let's us keep track of where we are in tkey in comparison to
                key.  Every time we encode a character we have a diffrence of 9 more
                characters offseted in tkey in relation to key
            */
             offset += 9;
             break;
             /* We don't need to encode this character */
           default:
             tkey[count - 1 + offset] = key[count - 1];
             break;
          }

     }
   /* Give our string its ending null */
   tkey[len + offset] = 0;
   /* Assign memory to fkey */
   *fkey = tkey;
   /* Free key */
   free(key);
}