// In order to work properly, your client should be properly configured for the
// 1252-Windows encoding. Alternatively, you may have to change to byte[] for it
// to pass properly over a socket. Written by Emil MA~1/4ller.

public string LockToKey(string lck)
{
    string Key = "";
    for (int i = 0, j; lck.Length > i; i++)
    {
        if (i == 0) j = lck[0] ^ 5;
        else j = lck[i] ^ lck[i - 1];
        for (j += ((j % 17) * 15); j > 255; j -= 255) ;
        switch (j)
        {
            case 0:
            case 5:
            case 36:
            case 96:
            case 124:
            case 126:
                Key += "/%DCN" + ((string)("00" + j.ToString())).Substring(j.ToString().Length - 1) + "%/";
                break;
            default:
                Key += (char)j;
                break;
        }
    }
    return (char)(Key[0] ^ Key[Key.Length - 1]) + Key.Substring(1);
}