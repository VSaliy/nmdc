'------------------------------------------------
'Function made by Massimiliano (Neo) from Italy -
'------------------------------------------------

' Wiki edit by bluebear: This LockToKey was acutally posted as a vb.net converter wich it's not.
' I've havent tryed this one, but guess it will work for vb6.
Public Function LockToKey(Lck As String) As String
    ' Edited by Х--Ga`uосvЩ--Х, the old LockToKey wasn't working properly as it was made for vb.net  converter
    Dim h As Integer, j As Integer
    n = 5

    h = InStr(1, Lck, " ")
    If h Then Lck = Left$(Lck, h - 1)

    h = Asc(Lck) Xor Asc(Right$(Lck, 1)) Xor Asc(Right$(Lck, 2)) Xor n
    h = (h \ 16) Xor (h * 16)

    Do While h > 255
        h = h - 256
    Loop

    Select Case h
        Case 0, 5, 36, 96, 124, 126
            LockToKey = "/%DCN" & Right$("00" & CStr(h), 3) & "%/"
        Case Else
            LockToKey = Chr$(h)
    End Select

    For j = 2 To Len(Lck)
        h = Asc(Mid$(Lck, j, 1)) Xor Asc(Mid$(Lck, j - 1, 1))
        h = (h \ 16) Xor (h * 16)
        Do While h > 255
            h = h - 256
        Loop

        Select Case h
            Case 0, 5, 36, 96, 124, 126
                LockToKey = LockToKey & "/%DCN" & Right$("00" & CStr(h), 3) & "%/"
            Case Else
                LockToKey = LockToKey & Chr$(h)
        End Select
    Next

End Function