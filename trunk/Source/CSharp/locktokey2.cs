public string LockToKey(string Lck)
{
  /*
   * This LockToKey is a direct translation of bluebear's VB.NET LockToKey
   *   into CSharp by kepp
   * Originally written by bluebear - http://www.thewildplace.dk/cache
   * Csharp translation was broken and fixed by bluebear
   * If this don't work for you it's because you use the wrong encoding on
   *   inbound/outbound data (System.Text.Encoding.Default)
   */

  /*
   * Edited by Carraya (I hate bugs)
   */

  Lck = Lck.Replace("$Lock ","");
  string sKey; 
  string sTmp; 
  byte iLen; 
  int iChar; 
  int iPos = Lck.IndexOf(" Pk=",1);

  if (Convert.ToBoolean(iPos))
    Lck = Lck.Substring(0,iPos);
  iChar = (Strings.Asc(Lck) ^ Strings.Asc(Lck.Substring(Lck.Length - 1)) ^ Strings.Asc(Lck.Substring(Lck.Length - 2, 1)) ^ 5);
  iChar = (iChar / 16) ^ (iChar * 16);

  while(iChar > 255)
  {
    iChar = iChar - 256;
  } 

  if ((iChar == 0) || (iChar == 5) || (iChar == 36) || (iChar == 96) || (iChar == 124) || (iChar == 126))
  {
    sTmp = "00" + iChar.ToString(); 
    iLen = Convert.ToByte(sTmp.Length);
    if (iLen > 3)
      iLen -= 3;
    else
      iLen = 0;
    sKey = "/%DCN" + sTmp.Substring(iLen) + "%/";
  }
  else
  {
    sKey = Strings.Chr(iChar).ToString();
  }

  for(iPos = 1; iPos < Lck.Length; iPos++)
  {
    iChar = Strings.Asc(Lck.Substring(iPos, 1)) ^ Strings.Asc(Lck.Substring(iPos - 1, 1));
    iChar = (iChar / 16) ^ (iChar * 16); 

    while(iChar > 255)
    {
      iChar = iChar - 256;
    }  

    if ((iChar == 0) || (iChar == 5) || (iChar == 36) || (iChar == 96) || (iChar == 124) || (iChar == 126))
    {
      sTmp = "00" + iChar.ToString(); 
      iLen = Convert.ToByte(sTmp.Length);
      if (iLen > 3) 
        iLen -= 3;
      else
        iLen = 0;
      sKey += "/%DCN" + sTmp.Substring(iLen) + "%/";
    }
    else
    {
      sKey += Strings.Chr(iChar).ToString();
    }
  }

  return sKey;
}