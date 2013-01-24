.text
.globl __start
__start:
# load with sign extension
lw $t0, memory
lh $t1, memory
lb $t2, memory
# load without sign extension
lhu $t3, memory
lbu $t4, memory
.data
memory:
.word 0xABCDE080 # little endian: 80E0CDAB