.text
.globl __start
__start:
# load with sign extension
JR $31
JALR $t0
# load without sign extension
addressB:
JR $ra