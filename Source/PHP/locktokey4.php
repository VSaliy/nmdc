//---------------------------------
// owiec at barbara . eu . org [PL]
//---------------------------------

function keygen($lock)
{
     $len = strlen($lock);
     $key = array();
     $key[0] = ord($lock[0]) ^ ord($lock[$len-1]) ^ ord($lock[$len-2]) ^ 5;
     for ($i = 1; $i < $len; $i++)
        $key[$i] = ord($lock[$i]) ^ ord($lock[$i-1]);
     for ($i = 0; $i < $len; $i++)
        $key[$i] = (($key[$i]<<4) & 240) | (($key[$i]>>4) & 15);
     $key = array_map('chr',$key);
     for($i = 0; $i<$len; $i++) 
     {
       if( $key[$i] == chr(0))
         $key[$i] = '/%DCN000%/';
       if( $key[$i] == chr(5))
         $key[$i] = '/%DCN005%/';
       if( $key[$i] == chr(36))
         $key[$i] = '/%DCN036%/';
       if( $key[$i] == chr(96))
         $key[$i] = '/%DCN096%/';
       if( $key[$i] == chr(124))
         $key[$i] = '/%DCN124%/';
       if( $key[$i] == chr(126))
         $key[$i] = '/%DCN126%/';
     }
     $key = implode('',$key);
     return $key;
}