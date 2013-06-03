/*
 * This LockToKey does NOT use Microsoft.VisualBasic as a reference 
 * also strips $Lock and Pk=
 * Written by Gargol (gargol@gbot.nu)
 */
public string L2K(string lck)
{
  lck = lck.Replace("$Lock ", "");
  int iPos = lck.IndexOf(" Pk=", 1);
  if (iPos > 0) lck = lck.Substring(0, iPos);
  int[] arrChar = new int[lck.Length + 1];
  int[] arrRet = new int[lck.Length + 1];
  arrChar[1] = lck[0];
  for (int i = 2; i < lck.Length + 1; i++)
  {
    arrChar[i] = lck[i - 1];
    arrRet[i] = arrChar[i] ^ arrChar[i - 1];
  }
  arrRet[1] = arrChar[1] ^ arrChar[lck.Length] ^ arrChar[lck.Length - 1] ^ 5;
  string sKey = "";
  for (int n = 1; n < lck.Length + 1; n++)
  {
    arrRet[n] = ((arrRet[n] * 16) & 240) | ((arrRet[n] / 16) & 15);
    int j = arrRet[n];
    switch (j)
    {
      case 0:
      case 5:
      case 36:
      case 96:
      case 124:
      case 126:
        sKey += "/%DCN"
             + ((string)("00" + j.ToString())).Substring(j.ToString().Length - 1)
             + "%/";
        break;
      default:
        sKey += Chr(Convert.ToByte((char)j));
        break;
    }
  }
  return sKey;
}

public static char Chr(byte src)
{
  return (Encoding.Default.GetChars(new byte[] { src })[0]);
}