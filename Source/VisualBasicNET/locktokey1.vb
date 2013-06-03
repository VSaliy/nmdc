Public Function LocktoKey(ByVal Lck As String) As String
    '// This LockToKey is for VB.NET
    '// Written by bluebear - http://www.thewildplace.dk/cache
    '// It uses the .net framework string class instead of the old vb6 style
    '// If this don't work for you it's because you use the wrong encoding on inbound/outbound data
    '// Expects lock with "$Lock" and PK in string.
    '// 

    Lck = Lck.Replace("$Lock ", "")
    Dim sKey As String
    Dim sTmp As String
    Dim iLen As Byte
    Dim iChar As Integer
    Dim iPos As Integer = Lck.IndexOf(" ", 1)
    If CBool(iPos) Then Lck = Lck.Substring(0, iPos)
    iChar = Asc(Lck) Xor Asc(Lck.Substring(Lck.Length - 1)) Xor Asc(Lck.Substring(Lck.Length - 2, 1)) Xor 5
    iChar = (iChar \ 16) Xor (iChar * 16)
    Do While iChar > 255
        iChar = iChar - 256
    Loop
    Select Case iChar
        Case 0, 5, 36, 96, 124, 126
            sTmp = "00" & CStr(iChar)
            iLen = CByte(sTmp.Length)
            If iLen > 3 Then iLen -= CByte(3) Else iLen = 0
            sKey = "/%DCN" & sTmp.Substring(iLen) & "%/"
        Case Else
            sKey = Chr(iChar)
    End Select
    For iPos = 1 To Lck.Length - 1
        iChar = Asc(Lck.Substring(iPos, 1)) Xor Asc(Lck.Substring(iPos - 1, 1))
        iChar = (iChar \ 16) Xor (iChar * 16)
        Do While iChar > 255
            iChar = iChar - 256
        Loop
        Select Case iChar
            Case 0, 5, 36, 96, 124, 126
                sTmp = "00" & CStr(iChar)
                iLen = CByte(sTmp.Length)
                If iLen > 3 Then iLen -= CByte(3) Else iLen = 0
                sKey += "/%DCN" & sTmp.Substring(iLen) & "%/"
            Case Else
                sKey += Chr(iChar)
        End Select
    Next
    Return sKey
End Function