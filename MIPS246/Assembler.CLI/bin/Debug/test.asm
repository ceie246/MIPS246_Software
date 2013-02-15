.text
.globl __start
__start:
SLL $t1 $t2 -1
# load with sign extension
JR $31
JALR $t0
# load without sign extension
addressB:
JR $ra