'--------------------------------
' Done by Ofca
' ofca@ofca.pl
'--------------------------------
' Wiki edit by bluebear: Old style VB6 LockToKey; would probably not work with the newer versions of hub software

Function Lock2Key(StrLock As String) As String
    Dim TLock2Key As String, TChar As Integer
    If Len(StrLock) < 3 Then
        Lock2Key = Left$("BROKENCLIENT", Len(StrLock))
        Exit Function
    End If
    TLock2Key = Chr$(Asc(Left$(StrLock, 1)) Xor Asc(Right$(StrLock, 1)) Xor Asc(Mid$(StrLock, Len(StrLock) - 1, 1)) Xor 5)
    For i = 2 To Len(StrLock)
        TLock2Key = TLock2Key & Chr$(Asc(Mid$(StrLock, i, 1)) Xor Asc(Mid$(StrLock, i - 1, 1)))
    Next i
    For i = 1 To Len(TLock2Key)
        TChar = Asc(Mid$(TLock2Key, i, 1))
        TChar = TChar * 16 + TChar  16 'Swap bits 11110000 -> 00001111
        TChar = TChar Mod 256
        If TChar = 0 Or TChar = 5 Or TChar = 36 Or TChar = 96 Or TChar = 124 Or TChar = 126 Then
            Lock2Key = Lock2Key & "/%DCN" & Right$("000" & TChar, 3) & "%/"
        Else
            Lock2Key = Lock2Key & Chr$(TChar)
        End If

    Next i
End Function