// By RaptoR franz@digital-wave.de
// This function assumes you have stripped $Lock and remove everything from
//   Pk= onwards
// This code also works with yhub's extended char locks!
// Modified by Andreas Brekken <andreas@abrekken.com> for performance,
//   readability, less code

public static string Decode( string aLock )
{
    char[] key = new char[aLock.Length];

    for (int i = 1; i < aLock.Length; i++)
    {
        key[i] = Strings.Chr(Strings.Asc(aLock[i])
            ^ Strings.Asc(aLock[i - 1]));
    }

    key[0] = Strings.Chr(Strings.Asc(aLock[0])
        ^ Strings.Asc(aLock[aLock.Length - 1])
        ^ Strings.Asc(aLock[aLock.Length - 2])
        ^ 5);

    for (int i = 0; i < aLock.Length; i++)
    {
        key[i] = Strings.Chr(
            ((Strings.Asc(key[i]) << 4) & 240)
            | ((Strings.Asc(key[i]) >> 4) & 15));
    }

    string keyString = "";
    for (int i = 0; i < key.Length; i++)
    {
        int j = Strings.Asc(key[i]);
        if (j != (int)key[i])
        {
            key[i] = (char)j;
        }
        keyString += key[i];
    }
    return escapeChars(keyString);
}

private static string escapeChars( string key )
{
    System.Text.StringBuilder builder =
        new System.Text.StringBuilder(key.Length);

    for (int index=0; index<key.Length; index++)
    {
        int code = (int)key[index];
        if (code == 0 || code == 5 || code == 36 || code == 96
                || code == 124 || code == 126)
            builder.AppendFormat("/%DCN{0:000}%/", code);
        else
            builder.Append(key[index]);
    }

    return builder.ToString();
}