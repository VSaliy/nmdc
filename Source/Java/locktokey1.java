/**
 * Contributed by dCoy
 */
public static String generateKey(String lockString){
    int i = 0;
    byte[] lock = null;
    byte[] key = null;

    lockString = lockString.substring(0,lockString.indexOf(' '));
    lockString.trim();
    lock = lockString.getBytes();
    key = new byte[lock.length];

    for(i=1;i<lock.length;i++){
        key[i] = (byte)((lock[i] ^ lock[i-1]) & 0xFF);
    }

    key[0] = (byte)((((lock[0] ^ lock[lock.length-1]) ^ lock[lock.length-2]) ^ 5) & 0xFF);

    for(i=0;i<key.length;i++){
        key[i] = (byte)((((key[i]<<4) & 0xF0) | ((key[i]>>4) & 0x0F)) & 0xFF);
    }

    return(dcnEncode(new String(key)));
}

public static String dcnEncode(String string){
    char[] replacements = null;
    int i = 0;
    int index = 0;

    replacements = new char[]{0,5,36,96,124,126};

    for(i=0;i<replacements.length;i++){
        while((index = string.indexOf(replacements[i])) >=0 ){
            string = string.substring(0,index)
                + "/%DCN"+leadz(replacements[i])+"%/"
                + string.substring(index+1,string.length());
        }
    }

    return(string);
}

private static String leadz(int nr){
    if(nr < 100 && nr > 10){
        return("0"+nr);
    } else if(nr < 10){
        return("00"+nr);
    } else{
        return(""+nr);
    }
}