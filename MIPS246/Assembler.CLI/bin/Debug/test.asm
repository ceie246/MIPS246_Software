.text
.globl __start
__start:
SLL $t1 $t2 22
# load with sign extension
JR $t1
JALR $t1
# load without sign extension
addressB:
JR $ra
J addressB
JAL __start