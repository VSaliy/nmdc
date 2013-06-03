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

            switch($key[$i]){

                    case chr(0):
                         $key[$i] = '/%DCN000%/';
                    break; 

                    case chr(2):
                         $key[$i] = '/%DCN005%/';
                    break;

                    case chr(36):
                         $key[$i] = '/%DCN036%/';
                    break; 

                    case chr(96):
                         $key[$i] = '/%DCN096%/';
                    break;

                    case chr(124):
                         $key[$i] = '/%DCN124%/';
                    break;

                    case chr(126):
                         $key[$i] = '/%DCN126%/';
                    break; 

           }
    }
    $key = implode("",$key);
    return $key;
}