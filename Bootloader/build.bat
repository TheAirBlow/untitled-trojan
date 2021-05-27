@echo off
C:\Users\ADMIN\AppData\Local\bin\NASM\nasm.exe boot.asm -f bin -o boot.bin
qemu-system-x86_64 -L "C:\Program Files\Qemu" -drive file=boot.bin