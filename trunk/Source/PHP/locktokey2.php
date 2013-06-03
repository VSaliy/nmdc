/**********************************
 *            lock2key            *
 *      -------------------       *
 *   Sunday, January 27, 2007     *
 *   (C) 2007 The Freeman Group   *
 *   emdfreeman [at] mail . ru    *
 *                                *
 **********************************/

function lock2key($lock){
    $len = strlen($lock);
    $key[0] = ord($lock[0]) ^ ord($lock[$len-1]) ^ ord($lock[$len-2]) ^ 5;
 
    for ($i = 1; $i < $len; $i++) $key[$i] = ord($lock[$i]) ^ ord($lock[$i-1]);
    for ($i = 0; $i < $len; $i++) $key[$i] = (($key[$i]<<4) & 240) | (($key[$i]>>4) & 15);
    
    $key = array_map('chr',$key);
    for($i = 0; $i<$len; $i++){

            for($z = 0; $z<240; $z++){
                    if(strlen($z) === 1 ) if( $key[$i] == chr($z)) $key[$i] = '/%DCN00'.$z.'%/';
            }

    }
    $key = implode("",$key);
    return $key;
}