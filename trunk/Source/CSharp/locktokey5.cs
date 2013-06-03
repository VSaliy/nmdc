public static string DecodeLock(string aLock)
{
    byte[] key      =  new byte[aLock.Length];
    byte[] bLock    = Encoding.ASCII.GetBytes(aLock); // On System.Text

    for (int i = 1; i < aLock.Length; i++)
        key[i] = (byte)(bLock[i] ^ bLock[i - 1]);
    key[0] = (byte)(bLock[0] ^ bLock[bLock.Length - 1] ^ bLock[bLock.Length - 2] ^ 5);
    for (int i = 0; i < aLock.Length; i++)
        key[i] = (byte)(((key[i] << 4) & 240) | ((key[i] >> 4) & 15));

    return EscapeChars(key);
}
public static string EscapeChars(byte[] key)
{
    StringBuilder builder = new StringBuilder(key.Length);

    for (int index = 0; index < key.Length; index++)
    {
        if (key[index] == 0 || key[index] == 5 || key[index] == 36 || key[index] == 96 || key[index] == 124 || key[index] == 126)
            builder.AppendFormat("/%DCN{0:000}%/", key[index]);
        else
            builder.Append((char)key[index]);
    }

    return builder.ToString();
}