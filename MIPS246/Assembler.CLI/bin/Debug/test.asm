.text
.globl __start
__start:
ADD $t1 $t2 $t3
# load with sign extension
JR $31
JALR $t0
# load without sign extension
addressB:
JR $ra