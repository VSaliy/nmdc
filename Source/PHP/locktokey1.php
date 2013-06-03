//------------------------------------------------------------------------------
	function EscapeDCN($s) {
	//------------------------------------------------------------------------------
		$search = array("\x00", "\x05", "\x24", "\x60", "\x7C", "\x7E");
		$replace = array('/%DCN000%/', '/%DCN005%/', '/%DCN036%/', '/%DCN096%/', '/%DCN124%/', '/%DCN126%/');
		return str_replace($search, $replace, $s);
	}
	//------------------------------------------------------------------------------
	function LockToKey($lock) {
	//------------------------------------------------------------------------------
		$len = strlen($lock);
		$key = array(0 => 0);
		for ($i = 1; $i < $len; $i++)
			$key[$i] = ord($lock[$i]) ^ ord($lock[$i-1]);
		$key[0] = ord($lock[0]) ^ ord($lock[$len-1]) ^ ord($lock[$len-2]) ^ 5;
		for ($i = 0; $i < $len; $i++)
			$key[$i] = chr((($key[$i] << 4) & 0xF0) | (($key[$i] >> 4) & 0x0F));
		return EscapeDCN(implode('', $key));
	}