# by Benjamin Bruheim

def lock2key(lock):
    "Generates response to $Lock challenge from Direct Connect Servers"
    lock = [ord(c) for c in lock]
    key = [0]
    for n in range(1,len(lock)):
        key.append(lock[n]^lock[n-1])
    key[0] = lock[0] ^ lock[-1] ^ lock[-2] ^ 5
    for n in range(len(lock)):
        key[n] = ((key[n] << 4) | (key[n] >> 4)) & 255
    result = ""
    for c in key:
        if c in [0, 5, 36, 96, 124, 126]:
            result += "/%%DCN%.3i%%/" % c
        else:
            result += chr(c)
    return result

if __name__=="__main__":
    key = lock2key("T&AUreb/M_2Wtp_lZU)EA_yU_)2[2/_4u:,`L`3\\m:+ctsnyw9@")
    assert key=="\x82'vArqp\xd4&!\xd6V2@\xf23c\xf0\xc7\xc6@\xe1b\xc2\xa0g" \
           + "\xb1\x96\x96\xd1\x07\xb6\x14\xf4a\xc4\xc2\xc25\xf6\x13u\x11" \
           + "\x84qp\xd1q\xe0\xe4\x97"